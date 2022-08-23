using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutiWallTest : MonoBehaviour
{
    GameObject tutiWallMihon;
    GameObject tutiWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(tutiWallMihon,transform.position, Quaternion.Euler(transform.forward));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("壊れるよ");
            Destroy(this.gameObject);
        }
    }
    public void setObj(GameObject tWMihon,GameObject tW)
    {
        tutiWallMihon = tWMihon;
        tutiWall = tW;
    }
}
