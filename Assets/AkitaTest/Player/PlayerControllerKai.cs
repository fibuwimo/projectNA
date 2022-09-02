﻿using System.Collections;
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
    public int stageCount = 1;
    public STATE state;
    public int life;
    public Text lifeText;
    public Text mutekiText;
    public Text coinText;
    public Text scoreText;
    public Text comboText;
    int score=0;
    int comboCount=0;
    public float comboBairitu=1.5f;
    public int coinCount;
    public float[] mutekiTimes;
    float mutekiCount = 0;
    float freezWarpTime;
    float freezTime;
    protected float freezCount;
    Vector3 startPosition;

    public GameObject tutiWallMihon;
    public GameObject tutiWallMihonRed;
    public GameObject tutiWall;
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
        
        if (state == STATE.ALIVE)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                Vector3 tPosition = (transform.position + transform.forward * 2f);
                float x = Mathf.Round(tPosition.x / 1.5f);
                float z = Mathf.Round(tPosition.z / 1.5f);
                float y = transform.position.y + 0.5f;
                Vector3 magicWallPosition = new Vector3(x * 1.5f, y, z * 1.5f);
                /* GameObject obj=Instantiate(tutiWallTest, magicWallPosition, Quaternion.Euler(transform.forward));
                 obj.GetComponent<tutiWallTest>().setObj(tutiWallMihon, tutiWall);*/
                Vector3 dir = new Vector3(x * 1.5f, 0f, z * 1.5f) - new Vector3(x * 1.5f, 10, z * 1.5f);
                Ray ray = new Ray(new Vector3(x * 1.5f, 10f, z * 1.5f), dir);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))

                {
                    Debug.Log("レイキャスト通過");
                    if (hit.collider.CompareTag("Wall"))
                    {
                        magicWallPosition.y = hit.point.y + 0.5f;
                        Instantiate(tutiWallMihonRed, magicWallPosition, Quaternion.Euler(transform.forward));
                    }
                    else
                    {
                        magicWallPosition.y = hit.point.y + 0.5f;
                        Instantiate(tutiWallMihon, magicWallPosition, Quaternion.Euler(transform.forward));
                    }

                }



            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                Vector3 tPosition = (transform.position + transform.forward * 2f);
                float x = Mathf.Round(tPosition.x / 1.5f);
                float z = Mathf.Round(tPosition.z / 1.5f);
                float y = transform.position.y + 0.5f;
                Vector3 magicWallPosition = new Vector3(x * 1.5f, y, z * 1.5f);
                Vector3 dir = new Vector3(x * 1.5f, 0f, z * 1.5f) - new Vector3(x * 1.5f, 10, z * 1.5f);
                Ray ray = new Ray(new Vector3(x * 1.5f, 10f, z * 1.5f), dir);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))

                {
                    if (hit.collider.CompareTag("Wall"))
                    {

                    }
                    else
                    {
                        magicWallPosition.y = hit.point.y + 0.5f;
                        Instantiate(tutiWall, magicWallPosition, Quaternion.Euler(transform.forward));
                    }

                }

            }
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
            mutekiCount += Time.deltaTime;

            if (mutekiCount >= mutekiTimes[stageCount - 1])
            {

                SetAlive();
            }
            if (Input.GetKey(KeyCode.Z))
            {
                Vector3 tPosition = (transform.position + transform.forward * 2f);
                float x = Mathf.Round(tPosition.x / 1.5f);
                float z = Mathf.Round(tPosition.z / 1.5f);
                float y = transform.position.y + 0.5f;
                Vector3 magicWallPosition = new Vector3(x * 1.5f, y, z * 1.5f);
                /* GameObject obj=Instantiate(tutiWallTest, magicWallPosition, Quaternion.Euler(transform.forward));
                 obj.GetComponent<tutiWallTest>().setObj(tutiWallMihon, tutiWall);*/
                Vector3 dir = new Vector3(x * 1.5f, 0f, z * 1.5f) - new Vector3(x * 1.5f, 10, z * 1.5f);
                Ray ray = new Ray(new Vector3(x * 1.5f, 10f, z * 1.5f), dir);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))

                {
                    Debug.Log("レイキャスト通過");
                    if (hit.collider.CompareTag("Wall"))
                    {
                        magicWallPosition.y = hit.point.y + 0.5f;
                        Instantiate(tutiWallMihonRed, magicWallPosition, Quaternion.Euler(transform.forward));
                    }
                    else
                    {
                        magicWallPosition.y = hit.point.y + 0.5f;
                        Instantiate(tutiWallMihon, magicWallPosition, Quaternion.Euler(transform.forward));
                    }

                }



            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                Vector3 tPosition = (transform.position + transform.forward * 2f);
                float x = Mathf.Round(tPosition.x / 1.5f);
                float z = Mathf.Round(tPosition.z / 1.5f);
                float y = transform.position.y + 0.5f;
                Vector3 magicWallPosition = new Vector3(x * 1.5f, y, z * 1.5f);
                Vector3 dir = new Vector3(x * 1.5f, 0f, z * 1.5f) - new Vector3(x * 1.5f, 10, z * 1.5f);
                Ray ray = new Ray(new Vector3(x * 1.5f, 10f, z * 1.5f), dir);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))

                {
                    if (hit.collider.CompareTag("Wall"))
                    {

                    }
                    else
                    {
                        magicWallPosition.y = hit.point.y + 0.5f;
                        Instantiate(tutiWall, magicWallPosition, Quaternion.Euler(transform.forward));
                    }

                }

            }
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
        if (state == STATE.DEAD)
        {
            mutekiText.text = "無敵じゃないよ";
        }
        if (state == STATE.FREEZ)
        {
            mutekiText.text = "無敵じゃないよ";
            
            freezCount += Time.deltaTime;
            if (freezCount >= freezWarpTime)
            {
                transform.position = startPosition;
                float z = startPosition.z + 1.0f;
                transform.LookAt(new Vector3(startPosition.x, startPosition.y, z));
                coinText.text = "COIN:" + coinCount;
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
            coinText.text = "COIN:" + coinCount;
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
            Destroy(other.gameObject);
            SetMuteki();
        }

    }
    void FixedUpdate()
    {
        if (state == STATE.ALIVE || state == STATE.MUTEKI)
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
        mutekiText.text = "無敵じゃないよ";
        comboCount = 0;
        comboText.text = "";
        state = STATE.ALIVE;
    }
    void SetMuteki()
    {
        Debug.Log("セット無敵呼ばれた");
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].activeSelf)
            {
                agents[i].GetComponent<EnemyKai>().runAwake(mutekiTimes[stageCount - 1]);
            }
        }
        mutekiText.text = "無敵だよ";
        mutekiCount = 0;
        state = STATE.MUTEKI;
    }
    void SetDead()
    {
        mutekiText.text = "無敵じゃないよ";
        Debug.Log("プレイヤー死亡");
        life -= 1;
        lifeText.text = "LIFE:" + life;
        comboCount = 0;
        comboText.text = "";
        state = STATE.DEAD;
    }
    void SetFreez()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(0, 0, 0);
        //rb.isKinematic = true;
        freezCount = 0;
        comboCount = 0;
        comboText.text ="";
        state = STATE.FREEZ;
    }

    public void Restart(float dTime, float sTime)
    {
       
        freezWarpTime = dTime;
        freezTime = dTime + sTime;
        SetFreez();
    }
    public void Clear(float cTime, float sTime)
    {
       
        freezWarpTime = cTime;
        freezTime = cTime + sTime;
        SetFreez();
    }
    public void Gameover(float gTime, float sTime)
    {
        freezWarpTime = gTime;
        freezTime = gTime + sTime;
        SetFreez();
    }
    public void scoreGain()
    {
        comboCount++;
        score += (int)(500 * Mathf.Pow(2f, comboCount-1));
        comboText.text = "COMBO:" + comboCount;
        scoreText.text = "SCORE:" + score;
    }
}