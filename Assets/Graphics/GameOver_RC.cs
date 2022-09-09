using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_RC : MonoBehaviour
{
    public GameObject Screen;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        Screen = GameObject.Find("GameOver_Screen");
        this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RC_Active(){
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        this.anim.SetBool("isRoopEnd",true);
    }

    public void RC_InActive(){
        Screen.GetComponent<GameOver_Screen>().Apeear();
        Destroy(gameObject);
        //this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
