using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Meido_Controller : MonoBehaviour
{
    public GameObject Fukidashi;
    public GameObject GuruGuru;
    public GameObject Bandage;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Fukidashi = GameObject.Find("GameOver_Fukidashi");
        GuruGuru = GameObject.Find("GameOver_GuruGuru");
        Bandage = GameObject.Find("GameOver_Bandage");
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void End_FirstRoop_Anim(){
        this.anim.SetBool("isRoopEnd",true);
    }

    public void GoodBye_Fukidashi_and_GuruGuru(){
        Fukidashi.GetComponent<GameOver_Fukidashi>().Fukidashi_InActive();
        GuruGuru.GetComponent<GameObject_GuruGuru>().GuruGuru_InActive();
    }

    public void Bandage_Start(){
        //Bandage.GetComponent<GameOver_Meido_Bandage>().Bandage_Active();
    }
    public void Zoom_In(){
        this.anim.SetBool("isZoomStart",true);
    }


}
