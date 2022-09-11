using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Score_Effect_1 : MonoBehaviour
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

    public void Score_Effect_1_Active(){
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void Score_Effect_1_InActive(){
        Destroy(gameObject);
        //this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void Score_Effect_1_Zoom_Out(){
        this.anim.SetBool("isWaitEnd",true);
    }

    public void Score_Effect_1_End(){
        this.anim.SetBool("isEndStart",true);
    }
}
