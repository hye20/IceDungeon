using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTester : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Mao");
    }


    void Update()
    {
        EnterMiniGame1();
    }

    void EnterMiniGame1()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Minigame1");
            player.transform.position = new Vector3(1, -0.25f, 0);

            player.GetComponent<PlayerController>().IsPlayerTurn = true;
            player.GetComponent<PlayerController>().DiceCount = 10000;
        } 
    }
}
