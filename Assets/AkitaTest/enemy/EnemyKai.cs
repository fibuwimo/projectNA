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
    public GameObject[] jyunkaiTarget;
    public int jyunkaiIndex = 0;
    public int taikiTime;
    protected float taikiCount;
    public int jyunkaiTime;
    protected float jyunkaiCount;
    public int tuibiTime;
    protected float tuibiCount;
    protected int runTime = 10;
    protected float runCount;
    protected float tempSpeed;
    protected Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        pl = GameObject.FindWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = pl.transform.position;
        agent.speed = 0;
        tempSpeed = startSpeeds[0];
        StartCoroutine(changeSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.isOnOffMeshLink){
            Debug.Log("ぴんくとぶ");
        }
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
            if (Input.anyKeyDown)
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
                SetDead();
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
            Debug.Log("きんたま踏んだ～");
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
        Debug.Log("敵巡回");
        agent.speed = tempSpeed;
        jyunkaiCount = 0;
        state = STATE.JYUNKAI;
    }
    protected void SetTuibi()
    {
        Debug.Log("敵追尾");
        agent.speed = tempSpeed;
        tuibiCount = 0;
        state = STATE.TUIBI;
    }
    protected void SetRun()
    {
        Debug.Log("敵逃走");
        agent.speed = tempSpeed/2;
        runCount = 0;
        state = STATE.RUN;
    }
    protected void SetTaiki()
    {
        Debug.Log("敵待機");
        taikiCount = 0;
        agent.speed = 0;
        state = STATE.TAIKI;
    }
    protected void SetDead()
    {
        Debug.Log("敵デッド");
        agent.speed = maxSpeeds[stageCount-1]*1.2f;
        state = STATE.DEAD;
    }
    protected void SetFreez()
    {
        Debug.Log("敵フリーズ");
        state = STATE.FREEZ;
    }

    public void runAwake()
    {
        if (state != STATE.RUN)
        {
            SetRun();
        }
    }
    public void Restart()
    {
        agent.speed = 0;
        tempSpeed = startSpeeds[stageCount-1];
        agent.Warp(startPosition);
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
}
