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
    protected STATE state = STATE.JYUNKAI;
    protected GameObject pl;
    protected NavMeshAgent agent;
    public GameObject[] jyunkaiTarget;
    public int jyunkaiIndex = 0;
    public int jyunkaiTime;
    public int tuibiTime;
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
            if (state == STATE.TUIBI)
            {
                agent.destination = pl.transform.position;
            }
            if (state == STATE.JYUNKAI)
            {
                agent.destination = jyunkaiTarget[jyunkaiIndex].transform.position;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < jyunkaiTarget.Length; i++)
        {
            if (other.gameObject == jyunkaiTarget[i])
            {

                jyunkaiIndex = (i + 1) % jyunkaiTarget.Length;
            }
        }

        if (other.gameObject.tag == "CrossTrigger")
        {
            if (state == STATE.TUIBI)
            {
                agent.destination = pl.transform.position;
            }
            if (state == STATE.JYUNKAI)
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
            Debug.Log("追尾になる");
            state = STATE.TUIBI;
            yield return new WaitForSeconds(tuibiTime);
            Debug.Log("巡回になる");
            state = STATE.JYUNKAI;
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
}

