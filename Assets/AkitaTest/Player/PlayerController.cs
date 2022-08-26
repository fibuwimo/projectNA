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
    // Start is called before the first frame update
    void Start()
    {
        state = STATE.ALIVE;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
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
        transform.position = new Vector3(0, 0, 0);
        state = STATE.ALIVE;
    }
}
