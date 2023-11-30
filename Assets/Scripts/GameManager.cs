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

    public Button battleBtn;

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
        battleBtn.onClick.AddListener(BattlePhase);
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

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
        player.transform.position = playerSpawnPoint;
        SceneManager.LoadScene("TEST_PJP");
    }
}