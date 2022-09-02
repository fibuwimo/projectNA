using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTest : MonoBehaviour
{
    [SerializeField] private float _maxAngularSpeed = Mathf.Infinity;
    [SerializeField] private float _smoothTime = 0.04f;
    [SerializeField] private Vector3 _forward = Vector3.forward;
    [SerializeField] private Vector3 _up = Vector3.up;
    [SerializeField] private Vector3 _axis = Vector3.up;
    private float _currentAngularVelocity;

    public float speed = 2f;
    float moveX = 0;
    float moveZ = 0;
    public Vector3 movingVelocity;

    public float jumpPower;
    private float distance = 0.6f;

    private Transform _transform;
    private Vector3 prevPosition;
    Rigidbody rb;
    private bool isMoving = false;
    private bool isJumpPressed = false;
    Animator animator;
    bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        prevPosition = _transform.position;
        rb = GetComponent<Rigidbody>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        var delta = position - prevPosition;
        //isJumpPressed = Input.GetButtonDown("Jump");
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveX,0,moveZ);
        direction.Normalize();
        movingVelocity = direction * speed;
        movingVelocity.y = position.y;
        if(movingVelocity.x==0&&movingVelocity.z==0){
            animator.SetBool("RUN",false);
        }else{
            if(isGround) animator.SetBool("RUN",true);
        }

        if(delta == Vector3.zero) return;
         // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
        // 回転補正計算
        var offsetRot = Quaternion.Inverse(Quaternion.LookRotation(_forward, _up));

        // ワールド空間の前方ベクトル取得
        var forward = _transform.TransformDirection(_forward);

        // 回転軸と垂直な平面に投影したベクトル計算
        var projectFrom = Vector3.ProjectOnPlane(forward, _axis);
        var projectTo = Vector3.ProjectOnPlane(delta, _axis);
        
        // 軸周りの角度差を求める
        var diffAngle = Vector3.Angle(projectFrom, projectTo);

        // 現在フレームで回転する角度の計算
        var rotAngle = Mathf.SmoothDampAngle(
            0,
            diffAngle,
            ref _currentAngularVelocity,
            _smoothTime,
            _maxAngularSpeed
        );
        
        // 軸周りでの回転の開始と終了を計算
        var lookFrom = Quaternion.LookRotation(projectFrom);
        var lookTo = Quaternion.LookRotation(projectTo);

        // 現在フレームにおける回転を計算
        var nextRot = Quaternion.RotateTowards(lookFrom, lookTo, rotAngle) * offsetRot;

        // オブジェクトの回転に反映
        _transform.rotation = nextRot;
        prevPosition = position;
    }

    void FixedUpdate(){
        //float jump = movingVelocity.y;
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);
        isGround = Physics.Raycast(ray, distance);
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
        Debug.Log(isGround);
        if (Input.GetButtonDown("Jump")){
            if(isGround){
                rb.AddForce(transform.up * jumpPower, ForceMode.Impulse); ;
                animator.SetTrigger("JUMP");
            }
        }
        rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, movingVelocity.z);
    }
}
        /*
        if (Input.GetButtonDown("Jump")){
            if(isGround) animator.SetTrigger("JUMP");
        }
        rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, movingVelocity.z);
    }
    void Jump(){

            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);

    }
}
*/