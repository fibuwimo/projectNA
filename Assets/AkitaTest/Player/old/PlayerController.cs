using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum STATE
    {
        ALIVE,
        MUTEKI,
        DEAD,
    }
    public STATE state;
    public int life;
    public Text lifeText;
    public Text mutekiText;
    public Text coinText;
    int coinCount;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        state = STATE.ALIVE;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Coin"))
        {
            coinCount++;
            coinText.text = "コイン:" +coinCount;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag.Contains("agent"))
        {
            if (state == STATE.ALIVE)
            {
                Dead();
            }
        }
        if (other.gameObject.tag==("mutekiItem"))
        {
            if (state == STATE.ALIVE)
            {
                StartCoroutine("Muteki");
            }
            Destroy(other.gameObject);
        }

    }
    private void Dead()
    {
        mutekiText.text = "無敵じゃないよ";
        Debug.Log("ちんだ");
        life -= 1;
        lifeText.text = "LIFE:"+life;
        state = STATE.DEAD;
    }
    IEnumerator Muteki()
    {
        mutekiText.text = "無敵だよ";
        state = STATE.MUTEKI;
        yield return new WaitForSeconds(10);
        if (state == STATE.MUTEKI)
        {
            mutekiText.text = "無敵じゃないよ";
            state = STATE.ALIVE;
        }
    }
    

    public void Restart()
    {
        transform.position = startPosition;
        float z= startPosition.z + 1.0f;
        transform.LookAt(new Vector3(startPosition.x, startPosition.y, z));
        state = STATE.ALIVE;
    }
}
