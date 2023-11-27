using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Player player;
    public Monster[] monsters;
    private int monstersHP;
    public Button btn;
    public bool playerTurn = true;
    public bool enemyAlive = true;

    public float delay;//for test

    void Start()
    {
        playerTurn = true;
        btn.onClick.AddListener(Magicbtn);
        SetMonsters();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.HP > 0 && monstersHP > 0)
        {
            PlayerAction();
            MonsterAction();
        }
        else if (player.HP <= 0 || monstersHP <= 0)
        {
            Debug.Log(player.HP);
            Debug.Log(monstersHP);
            Debug.Log("Battle phase End");
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
        Debug.Log("player's attack");
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].HP -= 50;
        }
    }
    public void Magicbtn()//일반 공격이랑 호환 가능한지? 변수 조정으로?
    {
        player.controller.animMagic = true;
        playerTurn = false;
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
        int rand = Random.RandomRange(1, 3);
        for (int i = 0; i <= rand; i++)
        {
            monsters[i] = Instantiate(monsters[i]);
            Debug.Log(monsters[i].HP);
            monstersHP += monsters[i].HP;
        }
    }
    private void MonsterAtk()
    {
        Debug.Log("Monster's attack");
        player.HP -= 25;// 30 -> monster.atk
    }
}
