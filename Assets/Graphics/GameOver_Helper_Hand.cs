using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Helper_Hand : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Helper_Hand_Active(){
        this.anim.SetBool("isWaitEnd",true);
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void Helper_Hand_InActive(){
        Destroy(gameObject);
        //this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
