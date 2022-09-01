using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerKai : MonoBehaviour
{
    public enum STATE
    {
        ALIVE,
        MUTEKI,
        DEAD,
        FREEZ,
    }
    public GameObject[] agents;
    public STATE state;
    public int life;
    public Text lifeText;
    public Text mutekiText;
    public Text coinText;
    public int coinCount;
    int mutekiTime = 10;
    float mutekiCount = 0;
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
        if (state == STATE.ALIVE)
        {

        }
        if (state == STATE.MUTEKI)
        {
            mutekiCount += Time.deltaTime;
            if (mutekiCount >= mutekiTime)
            {
                mutekiText.text = "無敵じゃないよ";
                SetAlive();
            }

        }
        if (state == STATE.DEAD)
        {

        }
        if (state == STATE.FREEZ)
        {

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Coin"))
        {
            coinCount++;
            coinText.text = "コイン:" + coinCount;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag.Contains("agent"))
        {
            EnemyKai eneScr = other.gameObject.GetComponent<EnemyKai>();
            if (eneScr.state != EnemyKai.STATE.DEAD && eneScr.state != EnemyKai.STATE.RUN)
            {
                SetDead();
            }
        }
        if (other.gameObject.tag == ("mutekiItem"))
        {
            if (state == STATE.ALIVE)
            {
                SetMuteki();

                for (int i = 0; i < agents.Length; i++)
                {
                    agents[i].GetComponent<EnemyKai>().runAwake();
                }
                Destroy(other.gameObject);
            }
        }

    }
    void SetAlive()
    {
        state = STATE.ALIVE;
    }
    void SetMuteki()
    {
        mutekiText.text = "無敵だよ";
        mutekiCount = 0;
        state = STATE.MUTEKI;
    }
    void SetDead()
    {
        mutekiText.text = "無敵じゃないよ";
        Debug.Log("プレイヤー死亡");
        life -= 1;
        lifeText.text = "LIFE:" + life;
        state = STATE.DEAD;
    }
    void SetFreez()
    {
        state = STATE.FREEZ;
    }

    public void Restart()
    {
        coinText.text = "コイン:" + coinCount;
        transform.position = startPosition;
        float z = startPosition.z + 1.0f;
        transform.LookAt(new Vector3(startPosition.x, startPosition.y, z));
        SetAlive();
    }
}