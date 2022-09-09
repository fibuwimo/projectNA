using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Screen : MonoBehaviour
{
    GameObject Meido;
    GameObject Helper;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Meido = GameObject.Find("GameOver_meido");
        Helper = GameObject.Find("GameOver_Helper");
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
}
