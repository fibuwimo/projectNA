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
    }
    public float[] startSpeeds;
    public float[] maxSpeeds;
    public float speedStep;
    public STATE state = STATE.JYUNKAI;
    protected GameObject pl;
    protected NavMeshAgent agent;
    public GameObject[] jyunkaiTarget;
    public int jyunkaiIndex = 0;
    public int jyunkaiTime;
    public int tuibiTime;
    float tempSpeed;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = pl.transform.position;
        StartCoroutine(changeSTATE());
        StartCoroutine(changeSpeed());
        agent.speed = startSpeeds[0];
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
        transform.position = new Vector3(0, 0.5f, 7.5f);
        agent.speed = startSpeeds[0];
        state = STATE.JYUNKAI;
    }
}

