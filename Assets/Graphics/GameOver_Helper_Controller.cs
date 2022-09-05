using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Helper_Controller : MonoBehaviour
{
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
    public void EndAnimation(){
        this.anim.SetBool("isRoopEnd",true);
    }
}