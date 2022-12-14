using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerKai : MonoBehaviour
{
    public GameObject player;
    public GameObject[] agents;
    PlayerControllerKai plCon;

    enum STATE {
        PLAY,
        INIT,
        RESTART,
        CLEAR,
        GAMEOVER,
        }
    STATE state;
    public int coinMax = 50;
    public int stageCount = 1;
    public Text stageText;
    public Text stageText2;
    public GameObject coins;
    public GameObject mutekiitems;
    public float clearEffectTime;
    float clearEffectCount;
    public float deadEffectTime;
    float deadEffectCount;
    public float restartEffectTime;
    float restartEffectCount;
    public float initEffectTime;
    float initEffectCount;
    public float gameoverEffectTime;
    // Start is called before the first frame update
    void Start()
    {
        plCon= player.GetComponent<PlayerControllerKai>();
        state = STATE.PLAY;
        setInit();
        //stageText.text = "STAGE1";
        stageText.text = "STAGE" + stageCount;
        stageText2.text = "STAGE" + stageCount;
        if (stageCount <= 30)
        {
            plCon.stageCount = stageCount;
        }
        else
        {
            plCon.stageCount = 30;
        }
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].activeSelf)
            {
                if (stageCount <= 30)
                {
                    agents[i].GetComponent<EnemyKai>().stageCount = stageCount;
                }
                else
                {
                    agents[i].GetComponent<EnemyKai>().stageCount = 30;
                }
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE.PLAY:
                if (plCon.state == PlayerControllerKai.STATE.DEAD)
                {
                    //state = STATE.RESTART;
                    setRestart();
                }
                if (plCon.coinCount >= coinMax)
                {
                    plCon.coinCount = 0;
                    //state = STATE.CLEAR;
                    setClear();
                }
                if (plCon.life == 0)
                {
                    plCon.Gameover(gameoverEffectTime, restartEffectTime);
                    for (int i = 0; i < agents.Length; i++)
                    {
                        if (agents[i].activeSelf)
                        {
                            agents[i].GetComponent<EnemyKai>().Gameover(gameoverEffectTime, restartEffectTime);
                        }
                    }
                    setGameover();
                }

                break;
            case STATE.INIT:
                initEffectCount += Time.deltaTime;
                if (initEffectCount >= initEffectTime)
                {
                    setPlay();
                }
                break;
            case STATE.RESTART:
                deadEffectCount += Time.deltaTime;
                if (deadEffectCount >= deadEffectTime)
                {
                    setPlay();
                }
                break;

            case STATE.CLEAR:
                clearEffectCount += Time.deltaTime;
                if (clearEffectCount >= clearEffectTime)
                {
                    setPlay();
                }
                break;
        }
        
    }
    IEnumerator Gameover()
    {
        yield return new WaitForSeconds(gameoverEffectTime);
        SceneManager.LoadScene("GameOver");
    }
    IEnumerator Haiti()
    {
        yield return new WaitForSeconds(clearEffectTime);
        stageText.text = "STAGE" + stageCount;
        stageText2.text = "STAGE" + stageCount;
        plCon.coinCount = 0;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in objects)
        {
            Destroy(coin);
        }
        GameObject[] objects2 = GameObject.FindGameObjectsWithTag("mutekiItem");
        foreach (GameObject mutekiitem in objects2)
        {
            Destroy(mutekiitem);
        }
        Instantiate(coins, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(mutekiitems, new Vector3(0, 0, 0), Quaternion.identity);

        //ここでステージごとの敵増加

        if (stageCount == 2)
        {
            agents[4].SetActive(true);
        }
        if (stageCount == 3)
        {
            agents[5].SetActive(true);
            agents[6].SetActive(true);
        }
        if (stageCount == 5)
        {
            agents[7].SetActive(true);
            agents[8].SetActive(true);
        }
        if (stageCount == 7)
        {
            agents[9].SetActive(true);
        }
        if (stageCount == 9)
        {
            agents[10].SetActive(true);
        }
        if (stageCount == 12)
        {
            agents[11].SetActive(true);
        }

    }
    void setPlay()
    {
        state = STATE.PLAY;
    }
    void setRestart()
    {
        plCon.Restart(deadEffectTime, restartEffectTime);
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].activeSelf)
            {
                agents[i].GetComponent<EnemyKai>().Restart(deadEffectTime, restartEffectTime);
            }
        }
        deadEffectCount = 0;
        state = STATE.RESTART;
    }
    void setInit()
    {
        plCon.Init(0, initEffectTime);
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].activeSelf)
            {
                agents[i].GetComponent<EnemyKai>().Init(0, initEffectTime);
            }
        }
        initEffectCount = 0;
        state = STATE.INIT;
    }

    void setClear()
    {
        stageCount++;
        if (stageCount <= 30)
        {
            plCon.stageCount = stageCount;
        }
        else
        {
            plCon.stageCount = 30;
        }
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].activeSelf)
            {
                if (stageCount <= 30)
                {
                    agents[i].GetComponent<EnemyKai>().stageCount = stageCount;
                }
                else
                {
                    agents[i].GetComponent<EnemyKai>().stageCount = 30;
                }
            }
        }

        plCon.Clear(clearEffectTime, restartEffectTime);
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].activeSelf)
            {
                agents[i].GetComponent<EnemyKai>().Clear(clearEffectTime, restartEffectTime);
            }
        }
        clearEffectCount = 0;
        state = STATE.CLEAR;
        StartCoroutine(Haiti());

    }
    void setGameover()
    {
        state = STATE.GAMEOVER;
        StartCoroutine(Gameover());
    }
    
}
