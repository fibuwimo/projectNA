using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMagic : MonoBehaviour
{
    public GameObject magicWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("発射");
            Vector3 magicWallPosition =transform.position+transform.forward*2.0f;
            magicWallPosition.y = 0.5f;
            Instantiate(magicWall,magicWallPosition,Quaternion.Euler(transform.forward));
        }
    }
}
