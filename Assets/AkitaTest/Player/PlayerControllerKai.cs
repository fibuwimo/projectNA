using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerKai : MonoBehaviour
{
    public static int resultScore;
    public AudioClip bgmMuteki;
    public AudioClip bgmClear;
    public AudioClip bgmMain;
    public AudioClip bgmDead;
    public AudioClip soundCoin;
    public AudioClip soundMutekiItem;
    public AudioClip soundJump;
    public AudioClip soundDead;
    AudioSource audioSource;
    
    public AudioSource audioSourceBGM;
    public AudioSource audioSourceJumpSe;
    public AudioSource audioSourceCoinSe;
    public AudioSource audioSourcePlayerDeadSe;

    public float bgmMainVol=0.5f;
    public float bgmMutekiVol = 0.5f;
    public float bgmClearVol = 0.5f;
    public float bgmDeadVol = 0.5f;

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
    bool isGround;
    bool jumpAble = true;

    Animator animator;
    GameObject child;
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
    public GameObject canvas;
    public Text stageText;
    public Text stageText2;
    public Text lifeText;
    public Text lifeText2;
    public Text mutekiText;
    public Text coinText;
    public Text scoreText;
    public Text tutimahouText;
    public Text rumbaText;
    public Text comboTextMuteki;
    public Image HidariueImage;
    public Image anten;
    public int tutimahouCountMax = 2;
    int tutimahouCount;
    public int rumbaCountMax=1;
    public int rumbaAddStageCount=3;
    int rumbaCount;
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

    public GameObject rumbaMihon;
    public GameObject rumbaMihonRed;
    public GameObject rumba;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _transform = transform;
        prevPosition = _transform.position;
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        state = STATE.FREEZ;
        life = 3;
        child = transform.GetChild(0).gameObject;
        animator = child.GetComponent<Animator>();
        tutimahouCount = tutimahouCountMax;
        rumbaCount = rumbaCountMax;
        tutimahouText.text = "" + tutimahouCount;
        rumbaText.text = "" + rumbaCount;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state == STATE.ALIVE)
        {
            if (tutimahouCount > 0)
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
                        if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Slope"))
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
                        if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Slope"))
                        {

                        }
                        else
                        {
                            Debug.Log(hit.collider.gameObject.name);
                            magicWallPosition.y = hit.point.y + 0.5f;
                            Instantiate(tutiWall, magicWallPosition, Quaternion.Euler(transform.forward));
                            tutimahouCount--;
                            tutimahouText.text = "" + tutimahouCount;
                        }

                    }

                }
            }
            if (rumbaCount > 0)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    Vector3 tPosition = (transform.position + transform.forward * 2f);
                    float x = Mathf.Round(tPosition.x / 1.5f);
                    float z = Mathf.Round(tPosition.z / 1.5f);
                    float y = transform.position.y + 0.5f;
                    Vector3 rumbaPosition = new Vector3(x * 1.5f, y, z * 1.5f);
                    Vector3 pPosition = new Vector3((Mathf.Round(transform.position.x / 1.5f) * 1.5f), y, (Mathf.Round(transform.position.z / 1.5f)) * 1.5f);
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
                            rumbaPosition.y = hit.point.y + 0.2f;
                            Instantiate(rumbaMihonRed, rumbaPosition, Quaternion.Euler(transform.forward));
                        }
                        else
                        {
                            rumbaPosition.y = hit.point.y + 0.2f;
                            Instantiate(rumbaMihon, rumbaPosition, Quaternion.Euler(transform.forward));
                        }

                    }



                }
                if (Input.GetKeyUp(KeyCode.X))
                {
                    Vector3 tPosition = (transform.position + transform.forward * 2f);
                    float x = Mathf.Round(tPosition.x / 1.5f);
                    float z = Mathf.Round(tPosition.z / 1.5f);
                    float y = transform.position.y + 0.5f;
                    Vector3 rumbaPosition = new Vector3(x * 1.5f, y, z * 1.5f);
                    Vector3 pPosition = new Vector3((Mathf.Round(transform.position.x / 1.5f) * 1.5f), y, (Mathf.Round(transform.position.z / 1.5f)) * 1.5f);
                    Vector3 rumbaDirection = (rumbaPosition - pPosition).normalized;
                    Quaternion requiredRotation = Quaternion.FromToRotation(new Vector3(0, 0, 1.0f), rumbaDirection);
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
                            Debug.Log(rumbaDirection);
                            rumbaPosition.y = hit.point.y + 0.2f;
                            Instantiate(rumba, rumbaPosition, requiredRotation);
                            rumbaCount--;
                            rumbaText.text = "" + rumbaCount;
                        }

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

            if (movingVelocity.x == 0 && movingVelocity.z == 0)
            {
                animator.SetBool("RUN", false);
            }
            else
            {
                if (isGround) animator.SetBool("RUN", true);
            }

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
            if (mutekiCount >= mutekiTimes[stageCount - 1]-3)
            {
                audioSourceBGM.volume -= 0.003f;
            }

            if (mutekiCount >= mutekiTimes[stageCount - 1])
            {

                SetAlive();
            }
            if (tutimahouCount > 0)
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
                        if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Slope"))
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
                        if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Slope"))
                        {

                        }
                        else
                        {
                            magicWallPosition.y = hit.point.y + 0.5f;
                            Instantiate(tutiWall, magicWallPosition, Quaternion.Euler(transform.forward));
                            tutimahouCount--;
                            tutimahouText.text = "" + tutimahouCount;
                        }

                    }

                }
            }
            if (rumbaCount > 0)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    Vector3 tPosition = (transform.position + transform.forward * 2f);
                    float x = Mathf.Round(tPosition.x / 1.5f);
                    float z = Mathf.Round(tPosition.z / 1.5f);
                    float y = transform.position.y + 0.5f;
                    Vector3 rumbaPosition = new Vector3(x * 1.5f, y, z * 1.5f);
                    Vector3 pPosition = new Vector3((Mathf.Round(transform.position.x / 1.5f) * 1.5f), y, (Mathf.Round(transform.position.z / 1.5f)) * 1.5f);
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
                            rumbaPosition.y = hit.point.y + 0.2f;
                            Instantiate(rumbaMihonRed, rumbaPosition, Quaternion.Euler(transform.forward));
                        }
                        else
                        {
                            rumbaPosition.y = hit.point.y + 0.2f;
                            Instantiate(rumbaMihon, rumbaPosition, Quaternion.Euler(transform.forward));
                        }

                    }



                }
                if (Input.GetKeyUp(KeyCode.X))
                {
                    Vector3 tPosition = (transform.position + transform.forward * 2f);
                    float x = Mathf.Round(tPosition.x / 1.5f);
                    float z = Mathf.Round(tPosition.z / 1.5f);
                    float y = transform.position.y + 0.5f;
                    Vector3 rumbaPosition = new Vector3(x * 1.5f, y, z * 1.5f);
                    Vector3 pPosition = new Vector3((Mathf.Round(transform.position.x / 1.5f) * 1.5f), y, (Mathf.Round(transform.position.z / 1.5f)) * 1.5f);
                    Vector3 rumbaDirection = (rumbaPosition - pPosition).normalized;
                    Quaternion requiredRotation = Quaternion.FromToRotation(new Vector3(0, 0, 1.0f), rumbaDirection);
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
                            Debug.Log(rumbaDirection);
                            rumbaPosition.y = hit.point.y + 0.2f;
                            Instantiate(rumba, rumbaPosition, requiredRotation);
                            rumbaCount--;
                            rumbaText.text = "" + rumbaCount;
                        }

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

            if (movingVelocity.x == 0 && movingVelocity.z == 0)
            {
                animator.SetBool("RUN", false);
            }
            else
            {
                if (isGround) animator.SetBool("RUN", true);
            }

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

            if (isGround)
            {
                rb.isKinematic = true;
            }

            freezCount += Time.deltaTime;
            if (freezCount >= freezWarpTime)
            {
                audioSourceBGM.volume -= 0.01f;
                transform.position = startPosition;
                float z = startPosition.z + 1.0f;
                transform.LookAt(new Vector3(startPosition.x, startPosition.y, z));
                coinText.text = "COIN:" + coinCount;
                animator.SetBool("DAMAGE", false);
                animator.SetBool("RUN", false);
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
           
            Destroy(other.gameObject);
            CoinScoreGain();
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
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);
        isGround = Physics.Raycast(ray, distance);
        if (state == STATE.ALIVE || state == STATE.MUTEKI)
        {
            //float jump = movingVelocity.y;
            Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
            Debug.Log(isGround);
            if (Input.GetButtonDown("Jump"))
            {
                if (isGround && jumpAble)
                {
                    rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
                    audioSourceJumpSe.PlayOneShot(soundJump);
                    StartCoroutine(SetJumpAble());
                    animator.SetTrigger("JUMP");
                }
            }
            rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, movingVelocity.z);
        }
    }
    void SetAlive()
    {
        audioSourceBGM.volume = bgmMainVol;
        audioSourceBGM.Stop();
        audioSourceBGM.PlayOneShot(bgmMain);
        rb.isKinematic = false;
        mutekiText.text = "無敵じゃないよ";
        comboCount = 0;
        state = STATE.ALIVE;
    }
    void SetMuteki()
    {
        audioSourceBGM.Stop();
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
        audioSourceBGM.volume=bgmMutekiVol;
        audioSource.PlayOneShot(soundMutekiItem);
        audioSourceBGM.PlayOneShot(bgmMuteki);
        state = STATE.MUTEKI;
    }
    void SetDead()
    {
        audioSourcePlayerDeadSe.PlayOneShot(soundDead);
        audioSourceBGM.Stop();
        audioSourceBGM.volume = bgmDeadVol;
        audioSourceBGM.PlayOneShot(bgmDead);
        mutekiText.text = "無敵じゃないよ";
        Debug.Log("プレイヤー死亡");
        life -= 1;
        rumbaCount++;
        if (rumbaCount > rumbaCountMax) rumbaCount = 1;
        tutimahouCount = tutimahouCountMax;
        rumbaText.text = "" + rumbaCount;
        lifeText.text = "LIFE:" + life;
        lifeText2.text = "" + life;
        comboCount = 0;
        animator.SetBool("DAMAGE", true);
        state = STATE.DEAD;
    }
    void SetFreez()
    {
        
        animator.SetBool("RUN", false);
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(0, 0, 0);
        //rb.isKinematic = true;
        freezCount = 0;
        comboCount = 0;
        state = STATE.FREEZ;
    }

    public void Restart(float dTime, float sTime)
    {
        freezWarpTime = dTime;
        freezTime = dTime + sTime;
        SetFreez();
        StartCoroutine(setAntenByDead());
    }
    public void Init(float dTime, float sTime)
    {
        freezWarpTime = dTime;
        freezTime = dTime + sTime;
        freezCount = 0;
        state = STATE.FREEZ;
        StartCoroutine(setAntenByInit());
    }
    public void Clear(float cTime, float sTime)
    {
        tutimahouCount++;
        if ((stageCount) % rumbaAddStageCount == 1)
        {
            rumbaCount++;
            if (rumbaCount > rumbaCountMax) rumbaCount = 1;
            rumbaText.text = "" + rumbaCount;
        }
        if (tutimahouCount > tutimahouCountMax) tutimahouCount = 2;
        tutimahouText.text = "" + tutimahouCount;
        freezWarpTime = cTime;
        freezTime = cTime + sTime;
        audioSourceBGM.Stop();
        audioSourceBGM.volume = bgmClearVol;
        audioSourceBGM.PlayOneShot(bgmClear);
        SetFreez();
        StartCoroutine(setAntenByClear());
    }
    public void Gameover(float gTime, float sTime)
    {
        freezWarpTime = gTime;
        freezTime = gTime + sTime;
        resultScore = score;
        SetFreez();
    }
    public void scoreGain()
    {
        comboCount++;
        score += (int)(500 * Mathf.Pow(2f, comboCount-1));
        //comboText.text = "+" + (int)(500 * Mathf.Pow(2f, comboCount - 1));
        Text comboTextM = Instantiate(comboTextMuteki);
        comboTextM.transform.SetParent(canvas.transform, false);
        comboTextM.text= "+" + (int)(500 * Mathf.Pow(2f, comboCount - 1));
        scoreText.text = "" + score.ToString("0000000");
    }
    public void RumbaScoreGain(int s)
    {
        score += s;
        scoreText.text = "" + score.ToString("0000000");
    }
    IEnumerator SetJumpAble()
    {
        jumpAble = false;
        yield return new WaitForSeconds(0.2f);
        jumpAble = true;

    }
    public static int getResultScore()
    {
        return resultScore;
    }
    public void CoinScoreGain()
    {
        coinCount++;
        coinText.text = "COIN:" + coinCount;
        score += 30;
        scoreText.text = "" + score.ToString("0000000");
        audioSourceCoinSe.PlayOneShot(soundCoin);
    }
    IEnumerator setAntenByDead()
    {
        scoreText.gameObject.SetActive(false);
        tutimahouText.gameObject.SetActive(false);
        rumbaText.gameObject.SetActive(false);
        HidariueImage.gameObject.SetActive(false);
        //stageText2.gameObject.SetActive(false);
        lifeText2.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        anten.gameObject.SetActive(true);
        lifeText.gameObject.SetActive(true);
        stageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        anten.gameObject.SetActive(false);
        lifeText.gameObject.SetActive(false);
        stageText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        tutimahouText.gameObject.SetActive(true);
        rumbaText.gameObject.SetActive(true);
        //stageText2.gameObject.SetActive(true);
        lifeText2.gameObject.SetActive(true);
        HidariueImage.gameObject.SetActive(true);

    }
    IEnumerator setAntenByInit()
    {
        scoreText.gameObject.SetActive(false);
        tutimahouText.gameObject.SetActive(false);
        rumbaText.gameObject.SetActive(false);
        HidariueImage.gameObject.SetActive(false);
        //stageText2.gameObject.SetActive(false);
        //lifeText2.gameObject.SetActive(false);
        yield return new WaitForSeconds(0f);
        anten.gameObject.SetActive(true);
        lifeText.gameObject.SetActive(true);
        stageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        anten.gameObject.SetActive(false);
        lifeText.gameObject.SetActive(false);
        stageText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        tutimahouText.gameObject.SetActive(true);
        rumbaText.gameObject.SetActive(true);
        //stageText2.gameObject.SetActive(true);
        lifeText2.gameObject.SetActive(true);
        HidariueImage.gameObject.SetActive(true);

    }
    IEnumerator setAntenByClear()
    {
        scoreText.gameObject.SetActive(false);
        tutimahouText.gameObject.SetActive(false);
        rumbaText.gameObject.SetActive(false);
        HidariueImage.gameObject.SetActive(false);
        //stageText2.gameObject.SetActive(false);
        lifeText2.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        stageText.text = "STAGE" + stageCount;
        stageText2.text = "STAGE" + stageCount;
        anten.gameObject.SetActive(true);
        lifeText.gameObject.SetActive(true);
        stageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        anten.gameObject.SetActive(false);
        lifeText.gameObject.SetActive(false);
        stageText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        tutimahouText.gameObject.SetActive(true);
        rumbaText.gameObject.SetActive(true);
        //stageText2.gameObject.SetActive(true);
        lifeText2.gameObject.SetActive(true);
        HidariueImage.gameObject.SetActive(true);

    }
}