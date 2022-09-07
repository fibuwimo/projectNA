using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject_GuruGuru : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        //this.GuruGuru_InActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GuruGuru_Active(){
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void GuruGuru_InActive(){
        Destroy(gameObject);
        //this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
