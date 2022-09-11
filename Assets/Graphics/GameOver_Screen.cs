using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver_Screen : MonoBehaviour
{
    GameObject Meido;
    GameObject Helper;
    GameObject GOSC;
    GameObject Effect_1;
    GameObject Effect_2;
    GameObject Score_Text;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Meido = GameObject.Find("GameOver_meido");
        Helper = GameObject.Find("GameOver_Helper");
        GOSC = GameObject.Find("GameOverScene_Controller");
        Effect_1 = GameObject.Find("GameOver_Score_Effect_1");
        Effect_2 = GameObject.Find("GameOver_Score_Effect_2");
        Score_Text = GameObject.Find("GameOver_Score_Text");
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Apeear(){
        this.anim.SetBool("isRoopEnd",true);
    }

    public void Meido_Zoom_In(){
        Meido.GetComponent<GameOver_Meido_Controller>().Zoom_In();
        Helper.GetComponent<GameOver_Helper_Controller>().Zoom_In();
    }

    public void Score_Effect_1_Apeear(){
        Effect_1.GetComponent<GameOver_Score_Effect_1>().Score_Effect_1_Active();
    }

    public void Score_and_Effect_2_Appear(){
        int score = GOSC.GetComponent<GameOverScene_Controller>().getScore();
        Score_Text.GetComponent<GameOver_Score_Text>().setScore(score);
        Effect_2.GetComponent<GameOver_Score_Effect_2>().Score_Effect_2_Active();
        Meido.GetComponent<GameOver_Meido_Controller>().Surprize_Zoom();
        Helper.GetComponent<GameOver_Helper_Controller>().Surprize_Zoom();
    }

    public void Score_Effect_Zoom_Out_Start(){
        Score_Text.GetComponent<GameOver_Score_Text>().Score_Text_Zoom_Out();
        Effect_1.GetComponent<GameOver_Score_Effect_1>().Score_Effect_1_Zoom_Out();
        Effect_2.GetComponent<GameOver_Score_Effect_2>().Score_Effect_2_Zoom_Out();
    }

    public void Screens_WipeOut(){
        this.anim.SetBool("isEndStart",true);
        Score_Text.GetComponent<GameOver_Score_Text>().Score_Text_End();
        Effect_1.GetComponent<GameOver_Score_Effect_1>().Score_Effect_1_End();
        Effect_2.GetComponent<GameOver_Score_Effect_2>().Score_Effect_2_End();
    }
}