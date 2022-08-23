using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class jyunkaiTuisekiEnemy : MonoBehaviour
{
    public GameObject[] jyunkaiTuisekiTarget;
    public int jyunkaiTuisekiIndex = 0;
    public Transform target;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((transform.position - target.position).magnitude < 10)
        {
            GetComponent<NavMeshAgent>().destination = target.position;
        }
        else
        {
            GetComponent<NavMeshAgent>().destination = jyunkaiTuisekiTarget[jyunkaiTuisekiIndex].transform.position;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < jyunkaiTuisekiTarget.Length; i++)
        {
            if (other.gameObject == jyunkaiTuisekiTarget[i])
            {
                jyunkaiTuisekiIndex =(i + 1)%jyunkaiTuisekiTarget.Length;
            }
        }
    }
}
