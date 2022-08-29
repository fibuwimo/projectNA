using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutiMagic : MonoBehaviour
{
    public GameObject tutiWallTest;
    public GameObject tutiWallMihon;
    public GameObject tutiWallMihonRed;
    public GameObject tutiWall;
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
            float y = transform.position.y+0.5f;
            Vector3 magicWallPosition = new Vector3(x * 1.5f, y, z * 1.5f);
            /* GameObject obj=Instantiate(tutiWallTest, magicWallPosition, Quaternion.Euler(transform.forward));
             obj.GetComponent<tutiWallTest>().setObj(tutiWallMihon, tutiWall);*/
            Vector3 dir = new Vector3(x * 1.5f, 0f, z * 1.5f) - new Vector3(x * 1.5f, 10, z * 1.5f);
            Ray ray=new Ray(new Vector3(x * 1.5f, 10f, z * 1.5f), dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))

            {
                Debug.Log("レイキャスト通過");
                if (hit.collider.CompareTag("Wall"))
                {
                    magicWallPosition.y = hit.point.y+0.5f;
                    Instantiate(tutiWallMihonRed, magicWallPosition, Quaternion.Euler(transform.forward));
                }
                else
                {
                    magicWallPosition.y = hit.point.y+0.5f;
                    Instantiate(tutiWallMihon, magicWallPosition, Quaternion.Euler(transform.forward));
                }
                
            }
            


        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
               Vector3 tPosition = (transform.position + transform.forward * 2f);
               float x = Mathf.Round(tPosition.x / 1.5f);
               float z = Mathf.Round(tPosition.z / 1.5f);
            float y = transform.position.y+0.5f;
            Vector3 magicWallPosition = new Vector3(x * 1.5f, y, z * 1.5f);
            Vector3 dir = new Vector3(x * 1.5f, 0f, z * 1.5f) - new Vector3(x * 1.5f, 10, z * 1.5f);
            Ray ray = new Ray(new Vector3(x * 1.5f, 10f, z * 1.5f), dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))

            {
                if (hit.collider.CompareTag("Wall"))
                {

                }
                else
                {
                    magicWallPosition.y = hit.point.y+0.5f;
                    Instantiate(tutiWall, magicWallPosition, Quaternion.Euler(transform.forward));
                }

            }
            
        }
          
    }
}

