using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Score_Effect_2 : MonoBehaviour
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

    public void Score_Effect_2_Active(){
        this.anim.SetBool("isWaitEnd",true);
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void Score_Effect_2_InActive(){
        Destroy(gameObject);
        //this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void Score_Effect_2_Zoom_Out(){
        this.anim.SetBool("isZoomOutStart",true);
    }

    public void Score_Effect_2_End(){
        this.anim.SetBool("isEndStart",true);
    }
}
