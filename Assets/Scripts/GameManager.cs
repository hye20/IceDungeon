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
    enum Mode { QuestMode, BattleMode }//player.controller.NowMode
    [SerializeField]private Mode mode;


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
    }
    private void Awake()
    {
        player = Instantiate(player);
        playerSpawnPoint = new Vector3(0, 0.25f, 0);
        player.transform.position = playerSpawnPoint;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //****************************************************************************
        //FOR_TEST
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (mode == Mode.QuestMode)BattlePhase();
            else if (mode == Mode.BattleMode)QuestPhase();
        }
        //*****************************************************************************
    }
    public void BattlePhase()
    {
        mode = Mode.BattleMode;
        player.controller.BattleMode();
        playerSpawnPoint = player.transform.position; 
        SceneManager.LoadScene("BattleScene");
    }
    public void QuestPhase()
    {
        mode = Mode.QuestMode;
        player.controller.QuestMode();
        SceneManager.LoadScene("TEST_PJP");
        //eventController.playchangeScene
        player.transform.position = playerSpawnPoint;
    }
}