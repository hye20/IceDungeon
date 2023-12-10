using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public Vector3 playerSpawnPoint;

    public enum Mode { QuestMode, BattleMode }//player.controller.NowMode
    public Mode mode;

    GameObject orangePenguin;
    public bool Minigame1Clear;

    private void Start()
    {
        if(instance == null)
        { 
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            Destroy(this.player.gameObject);
        }

        Minigame1Clear = false;
    }
    private void Awake()
    {
        player = Instantiate(player);
        playerSpawnPoint = new Vector3(0.5f, 4.0f, 0);
        player.transform.position = playerSpawnPoint;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //****************************************************************************
        //FOR_TEST, test 전용 코드 여기에 추가해주세요
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (mode == Mode.QuestMode)BattlePhase();
            else if (mode == Mode.BattleMode)QuestPhase();
        }
        //*****************************************************************************

        if(Minigame1Clear && mode == Mode.QuestMode)
        {
            orangePenguin = GameObject.Find("Orange_Penguin_MiniGame");
            Destroy(orangePenguin);
        }
    }
    public void BattlePhase()
    {
        mode = Mode.BattleMode;
        player.controller.BattleMode();
        SceneManager.LoadScene("BattleScene");
        playerSpawnPoint = player.transform.position; 
    }
    public void QuestPhase()
    {
        mode = Mode.QuestMode;
        player.controller.QuestMode();
        SceneManager.LoadScene("DKTest");
        //eventController.playchangeScene
        player.transform.position = playerSpawnPoint;
    }
}