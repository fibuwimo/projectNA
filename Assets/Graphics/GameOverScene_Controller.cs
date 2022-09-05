﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene_Controller : MonoBehaviour
{
    public bool isOperable;
    public GameObject Doyon;
    public GameObject Moji;
    public GameObject Helper;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        isOperable = false;
        Moji = GameObject.Find("GameOver_Moji");
        Doyon = GameObject.Find("GameOver_Doyon");
    }

    // Update is called once per frame
    void Update()
    {
        if(isOperable && (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))){
            Moji.GetComponent<GameOver_Moji_Controller>().EndAnimation();
            Doyon.GetComponent<GameOver_Doyon_Controller>().EndAnimation();
        }
    }

    void Set_isOperable(){
        isOperable = true;
    }
}
