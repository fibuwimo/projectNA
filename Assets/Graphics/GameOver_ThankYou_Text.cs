using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_ThankYou_Text : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThankYou_Text_Active(){
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void ThankYou_Text_InActive(){
        Destroy(gameObject);
    }
}
