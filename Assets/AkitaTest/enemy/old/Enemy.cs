using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public enum STATE
    {
        JYUNKAI,
        TUIBI,
        RUN,
        TAIKI,
    }
    public float[] startSpeeds;
    public float[] maxSpeeds;
    public float speedStep;
    public STATE state = STATE.JYUNKAI;
    protected GameObject pl;
    protected NavMeshAgent agent;
    public GameObject[] jyunkaiTarget;
    public float taikiTime;
    public int jyunkaiIndex = 0;
    public int jyunkaiTime;
    public int tuibiTime;
    float tempSpeed;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        pl = GameObject.FindWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine(Init());
        state = STATE.TAIKI;
    }

    IEnumerator Init()
    {
        StopCoroutine(changeSTATE());
        StopCoroutine(changeSpeed());
        agent.speed = 0;
        yield return new WaitForSeconds(taikiTime);
        state = STATE.JYUNKAI;
        agent.destination = pl.transform.position;
        agent.speed = startSpeeds[0];
        StartCoroutine(changeSTATE());
        StartCoroutine(changeSpeed());
    }

    // Update is called once per frame
    void Update()
    {
       
        if (agent.desiredVelocity.magnitude == 0f)
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
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (state == STATE.RUN)
            {
                Restart();
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
        }
    }
    IEnumerator changeSTATE()
    {
        while (true)
        {
            yield return new WaitForSeconds(jyunkaiTime);
            if (state == STATE.JYUNKAI)
            {
                state = STATE.TUIBI;
            }
            yield return new WaitForSeconds(tuibiTime);
            if (state == STATE.TUIBI)
            {
                state = STATE.JYUNKAI;
            }
        }
    }
    IEnumerator Run()
    {
        state = STATE.RUN;
        tempSpeed = agent.speed;
        agent.speed =tempSpeed/2;
        yield return new WaitForSeconds(10);
        if (state == STATE.RUN)
        {
            agent.speed = tempSpeed;
            state = STATE.JYUNKAI;
        }
    }
    public void runAwake()
    {
        if (state != STATE.RUN)
        {
            StartCoroutine("Run");
        }
    }
    IEnumerator changeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            agent.speed += speedStep;
            if (agent.speed >= maxSpeeds[0])
            {
                agent.speed = maxSpeeds[0];
            }
        }
    }
    public void Restart()
    {
        StopCoroutine(Init());
        agent.speed = 0;
        transform.position = new Vector3(0, 0, 10);
        state = STATE.TAIKI;
        StartCoroutine(Init());
    }
}

