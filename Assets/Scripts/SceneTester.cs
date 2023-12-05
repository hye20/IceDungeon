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
            SceneManager.LoadScene("DKTest_MiniGame1");
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
