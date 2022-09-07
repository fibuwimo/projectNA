using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKai : MonoBehaviour
{
    public enum STATE
    {
        JYUNKAI,
        TUIBI,
        RUN,
        TAIKI,
        DEAD,
        FREEZ,
    }
    
    public int stageCount=1;
    public float[] startSpeeds;
    public float[] maxSpeeds;
    public float speedStep;
    public float speedStepTime;
    public STATE state = STATE.FREEZ;
    protected GameObject pl;
    protected NavMeshAgent agent;
    protected Rigidbody agentRigidbody;
    public GameObject[] jyunkaiTarget;
    public int jyunkaiIndex = 0;
    public float taikiTime;
    protected float taikiCount;
    public float jyunkaiTime;
    protected float jyunkaiCount;
    public float tuibiTime;
    protected float tuibiCount;
    protected float freezWarpTime;
    protected float freezTime;
    protected float freezCount;
    protected int tenmetuCount;
    protected float runTime = 10;
    protected float runCount;
    protected float tempSpeed;
    public float runSpeedBairitu;
    public float deadSpeedBairitu;
    protected Vector3 startPosition;
    protected GameObject child;
    protected GameObject mago;
    public Material firstColor;
    public Material runColor;
    public Material tenmetuColor;
    public Material deadColor;
    protected PlayerControllerKai plCon;
    [SerializeField, Range(0F, 90F), Tooltip("射出する角度")]
    private float upAngle;
    [SerializeField, Range(0F, 90F), Tooltip("射出する角度")]
    private float dropAngle;
    protected Animator animator;
    protected Renderer agentRendere;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        pl = GameObject.FindWithTag("Player");
        plCon = pl.GetComponent<PlayerControllerKai>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agentRigidbody = agent.GetComponent<Rigidbody>();
        agent.destination = pl.transform.position;
        agent.speed = 0;
        tempSpeed = startSpeeds[stageCount - 1];
        StartCoroutine(changeSpeed());
        if (transform.childCount>0)
        {
            child = transform.GetChild(0).gameObject;
            mago = transform.GetChild(0).transform.GetChild(1).gameObject;
            agentRendere = mago.GetComponent<Renderer>();
        }
        animator = child.GetComponent<Animator>();
        StartCoroutine(MoveNormalSpeed(agent));
    }

    // Update is called once per frame
    void Update()
    {
        if (state == STATE.JYUNKAI)
        {
            agent.speed = tempSpeed;
            if (agent.desiredVelocity.magnitude == 0f)
            {
                agent.destination = jyunkaiTarget[jyunkaiIndex].transform.position;
            }
            jyunkaiCount += Time.deltaTime;
            if (jyunkaiCount >= jyunkaiTime)
            {
                SetTuibi();
            }

        }
        if (state == STATE.TUIBI)
        {
            agent.speed = tempSpeed;
            if (agent.desiredVelocity.magnitude == 0f)
            {
                agent.destination = pl.transform.position;
            }
            tuibiCount += Time.deltaTime;
            if (tuibiCount >= tuibiTime)
            {
                SetJyunkai();
            }

        }
        if (state == STATE.RUN)
        {
            runCount += Time.deltaTime;
            tenmetuCount++;
            if (runCount >= runTime - 1.5f)
            {
                if (tenmetuCount % 4 == 0 && tenmetuCount % 8 != 0)
                {
                    agentRendere.material = runColor;
                }
                else if (tenmetuCount % 4 == 0)
                {
                    agentRendere.material = tenmetuColor;
                }
            }
            else if (runCount >= runTime - 3.0f)
            {
                if (tenmetuCount % 16 == 0 && tenmetuCount % 32 !=0)
                {
                    agentRendere.material =runColor;
                }
                else if (tenmetuCount % 16 == 0)
                {
                    agentRendere.material = tenmetuColor;
                }
            }
            
            if (runCount >= runTime)
            {
                SetTuibi();
            }
            if (agent.desiredVelocity.magnitude == 0f)
            {
                Vector3 runPosition = transform.position + (transform.position - pl.transform.position).normalized * 3;
                runPosition.y = 0;
                agent.destination = runPosition;
            }

        }
        if (state == STATE.TAIKI)
        {

            taikiCount += Time.deltaTime;
            if (taikiCount >= taikiTime)
            {
                SetJyunkai();
            }

        }
        if (state == STATE.DEAD)
        {
            agent.speed = maxSpeeds[stageCount-1] * 1.5f;
            if (agent.desiredVelocity.magnitude == 0f)
            {
                agent.destination = startPosition;
            }
            if (Vector3.Distance(transform.position, startPosition) <= 0.5f)
            {
                SetTuibi();
            }

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
                agent.Warp(startPosition);
            }
            if (freezCount >= freezTime)
            {
                SetTaiki();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (state == STATE.RUN)
            {
                Debug.Log("デッドするよ");

                deadByMuteki();
            }
        }
        for (int i = 0; i < jyunkaiTarget.Length; i++)
        {
            if (other.gameObject == jyunkaiTarget[i])
            {
                jyunkaiIndex = (i + 1) % jyunkaiTarget.Length;
            }
        }

        if (other.gameObject.tag == "CrossTrigger")
        {
            if (state == STATE.RUN)
            {
                Vector3 runPosition = transform.position + (transform.position - pl.transform.position).normalized * 3;
                runPosition.y = 0;
                agent.destination = runPosition;
            }
            else if (state == STATE.TUIBI)
            {
                agent.destination = pl.transform.position;
            }
            else if (state == STATE.JYUNKAI)
            {
                agent.destination = jyunkaiTarget[jyunkaiIndex].transform.position;
            }
            else if (state == STATE.DEAD)
            {
                agent.destination = startPosition;
            }
        }
    }

    protected void SetJyunkai()
    {
        animator.SetBool("TUIBI", true);
        animator.SetBool("RUN", false);
        animator.SetBool("FREEZ", false);
        animator.SetBool("DEAD", false);
        //animator.SetBool("TUIBI", true);
        Debug.Log("敵巡回");
        if (firstColor != null)
        {
            mago.GetComponent<Renderer>().material = firstColor;
        }
        agent.speed = tempSpeed;
        jyunkaiCount = 0;
        state = STATE.JYUNKAI;
    }
    protected void SetTuibi()
    {
        animator.SetBool("TUIBI", true);
        animator.SetBool("RUN", false);
        animator.SetBool("FREEZ", false);
        animator.SetBool("DEAD", false);
        //animator.SetBool("TUIBI", true);
        Debug.Log("敵追尾");
        if (firstColor != null)
        {
            mago.GetComponent<Renderer>().material = firstColor;
        }
        agent.speed = tempSpeed;
        tuibiCount = 0;
        state = STATE.TUIBI;
    }
    protected void SetRun()
    {
        animator.SetBool("TUIBI",false);
        animator.SetBool("RUN", true);
        animator.SetBool("FREEZ", false);
        animator.SetBool("DEAD", false);
        //animator.SetBool("TUIBI", true);
        Debug.Log("敵逃走");
        if (runColor != null)
        {
            mago.GetComponent<Renderer>().material = runColor;
        }
        agent.speed = tempSpeed*runSpeedBairitu;
        runCount = 0;
        tenmetuCount = 0;
        state = STATE.RUN;
    }
    protected void SetTaiki()
    {
        animator.SetBool("RUN", false);
        animator.SetBool("DEAD", false);
        animator.SetBool("TUIBI", false);
        animator.SetBool("FREEZ", true);
        if (firstColor != null)
        {
            mago.GetComponent<Renderer>().material = firstColor;
        }
        Debug.Log("敵待機");
        taikiCount = 0;
        agent.speed = 0;
        state = STATE.TAIKI;
    }
    protected void SetDead()
    {
        StartCoroutine(TimeStop());
        animator.SetBool("DEAD", true);
        animator.SetBool("TUIBI", false);
        animator.SetBool("FREEZ", false);
        animator.SetBool("RUN", false);
        if (deadColor != null)
        {
            mago.GetComponent<Renderer>().material = deadColor;
        }
        Debug.Log("敵デッド");
        agent.speed = maxSpeeds[stageCount-1]*deadSpeedBairitu;
        state = STATE.DEAD;
    }
    protected void SetFreez()
    {
        animator.SetBool("DEAD", false);
        animator.SetBool("TUIBI", false);
        animator.SetBool("FREEZ", true);
        animator.SetBool("RUN", false);
        if (firstColor != null)
        {
            mago.GetComponent<Renderer>().material = firstColor;
        }
        Debug.Log("敵フリーズ");
        freezCount = 0;
        state = STATE.FREEZ;
    }

    public void runAwake(float mTime)
    {
        runTime = mTime;
        SetRun();
    }
    public void deadByMuteki()
    {
        plCon.scoreGain();
        SetDead();
    }
    public void deadByRumba()
    {
        SetDead();
    }
    public void Restart(float dTime,float sTime)
    {
        agent.speed = 0;
        tempSpeed = startSpeeds[stageCount-1];
        freezWarpTime = dTime;
        freezTime = dTime + sTime;
        SetFreez();

    }
    public void Clear(float cTime,float sTime)
    {

        agent.speed = 0;
        tempSpeed = startSpeeds[stageCount - 1];
        freezWarpTime = cTime;
        freezTime = cTime + sTime;
        SetFreez();

    }
    public void Gameover(float gTime, float sTime)
    {
        agent.speed = 0;
        tempSpeed = startSpeeds[stageCount - 1];
        freezWarpTime = gTime+1;
        freezTime = gTime+1 + sTime;
        SetFreez();

    }
    IEnumerator changeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedStepTime);
            tempSpeed += speedStep;
            if (tempSpeed >= maxSpeeds[stageCount-1])
            {
                tempSpeed = maxSpeeds[stageCount-1];
            }
        }
    }
    IEnumerator MoveNormalSpeed(NavMeshAgent agent)
    {
        agent.autoTraverseOffMeshLink = false; // OffMeshLinkによる移動を禁止
        var agentRigidbody = agent.GetComponent<Rigidbody>();

        while (true)
        {
            // OffmeshLinkに乗るまで普通に移動
            yield return new WaitWhile(() => agent.isOnOffMeshLink == false);
            animator.SetTrigger("JUMP");

            // OffMeshLinkに乗ったので、NavmeshAgentによる移動を止めて、
            // OffMeshLinkの終わりまでNavmeshAgent.speedと同じ速度で移動

            agent.Stop();
            agentRigidbody.isKinematic = false;

            float rad;
            Vector3 pointA = transform.position;
            Vector3 pointB = new Vector3(agent.currentOffMeshLinkData.endPos.x, agent.currentOffMeshLinkData.endPos.y + 0.5f, agent.currentOffMeshLinkData.endPos.z);
            if (agent.currentOffMeshLinkData.linkType == UnityEngine.AI.OffMeshLinkType.LinkTypeDropDown)
            {
                rad = dropAngle * Mathf.PI / 180;
            }
            else
            {
                rad = upAngle * Mathf.PI / 180;
            }
            float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));
            float y = pointA.y - pointB.y;
            float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

            Vector3 velocity = new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed;

            agentRigidbody.AddForce(velocity * agentRigidbody.mass, ForceMode.Impulse);

            yield return new WaitWhile(() =>
            {
                return Vector3.Distance(transform.localPosition,pointB) > 0.4f;
                //return Mathf.Abs(transform.localPosition.y-pointB.y) > 0.1f;

            });
            animator.ResetTrigger("JUMP");


            // NavmeshAgentを到達した事にして、Navmeshを再開
            agent.CompleteOffMeshLink();
            agent.Resume();
            agentRigidbody.isKinematic = true;
        }
    }
    IEnumerator TimeStop()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.05f);
        Time.timeScale = 1f;
    }
}
