using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_RC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RC_Active(){
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void RC_InActive(){
        Destroy(gameObject);
        //this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
