using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour
{
    public GameObject wall;
    public GameObject CrossTrigger;
    // Start is called before the first frame update
    void Start()
    {
        /*
        for (int z = -4; z < 5; z++)
        {
            for (int x = -4; x < 5; x++)
            {
                Instantiate(wall, new Vector3(x*3, 0,z*3), Quaternion.identity);
            }
        }
        */
        for (int z = -5; z < 5; z++)
        {
            for (int x = -5; x < 5; x++)
            {
                Instantiate(CrossTrigger, new Vector3(x * 3+1.5f, 0, z * 3+1.5f), Quaternion.identity);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
