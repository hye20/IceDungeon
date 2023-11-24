using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Player player;
    private Monster[] monsters;
    public Button btn;
    public bool playerTurn;

    public float delay;//for test

    void Start()
    {
        playerTurn = true;
        btn.onClick.AddListener(MagicAtk);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction();
        MonsterAction();
    }
    public void MonsterAction()
    {
        if(playerTurn == false)
        {
            Debug.Log(playerTurn);
            delay += Time.deltaTime;
            //random action
            btn.gameObject.SetActive(false);
            if (delay > 3.0f)
            {
                Debug.Log("monster Action");
                playerTurn = true;
                delay = 0f;
            }
        }
    }
    public void PlayerAction()
    {
        if(playerTurn == true)
        {
            //select action
            btn.gameObject.SetActive(true);
        }
    }
    public void MagicAtk()//일반 공격이랑 호환 가능한지? 변수 조정으로?
    {
        player.controller.pramAction=true;
        Debug.Log("Magic atk");
        playerTurn = false;
        /*
        if(!player.controller.pramAction)
        {
            playerTurn = false;
        }*/
    }
}
