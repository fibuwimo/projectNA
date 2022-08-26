using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] agents;
    PlayerController plCon;

    enum STATE {
        PLAY,
        RESTART,
        GAMEOVER,
        }
    STATE state;
    // Start is called before the first frame update
    void Start()
    {
        plCon= player.GetComponent<PlayerController>();
        state = STATE.PLAY;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) 
        {
            case STATE.PLAY:
                if (plCon.state == PlayerController.STATE.DEAD)
                {
                    state = STATE.RESTART;
                }
                if (plCon.state == PlayerController.STATE.MUTEKI)
                {
                    for (int i = 0; i < agents.Length; i++)
                    {
                        agents[i].GetComponent<Enemy>().runAwake();
                    }
                }

                break;
            case STATE.RESTART:
                plCon.Restart();
                for(int i = 0; i< agents.Length; i++)
                {
                    agents[i].GetComponent<Enemy>().Restart();
                }
                state = STATE.PLAY;
                break;
        }
        
    }
}
