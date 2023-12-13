using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Transform[] _monterSpawnPoint;
    [SerializeField] private Monster[] _monsterData;
    [SerializeField] private Monster[] _monsters;//monsters instance

    [SerializeField] private UIPlayerState _playerStatus;
    [SerializeField] private UIMonster[] _monstersBar;
    [SerializeField] private GameObject[] _effectList;

    public bool playerTurn = true;
    private bool _monsters_is_dead = false;
    private bool _battle_end = false;

    public GameObject btnList;
    public Button attackBtn;
    public Button magicBtn;
    public Button runBtn;
    public Button exitBtn;

    public float delay;//for test

    AudioSource audioSource;
    public AudioClip VictoryClip;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        GameManager.instance.player.transform.position = _playerSpawnPoint.position;
        playerTurn = true;
        _monsters_is_dead = false;
        _battle_end = false;
        attackBtn.onClick.AddListener(SelectTarget);
        magicBtn.onClick.AddListener(PlayerMagic);
        runBtn.onClick.AddListener(PlayerRun);
        SetMonsters();
        exitBtn.onClick.AddListener(GameManager.instance.QuestPhase);
        exitBtn.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }
    /*****************************************
     전투의 진행
    1. 몬스터, 플레이어의 생존 여부 판정
        ㄴ 어느 한쪽이 전멸했다면 전투는 종료
    2. 턴의 시작은 항상 플레이어의 행동 선택 - btnList.setActive(true)
        ㄴ 선택 종료시 setActive(false) -> 플레이어의 추가입력 방지.
    3. 플레이어의 선택이 마무리가 되면 전투 참여자들의 Speed값에 따라 차례대로 공격
     ******************************************/
    void Update()
    {
        Check_Monsters_Dead();
        if (GameManager.instance.player.HP <= 0)
        {
            Debug.Log($"player HP : {GameManager.instance.player.HP}");
            Debug.Log("Battle phase End");
            PrintResult();
        }
        else if (_monsters_is_dead)
        {
            Debug.Log($"Monsters terminated : {_monsters_is_dead}");
            Debug.Log($"player HP : {GameManager.instance.player.HP}");
            Invoke("PrintResult",2.0f);
        }
    }

    
    public void SelectTarget()
    {
        btnList.gameObject.SetActive(false);
        for (int i=0;i< _monsters.Length;i++)
        {
            if (!_monsters[i].is_dead)_monsters[i].able_target = true;
        }
    }
    private void Stagger()
    {
        //몬스터 진동, 경직 함수 추가 예정
    }
    public void PlayerAction()
    {
        if (playerTurn == true)
        {
            btnList.gameObject.SetActive(true);
        }
    }
    public void PlayerAtk(int index)
    {
        SetPriority();
        for(int i=0;i< _monsters.Length; i++)
        {
            _monsters[i].able_target=false;
        }
        Invoke("ChangeTurn", 3.0f);
        //play atk anim
        GameManager.instance.player.controller.is_Attack = true;
        Instantiate(_effectList[0]).transform.position = _monsters[index].transform.position;
        _monsters[index].HP -= GameManager.instance.player.atk;
        Debug.Log("player's attack");
        _monstersBar[index].UpdateTatgetValue(_monsters[index].HP, _monsters[index].maxHP);
    }
    private void PlayerMagic()
    {
        SetPriority();
        btnList.gameObject.SetActive(false);
        Invoke("ChangeTurn", 3.0f);
        GameManager.instance.player.controller.is_Magic = true;
        Debug.Log("player's magic");
        for (int i = 0; i < _monsters.Length; i++)
        {
            if (!_monsters[i].is_dead)
            {
                _monsters[i].HP -= GameManager.instance.player.SP;
                Instantiate(_effectList[1]).transform.position = _monsters[i].transform.position;
                _monstersBar[i].UpdateTatgetValue(_monsters[i].HP, _monsters[i].maxHP);
            }
        }
    }
    private void SetPriority()
    {
        //1. interface - status를 만들어서 직접 참조하여 이를 기반으로 Queue를 구성한다.
        //2. Dictionary 형태로 구현하여 일일히 대조하여 Queue를 생성한다.
    }
    public void PrintResult()
    {
        if (GameManager.instance.player.HP <= 0)
        {
            Debug.Log("defeated");
        }
        else if (_monsters_is_dead && !_battle_end)
        {
            exitBtn.gameObject.SetActive(true);
            GameManager.instance.player.controller.is_Victory = true;
            _battle_end = true;

            audioSource.clip = VictoryClip;
            audioSource.PlayOneShot(VictoryClip);
        }
    }
    private void PlayerRun() 
    {
        if(Random.value>0.5)
        {
            Debug.Log("player is run");
            GameManager.instance.QuestPhase();
        }
        else
        {
            Debug.Log("player is failed run");
        }
    }
    
    public void ChangeTurn()
    {
        playerTurn = !playerTurn;
        Debug.Log($"Player Turn is {playerTurn} now.");
        if(playerTurn)
        {
            PlayerAction();
        }
        else
        {
            MonsterAction();
        }
    }

    private void SetMonsters()
    {
        int rand = Random.Range(1, 4);//1~3����
        _monsters = new Monster[rand];
        for (int i = 0; i < rand; i++)
        {
            _monsters[i] = Instantiate(_monsterData[0]);
            _monsters[i].index = i;
            _monstersBar[i].gameObject.SetActive(true);
            _monsters[i].transform.position = _monterSpawnPoint[i].position;
            Debug.Log(_monsters[i].transform.position);
            _monsters[i].gameObject.name = _monsterData[0].name + (i+1);
            _monstersBar[i].SetName(_monsters[i].gameObject.name);
        }
    }
    private bool Check_Monsters_Dead()
    {
        int deadMonsterCnt = 0;
        for (int i = 0; i < _monsters.Length; i++)
        {
            if (_monsters[i].is_dead)
            {
                deadMonsterCnt++;
            }
        }
        if (deadMonsterCnt == _monsters.Length)
        {
            _monsters_is_dead = true;
        }
        return _monsters_is_dead;
    }
    public void MonsterAction()
    {
        int randomAction;
        if (!playerTurn && !_monsters_is_dead)
        {
            for (int i = 0; i < _monsters.Length; i++)
            {
                randomAction = Random.Range(1, 3);//1~2
                if (randomAction == 1)
                {
                    MonsterAtk(_monsters[i]);
                    Instantiate(_effectList[0]).transform.position = GameManager.instance.player.transform.position;
                }
                else if (randomAction == 2)
                {
                    MonsterSkill(_monsters[i]);
                    Instantiate(_effectList[0]).transform.position = GameManager.instance.player.transform.position;
                }
                Invoke("ChangeTurn", 2.0f);
            }
        }
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
        monster.MonsterAnim("Attack");
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
        monster.MonsterAnim("Skill");
    }
}
