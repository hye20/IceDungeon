using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform[] monterSpawnPoint;
    [SerializeField] private Monster[] monsterData;
    [SerializeField] private Monster[] monsters;//monsters instance
    public bool playerTurn = true;
    private bool monsters_is_dead = false;
    private bool battle_end = false;

    [SerializeField] public GameObject btnList;
    [SerializeField] public Button attackBtn;
    [SerializeField] public Button magicBtn;
    [SerializeField] public Button runBtn;
    [SerializeField] public Button exitBtn;

    public float delay;//for test

    void Start()
    {
        GameManager.instance.player.transform.position = playerSpawnPoint.position;
        playerTurn = true;
        monsters_is_dead = false;
        battle_end = false;
        attackBtn.onClick.AddListener(PlayerAtk);
        magicBtn.onClick.AddListener(PlayerMagic);
        runBtn.onClick.AddListener(PlayerRun);
        SetMonsters();
        exitBtn.onClick.AddListener(GameManager.instance.QuestPhase);
        exitBtn.gameObject.SetActive(false);
    }

    void Update()
    {
        Check_Monsters_Dead();
        if (GameManager.instance.player.HP > 0 && !monsters_is_dead)
        {
            PlayerAction();
            MonsterAction();
        }
        else if (GameManager.instance.player.HP <= 0)
        {
            Debug.Log($"player HP : {GameManager.instance.player.HP}");
            Debug.Log("Battle phase End");
            PrintResult();
        }
        else if (monsters_is_dead)
        {
            Debug.Log($"Monsters terminated : {monsters_is_dead}");
            Debug.Log($"player HP : {GameManager.instance.player.HP}");
            PrintResult();
        }
    }

    public void MonsterAction()
    {
        int randomAction;
        if (!playerTurn && !monsters_is_dead)
        {
            for (int i = 0; i < monsters.Length; i++)
            {
                randomAction = Random.Range(1, 3);//1~2
                if (randomAction == 1)
                {
                    MonsterAtk(monsters[i]);
                }
                else if (randomAction == 2)
                {
                    MonsterSkill(monsters[i]);
                }
                //random action
                //monster atk
            }
            ChangeTurn();
            btnList.gameObject.SetActive(true);
        }
    }
    public void PlayerAction()
    {
        if (playerTurn == true)
        {
            //+select action
            //btnList.gameObject.SetActive(true);
        }
    }

    private void PlayerAtk()
    {
        btnList.gameObject.SetActive(false);
        Invoke("ChangeTurn", 3.0f);
        //play atk anim
        GameManager.instance.player.controller.is_Attack = true;
        Debug.Log("player's attack");
        for (int i = 0; i < monsters.Length; i++)
        {
            if (!monsters[i].is_dead) monsters[i].HP -= GameManager.instance.player.atk;
        }
    }
    private void PlayerMagic()
    {
        btnList.gameObject.SetActive(false);
        Invoke("ChangeTurn", 3.0f);
        GameManager.instance.player.controller.is_Magic = true;
        Debug.Log("player's magic");
        for (int i = 0; i < monsters.Length; i++)
        {
            if (!monsters[i].is_dead) monsters[i].HP -= GameManager.instance.player.SP;
        }
    }
    public void PrintResult()
    {
        if (GameManager.instance.player.HP <= 0)
        {
            Debug.Log("defeated");
        }
        else if (monsters_is_dead && !battle_end)
        {
            exitBtn.gameObject.SetActive(true);
            GameManager.instance.player.controller.is_Victory = true;
            battle_end = true;
        }
    }
    private void PlayerRun() { }

    public void ChangeTurn()
    {
        playerTurn = !playerTurn;
        Debug.Log($"Player Turn is {playerTurn} now.");
    }

    private void SetMonsters()
    {
        int rand = Random.Range(1, 4);//1~3¸¶¸®
        monsters = new Monster[rand];
        for (int i = 0; i < rand; i++)
        {
            monsters[i] = Instantiate(monsterData[0]);
            monsters[i].transform.position = monterSpawnPoint[i].position;
            Debug.Log(monsters[i].HP);
        }
    }
    private bool Check_Monsters_Dead()
    {
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
            monsters_is_dead = true;
        }
        return monsters_is_dead;
    }
    private void MonsterAtk(Monster monster)
    {
        if (0.1 >= Random.value)
        {
            Debug.Log("But, monster's attack is missed");
        }
        else
        {
            Debug.Log($"Monster-{monster.name} is attacked!");
            GameManager.instance.player.HP -= monster.atk;// -> monster.atk
        }
    }
    private void MonsterSkill(Monster monster)
    {
        if (0.1 >= Random.value)
        {
            Debug.Log("But, monster's attack is missed");
        }
        else
        {
            Debug.Log($"Monster-{monster.name} uses special attack!");
            GameManager.instance.player.HP -= monster.SP;// -> monster.atk
        }
    }
}
