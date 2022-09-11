using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_Title : MonoBehaviour
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

    public void Title_Appear(){
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        this.anim.SetBool("isWaitEnd",true);
    }

    public void Go_To_TitleScene(){
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }
}
