using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerKai : MonoBehaviour
{
    public GameObject player;
    public GameObject[] agents;
    PlayerControllerKai plCon;

    enum STATE {
        PLAY,
        RESTART,
        GAMEOVER,
        }
    STATE state;
    // Start is called before the first frame update
    void Start()
    {
        plCon= player.GetComponent<PlayerControllerKai>();
        state = STATE.PLAY;
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
                

                break;
            case STATE.RESTART:
                plCon.Restart();
                for(int i = 0; i< agents.Length; i++)
                {
                    agents[i].GetComponent<EnemyKai>().Restart();
                }
                state = STATE.PLAY;
                break;
        }
        
    }
}
