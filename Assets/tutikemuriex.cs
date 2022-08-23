using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutikemuriex : MonoBehaviour
{
    public ParticleSystem kemuri;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            Debug.Log("けむりでてる");
            Instantiate(
                kemuri,
                transform.position,
                Quaternion.identity);
        }
        else
        {
        }
            
    }
}
