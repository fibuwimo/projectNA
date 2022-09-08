using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScoreController : MonoBehaviour
{
    public Text resultScoreT;
    // Start is called before the first frame update
    void Start()
    {
        resultScoreT.text = "すこあ"+PlayerControllerKai.getResultScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
