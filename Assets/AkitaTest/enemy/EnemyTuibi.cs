using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTuibi : MonoBehaviour
{
    private GameObject pl;
    private NavMeshAgent agent;
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
            agent.destination = pl.transform.position;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CrossTrigger")
        {
            agent.destination = pl.transform.position;
        }
    }
}
