using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Helper_Controller : MonoBehaviour
{
    public GameObject Meido;
    public GameObject RC;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Meido = GameObject.Find("GameOver_meido");
        RC = GameObject.Find("GameOver_RC");
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
}