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
        RESTART,
        CLEAR,
        GAMEOVER,
        }
    STATE state;
    public int coinMax = 50;
    public int stageCount = 1;
    public Text stageText;
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        plCon= player.GetComponent<PlayerControllerKai>();
        state = STATE.PLAY;
        stageText.text = "STAGE1";
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE.PLAY:
                if (plCon.state == PlayerControllerKai.STATE.DEAD)
                {
                    state = STATE.RESTART;
                }
                if (plCon.coinCount >= coinMax)
                {
                    plCon.coinCount = 0;
                    state = STATE.CLEAR;
                }
                if (plCon.life == 0)
                {
                    SceneManager.LoadScene("GameOverTest");
                }

                break;
            case STATE.RESTART:
                plCon.Restart();
                for (int i = 0; i < agents.Length; i++)
                {
                    agents[i].GetComponent<EnemyKai>().Restart();
                }
                state = STATE.PLAY;
                break;
            case STATE.CLEAR:
                stageCount++;
                if (stageCount > 5)
                {
                    SceneManager.LoadScene("ClearTest");
                }else {
                for (int i = 0; i < agents.Length; i++)
                {
                    agents[i].GetComponent<EnemyKai>().stageCount++;
                }
                stageText.text = "STAGE" + stageCount;
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Coin");
                foreach (GameObject coin in objects)
                {
                    Destroy(coin);
                }

                //コイン生成。ここはステージ構成の違いにより、後で書き直しが必要。
                /*
                for (int z = -9; z < 10; z++)
                {
                    for (int x = -9; x < 10; x++)
                    {
                        Instantiate(coin, new Vector3(x * 1.5f, 0.2f, z * 1.5f), Quaternion.identity);
                    }
                }
                */
                        Instantiate(coin, new Vector3(0,0,0), Quaternion.identity);

                plCon.Restart();
                for (int i = 0; i < agents.Length; i++)
                {
                    agents[i].GetComponent<EnemyKai>().Restart();
                }
                state = STATE.PLAY;
                }
                break;
        }
        
    }
}
