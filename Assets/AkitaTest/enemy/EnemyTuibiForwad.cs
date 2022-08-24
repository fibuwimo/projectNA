using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTuibiEx: MonoBehaviour
{
    private GameObject pl;
    private NavMeshAgent agent;
    private GameObject agentRed;
    // Start is called before the first frame update
    void Start()
    {
        pl= GameObject.FindWithTag("Player");
        agent= gameObject.GetComponent<NavMeshAgent>();
        agent.destination = pl.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.desiredVelocity.magnitude == 0)
        {
            agent.destination = pl.transform.position+pl.transform.forward*12.0f;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CrossTrigger")
        {
            agent.destination = pl.transform.position + pl.transform.forward * 12.0f;
        }
    }
}
