using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene_Controller : MonoBehaviour
{
    public bool isOperable;
    public GameObject Doyon;
    public GameObject Moji;
    public GameObject Helper;

    public GameObject Meido;

    public int score;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        isOperable = false;
        Moji = GameObject.Find("GameOver_Moji");
        Doyon = GameObject.Find("GameOver_Doyon");
        Helper = GameObject.Find("GameOver_Helper");
        Meido = GameObject.Find("GameOver_Meido");
        score = PlayerControllerKai.getResultScore();
        Debug.Log(score);
    }

    // Update is called once per frame
    void Update()
    {
        if(isOperable && (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))){
            Moji.GetComponent<GameOver_Moji_Controller>().EndAnimation();
            Doyon.GetComponent<GameOver_Doyon_Controller>().EndAnimation();
            Helper.GetComponent<GameOver_Helper_Controller>().EndAnimation();
            isOperable = false;
        }
    }

    public void Set_isOperable(){
        isOperable = true;
    }

    public int getScore(){
        return score;
    }
}
