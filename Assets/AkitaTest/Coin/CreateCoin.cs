using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCoin : MonoBehaviour
{
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        for (int z = -9; z < 10; z++)
        {
            for (int x = -9; x < 10; x++)
            {
                Instantiate(coin, new Vector3(x * 1.5f, 0.2f, z * 1.5f), Quaternion.identity);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
