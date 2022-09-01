using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerKai : MonoBehaviour
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
    public enum STATE
    {
        ALIVE,
        MUTEKI,
        DEAD,
        FREEZ,
    }
    public GameObject[] agents;
    public STATE state;
    public int life;
    public Text lifeText;
    public Text mutekiText;
    public Text coinText;
    public int coinCount;
    int mutekiTime = 10;
    float mutekiCount = 0;
    int freezWarpTime;
    int freezTime;
    protected float freezCount;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        prevPosition = _transform.position;
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        state = STATE.ALIVE;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity);
        if (state == STATE.ALIVE)
        {
            var position = transform.position;
            var delta = position - prevPosition;
            //isJumpPressed = Input.GetButtonDown("Jump");
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(moveX, 0, moveZ);
            direction.Normalize();
            movingVelocity = direction * speed;
            movingVelocity.y = position.y;

            if (delta == Vector3.zero) return;
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
        if (state == STATE.MUTEKI)
        {
            var position = transform.position;
            var delta = position - prevPosition;
            //isJumpPressed = Input.GetButtonDown("Jump");
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(moveX, 0, moveZ);
            direction.Normalize();
            movingVelocity = direction * speed;
            movingVelocity.y = position.y;

            if (delta == Vector3.zero) return;
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
            mutekiCount += Time.deltaTime;
            if (mutekiCount >= mutekiTime)
            {
                mutekiText.text = "無敵じゃないよ";
                SetAlive();
            }

        }
        if (state == STATE.DEAD)
        {

        }
        if (state == STATE.FREEZ)
        {
            /*if (Input.anyKeyDown)
            {
                SetTaiki();
            }
            */
            freezCount += Time.deltaTime;
            if (freezCount >= freezWarpTime)
            {
                transform.position = startPosition;
                float z = startPosition.z + 1.0f;
                transform.LookAt(new Vector3(startPosition.x, startPosition.y, z));
            }
            if (freezCount >= freezTime)
            {
                SetAlive();
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Coin"))
        {
            coinCount++;
            coinText.text = "コイン:" + coinCount;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag.Contains("agent"))
        {
            EnemyKai eneScr = other.gameObject.GetComponent<EnemyKai>();
            if (eneScr.state != EnemyKai.STATE.DEAD && eneScr.state != EnemyKai.STATE.RUN && (state==STATE.ALIVE || state==STATE.MUTEKI))
            {
                SetDead();
            }
        }
        if (other.gameObject.tag == ("mutekiItem"))
        {
            if (state == STATE.ALIVE)
            {
                SetMuteki();

                for (int i = 0; i < agents.Length; i++)
                {
                    agents[i].GetComponent<EnemyKai>().runAwake();
                }
                Destroy(other.gameObject);
            }
        }

    }
    void FixedUpdate()
    {
        if (state == STATE.ALIVE || state == STATE.MUTEKI) ;
        {
            //float jump = movingVelocity.y;
            Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
            Ray ray = new Ray(rayPosition, Vector3.down);
            bool isGround = Physics.Raycast(ray, distance);
            Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
            Debug.Log(isGround);
            if (Input.GetButtonDown("Jump"))
            {
                if (isGround) rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            }
            rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, movingVelocity.z);
        }
    }
    void SetAlive()
    {
        rb.isKinematic = false;
        state = STATE.ALIVE;
    }
    void SetMuteki()
    {
        mutekiText.text = "無敵だよ";
        mutekiCount = 0;
        state = STATE.MUTEKI;
    }
    void SetDead()
    {
        rb.velocity = Vector3.zero;
        mutekiText.text = "無敵じゃないよ";
        Debug.Log("プレイヤー死亡");
        life -= 1;
        lifeText.text = "LIFE:" + life;
        state = STATE.DEAD;
    }
    void SetFreez()
    {
        rb.isKinematic = true;
        freezCount = 0;
        state = STATE.FREEZ;
    }

    public void Restart(int dTime, int sTime)
    {
       
        freezWarpTime = dTime;
        freezTime = dTime + sTime;
        coinText.text = "コイン:" + coinCount;
        SetFreez();
    }
    public void Clear(int cTime, int sTime)
    {
       
        freezWarpTime = cTime;
        freezTime = cTime + sTime;
        coinText.text = "コイン:" + coinCount;
        SetFreez();
    }
    public void Gameover(int gTime, int sTime)
    {
        freezWarpTime = gTime;
        freezTime = gTime + sTime;
        coinText.text = "コイン:" + coinCount;
        SetFreez();
    }
}