using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RumbaController : MonoBehaviour
{
    Rigidbody rb;
    int comboCount;
    public GameObject canvas;
    
    public Text comboTextRumba;
    PlayerControllerKai plCon;
    public AudioClip soundRumba;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 10.0f;
        canvas = GameObject.FindWithTag("Canvas");
        plCon = GameObject.FindWithTag("Player").GetComponent<PlayerControllerKai>();

        audioSource.PlayOneShot(soundRumba);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude <= 0.1f)
        {
            
            audioSource.Stop();
            Destroy(this.gameObject);
        }
        if (rb.velocity.magnitude <= 8.0f)
        {
            rb.velocity = transform.forward * 10.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("agent"))
        {
            EnemyKai eneScr = other.gameObject.GetComponent<EnemyKai>();
            if (eneScr.state != EnemyKai.STATE.DEAD)
            {
                eneScr.deadByRumba();
                comboCount++;
                //comboText2.text = "+" + (int)(500 * Mathf.Pow(2f, comboCount - 1));
                Text comboTextR = Instantiate(comboTextRumba);
                comboTextR.transform.SetParent(canvas.transform, false);
                comboTextR.text = "+" + (int)(500 * Mathf.Pow(2f, comboCount - 1));
                plCon.RumbaScoreGain((int)(500 * Mathf.Pow(2f, comboCount - 1)));
                
            }
            
        }
        if (other.gameObject.tag.Contains("Coin"))
        {
            
            Destroy(other.gameObject);
            plCon.CoinScoreGain();
        }
    }
}
