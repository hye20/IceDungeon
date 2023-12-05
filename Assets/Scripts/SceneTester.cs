using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTester : MonoBehaviour
{
    const string minigame1 = "DKTest_MiniGame1";
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
            SceneManager.LoadScene(minigame1);
            player.transform.position = new Vector3(1, -0.25f, 0);
        }
    }
}
