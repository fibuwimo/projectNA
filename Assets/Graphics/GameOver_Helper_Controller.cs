using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Helper_Controller : MonoBehaviour
{
    public GameObject Meido;
    public GameObject RC;
    public GameObject Hand;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Meido = GameObject.Find("GameOver_meido");
        RC = GameObject.Find("GameOver_RC");
        Hand = GameObject.Find("GameOver_Helper_Hand");
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndAnimation(){
        this.anim.SetBool("isRoopEnd",true);
    }
    public void Surprized_Meido(){
        Meido.GetComponent<GameOver_Meido_Controller>().End_FirstRoop_Anim();
    }
    public void Push_RC(){
        RC.GetComponent<GameOver_RC>().RC_Active();
    }
    public void Zoom_In(){
        this.anim.SetBool("isZoomStart",true);
    }
    public void Surprize_Zoom(){
        this.anim.SetBool("isZoomSurStart",true);
    }
    public void Hand_Appear(){
        Hand.GetComponent<GameOver_Helper_Hand>().Helper_Hand_Active();
    }
}