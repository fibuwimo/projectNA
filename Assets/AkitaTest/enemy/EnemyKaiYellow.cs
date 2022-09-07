using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKaiYellow : EnemyKai
{
    public GameObject targetAgent;
    // Start is called before the first frame update


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
                agent.destination = pl.transform.position + (pl.transform.position - targetAgent.transform.position).normalized * 3;
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
            agentRendere.material = runColor;
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
                if (tenmetuCount % 8 == 0 && tenmetuCount % 16 != 0)
                {
                    agentRendere.material = runColor;
                }
                else if (tenmetuCount % 8 == 0)
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
            agent.speed = maxSpeeds[0] * 1.5f;
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
                agent.destination = pl.transform.position + (pl.transform.position - targetAgent.transform.position).normalized * 3;
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
}
