using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOrange : Enemy
{
    public GameObject targetAgent;

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
                agent.destination = pl.transform.position+(pl.transform.position - targetAgent.transform.position).normalized * 3;
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
                agent.destination = pl.transform.position + (pl.transform.position - targetAgent.transform.position).normalized * 3;
            }
            else if (state == STATE.JYUNKAI)
            {
                agent.destination = jyunkaiTarget[jyunkaiIndex].transform.position;
            }
        }
    }
}
