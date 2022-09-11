using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Score_Text : MonoBehaviour
{
    int score;
    string score_8digits;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScore(int sc){
        score = sc;
        score_8digits = score.ToString("D7");
        gameObject.GetComponent<UnityEngine.UI.Text>().text = score_8digits;
    }

    public void Score_Text_Zoom_Out(){
        this.anim.SetBool("isWaitEnd",true);
    }

    public void Score_Text_End(){
        this.anim.SetBool("isEndStart",true);
    }
}
