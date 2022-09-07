using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Fukidashi : MonoBehaviour
{
    GameObject GOS_C;
    // Start is called before the first frame update
    void Start()
    {
        GOS_C = GameObject.Find("GameOverScene_Controller");
        this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        //this.Fukidashi_InActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver_Push_Active(){

        GOS_C.GetComponent<GameOverScene_Controller>().Set_isOperable();
    }
    public void Fukidashi_Active(){
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }
    public void Fukidashi_InActive(){
        Destroy(gameObject);
        //this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
