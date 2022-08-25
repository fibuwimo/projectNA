using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBlue: Enemy
{

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
     
}
