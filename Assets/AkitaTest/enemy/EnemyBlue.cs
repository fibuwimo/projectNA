using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBlue: MonoBehaviour
{
    public enum STATE
    {
        JYUNKAI,
        TUIBI,
    }
    STATE state = STATE.JYUNKAI;
    private GameObject pl;
    private NavMeshAgent agent;
    public GameObject[] jyunkaiTarget;
    public int jyunkaiIndex = 0;
    public int jyunkaiTime;
    public int tuibiTime;
    // Start is called before the first frame update
    void Start()
    {
        pl= GameObject.FindWithTag("Player");
        agent= gameObject.GetComponent<NavMeshAgent>();
        agent.destination = pl.transform.position;
        StartCoroutine(chageSTATE());
    }

    // Update is called once per frame
    void Update()
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
                if (Vector3.Distance(pl.transform.position, transform.position) <= 3.5f)
                {
                    agent.destination = pl.transform.position;
                }
                else
                {
                    agent.destination = pl.transform.position + pl.transform.forward * 4f;
                }
            }
            if (state == STATE.JYUNKAI)
            {
                agent.destination = jyunkaiTarget[jyunkaiIndex].transform.position;
            }
            
        }
    }
    IEnumerator chageSTATE()
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
}
