using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutiWallTest : MonoBehaviour
{
    public bool isOk=true;
    public GameObject tutiWallMihon;
    public GameObject tutiWall;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(createMihon), 0.1f);
        InvokeRepeating(nameof(create), 0f, 0.1f);
        Destroy(this.gameObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            isOk = false;
        }
    }
    public void setObj(GameObject twm, GameObject tw)
    {
        tutiWallMihon = twm;
        tutiWall = tw;
    }
    public void createMihon()
    {
        Debug.Log(isOk);
        if (isOk)
        {
            Instantiate(tutiWallMihon, transform.position, Quaternion.Euler(transform.forward));
        }
    }
    public void create()
    {
        if (Input.GetKeyUp(KeyCode.Z) && isOk)
        {
            Instantiate(tutiWall, transform.position, Quaternion.Euler(transform.forward));
            Destroy(this.gameObject);
        }

    }
}
