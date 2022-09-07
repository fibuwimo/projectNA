using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbaMagic : MonoBehaviour
{
    public GameObject rumbaMihon;
    public GameObject rumbaMihonRed;
    public GameObject rumba;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Vector3 tPosition = (transform.position + transform.forward * 2f);
            float x = Mathf.Round(tPosition.x / 1.5f);
            float z = Mathf.Round(tPosition.z / 1.5f);
            float y = transform.position.y + 0.5f;
            Vector3 rumbaPosition = new Vector3(x * 1.5f, y, z * 1.5f);
            Vector3 pPosition = new Vector3((Mathf.Round(transform.position.x / 1.5f) * 1.5f), y, (Mathf.Round(transform.position.z / 1.5f)) * 1.5f);
            /* GameObject obj=Instantiate(tutiWallTest, magicWallPosition, Quaternion.Euler(transform.forward));
             obj.GetComponent<tutiWallTest>().setObj(tutiWallMihon, tutiWall);*/
            Vector3 dir = new Vector3(x * 1.5f, 0f, z * 1.5f) - new Vector3(x * 1.5f, 10, z * 1.5f);
            Ray ray = new Ray(new Vector3(x * 1.5f, 10f, z * 1.5f), dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))

            {
                Debug.Log("レイキャスト通過");
                if (hit.collider.CompareTag("Wall"))
                {
                    rumbaPosition.y = hit.point.y+0.2f;
                    Instantiate(rumbaMihonRed, rumbaPosition, Quaternion.Euler(transform.forward));
                }
                else
                {
                    rumbaPosition.y = hit.point.y+0.2f;
                    Instantiate(rumbaMihon, rumbaPosition, Quaternion.Euler(transform.forward));
                }

            }



        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Vector3 tPosition = (transform.position + transform.forward * 2f);
            float x = Mathf.Round(tPosition.x / 1.5f);
            float z = Mathf.Round(tPosition.z / 1.5f);
            float y = transform.position.y + 0.5f;
            Vector3 rumbaPosition = new Vector3(x * 1.5f, y, z * 1.5f);
            Vector3 pPosition = new Vector3((Mathf.Round(transform.position.x / 1.5f)*1.5f), y,(Mathf.Round(transform.position.z / 1.5f))*1.5f);
            Vector3 rumbaDirection = (rumbaPosition - pPosition).normalized;
            Quaternion requiredRotation = Quaternion.FromToRotation(new Vector3(0, 0, 1.0f),rumbaDirection);
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
                    Debug.Log(rumbaDirection);
                    rumbaPosition.y = hit.point.y+0.2f;
                    Instantiate(rumba, rumbaPosition, requiredRotation);
                }

            }

        }

    }
}
