using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuchibokori : MonoBehaviour
{
    public ParticleSystem kemuri;
    public Rigidbody rb;
    bool Grounded;
    Ray ray;
    float distance=0.5f;
    RaycastHit hit;
    Vector3 rayPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        rayPosition=transform.position+new Vector3(0,0.5f,0);
        ray=new Ray(rayPosition,transform.up*-1);
        Debug.DrawRay(ray.origin,ray.direction*distance,Color.red);
        print(Grounded);
        if(Physics.Raycast(ray,out hit,distance)){
            Grounded=true;
        }else{
            Grounded=false;
        }

        if (rb.velocity.magnitude > 0.08f&&Grounded)
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