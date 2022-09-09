using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTextController : MonoBehaviour
{
    public RectTransform rT;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 1.5f);
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        rT.position += new Vector3(0, 0.5f, 0);
    }
}
