using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutiMagic : MonoBehaviour
{
    public GameObject magicWallTest;
    public GameObject magicWallMihon;
    public GameObject magicWall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Vector3 tPosition = (transform.position + transform.forward * 2f);
            float x = Mathf.Round(tPosition.x / 1.5f);
            float z = Mathf.Round(tPosition.z / 1.5f);
            Vector3 magicWallPosition = new Vector3(x * 1.5f, 0.5f, z * 1.5f);
            Instantiate(magicWallMihon, magicWallPosition, Quaternion.Euler(transform.forward)); 
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
               Vector3 tPosition = (transform.position + transform.forward * 2f);
               float x = Mathf.Round(tPosition.x / 1.5f);
               float z = Mathf.Round(tPosition.z / 1.5f);
               Vector3 magicWallPosition = new Vector3(x * 1.5f, 0.5f, z * 1.5f);
               Instantiate(magicWall, magicWallPosition, Quaternion.Euler(transform.forward));
        }
          
    }
}

