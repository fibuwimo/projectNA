using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject_GuruGuru : MonoBehaviour
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

    void Fukidashi_Active(){
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }
}
