using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform[] monterSpawnPoint;
    public Monster[] monsters;
    public Button btn;
    public bool playerTurn = true;
    public bool enemyAlive = true;

    public float delay;//for test

    void Start()
    {
        GameManager.instance.player.transform.position = playerSpawnPoint.position;
        playerTurn = true;
        btn.onClick.AddListener(Magicbtn);
        SetMonsters();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.player.HP > 0 && !Check_Monsters_Dead())
        {
            PlayerAction();
            MonsterAction();
        }
        else if (GameManager.instance.player.HP <= 0 || Check_Monsters_Dead())
        {
            Debug.Log(GameManager.instance.player.HP);
            Debug.Log(Check_Monsters_Dead());
            Debug.Log("Battle phase End");
            GameManager.instance.QuestPhase();
            GameManager.instance.player.controller.QuestMode();
        }
    }
    public void MonsterAction()
    {
        if (playerTurn == false)
        {
            Debug.Log(playerTurn);
            delay += Time.deltaTime;
            //random action
            btn.gameObject.SetActive(false);
            if (delay > 3.0f)
            {
                MonsterAtk();
                playerTurn = true;
                delay = 0f;
            }
        }
    }
    public void PlayerAction()
    {
        if (playerTurn == true)
        {
            //select action
            btn.gameObject.SetActive(true);
        }
    }

    private void PlayerAtk()
    {
        playerTurn = false;
        Debug.Log("player's attack");
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].HP -= GameManager.instance.player.atk;
        }
    }
    public void Magicbtn()//일반 공격이랑 호환 가능한지? 변수 조정으로?
    {
        GameManager.instance.player.controller.animMagic = true;
        PlayerAtk();
        /*
        if(!player.controller.pramAction)
        {
            playerTurn = false;
        }*/
    }

    public void ChangeTurn()
    {
        playerTurn = !playerTurn;
        Debug.Log($"Player Turn is {playerTurn} now.");
    }

    private void SetMonsters()
    {
        int rand = Random.Range(1, 3);//1~3마리
        for (int i = 0; i <= rand; i++)
        {
            monsters[i] = Instantiate(monsters[i]);
            monsters[i].transform.position = monterSpawnPoint[i].position;
            Debug.Log(monsters[i].HP);
        }
    }
    private bool Check_Monsters_Dead()
    {
        bool is_dead = false;
        int deadMonsterCnt = 0;
        for (int i = 0; i < monsters.Length; i++)
        {
            if (monsters[i].is_dead)
            {
                deadMonsterCnt++;
            }
        }
        if (deadMonsterCnt == monsters.Length)
        {
            is_dead = true;
        }
        return is_dead;
    }
    private void MonsterAtk()
    {
        Debug.Log("Monster's attack");
        if (0.1 >= Random.value)
        {
            Debug.Log("But, monster's attack is missed");
        }
        GameManager.instance.player.HP -= 25;// -> monster.atk
    }
}
