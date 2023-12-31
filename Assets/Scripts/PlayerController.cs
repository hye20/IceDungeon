using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public bool isStart = true;

    /*************Quest Mode***************/
    private float moveSpeed = 1.0f;

    public ItemManager ItemManager;
    public SpriteRenderer ItemSpriteRenderer;
    PenguinStarter penguinStarter;
    DiceManager diceManager;

    public bool IsPlayerTurn;
    public Canvas ArrowCanvas;

    private Vector3 _luDirection;
    private Vector3 _ruDirection;
    private Vector3 _ldDirection;
    private Vector3 _rdDirection;

    private Vector3 _endPos;

    public Animator animator;
    public Animator FaderAnimator;

    public Button[] ArrowButtons = new Button[4];

    public bool LUButtonPressed = false;
    public bool RUButtonPressed = false;
    public bool LDButtonPressed = false;
    public bool RDButtonPressed = false;

    public Canvas StatusCanvas;

    /*
    public GameObject SettingButton;
    public GameObject HelpButton;
    public GameObject QuestPanel;
    public GameObject DicePanel;
    */

    public int DiceCount;//�ൿ��

    /*************Battle Mode****************/
    public bool is_Attack;
    public bool is_Magic;
    public bool is_Victory;
    public bool is_Guard;

    void Awake()
    {
        DiceCount = 0;
        FaderAnimator = GameObject.Find("Fader").GetComponent<Animator>();

        /*
        SettingButton = GameObject.Find("UICanvas").transform.Find("Setting_Button").gameObject;
        HelpButton = GameObject.Find("UICanvas").transform.Find("Help_Button").gameObject;
        QuestPanel = GameObject.Find("UICanvas").transform.Find("Quest_Panel").gameObject;
        DicePanel = GameObject.Find("UICanvas").transform.Find("Dice_Panel").gameObject;
        */

        _luDirection = new Vector3(-0.5f, 0.25f, 0);
        _ruDirection = new Vector3(0.5f, 0.25f, 0);
        _ldDirection = new Vector3(-0.5f, -0.25f, 0);
        _rdDirection = new Vector3(0.5f, -0.25f, 0);

        animator = GetComponent<Animator>();

        ItemSpriteRenderer = transform.Find("ItemSprite").GetComponent<SpriteRenderer>();
        penguinStarter = GameObject.FindWithTag("Penguin").GetComponent<PenguinStarter>();
        diceManager = GameObject.Find("DiceManager").GetComponent<DiceManager>();

        for (int i = 0; i < ArrowButtons.Length; i++)
        {
            int number = i;
            ArrowButtons[i].onClick.AddListener(() => OnButtonClicked(number));
        }
    }

    void Update()
    {
        //�����ʿ�
        if (GameManager.instance.mode == GameManager.Mode.QuestMode)
        {
            if (SceneManager.GetActiveScene().name == "DKTest")
            {
                ItemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
            }
        }

            if (penguinStarter.penguinReturn)
        {
            StartCoroutine(PlayerStarter());
        }

        Starter();
        PlayerTurn();
        ItemObtained();
    }

    void Starter()
    {
        if (isStart)
        {
            animator.SetBool("Falling", true);

            Vector3 endPos = new Vector3(0.5f, 0, 0);

            transform.position = Vector3.MoveTowards(transform.position, endPos, 2.0f * Time.deltaTime);

            if (transform.position == endPos)
            {
                animator.SetBool("Falling", false);
                animator.SetBool("Lying", true);
            }
        }
    }

    void PlayerTurn()
    {
        if (IsPlayerTurn)
        {
            ArrowCanvas.gameObject.SetActive(true);
        }
        else
        {
            ArrowCanvas.gameObject.SetActive(false);
        }
        Move();

        AttackAnim();
        MagicAnim();
        VictoryAnim();

        if (DiceCount == 0)
        {
            IsPlayerTurn = false;
        }
    }

    void Move()
    {
        if (LUButtonPressed)
        {
            animator.SetTrigger("LU_Trigger");
            animator.SetBool("Move_LU", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                LUButtonPressed = false;
                animator.ResetTrigger("LU_Trigger");
                animator.SetBool("Move_LU", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }
        else if (RUButtonPressed)
        {
            animator.SetTrigger("RU_Trigger");
            animator.SetBool("Move_RU", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                RUButtonPressed = false;
                animator.ResetTrigger("RU_Trigger");
                animator.SetBool("Move_RU", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }
        else if (LDButtonPressed)
        {
            animator.SetTrigger("LD_Trigger");
            animator.SetBool("Move_LD", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                LDButtonPressed = false;
                animator.ResetTrigger("LD_Trigger");
                animator.SetBool("Move_LD", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }
        else if (RDButtonPressed)
        {
            animator.SetTrigger("RD_Trigger");
            animator.SetBool("Move_RD", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                RDButtonPressed = false;
                animator.ResetTrigger("RD_Trigger");
                animator.SetBool("Move_RD", false);
                transform.position = _endPos;

                DiceCount--;                
                ArrowCanvas.gameObject.SetActive(true);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Move_LU") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Move_RU") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Move_LD") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Move_RD"))
        {
            ArrowCanvas.gameObject.SetActive(false);
        }
    }

    public void BattleMode()
    {
        animator.SetTrigger("RU_Trigger");
        IsPlayerTurn = false;
    }
    public void QuestMode()
    {
        animator.ResetTrigger("RU_Trigger");
        IsPlayerTurn = true;
    }

    public void AttackAnim()
    {
        if (is_Attack) animator.SetBool("Attack", true);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_RU") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("Attack", false);
            is_Attack = false;
        }
    }
    public void MagicAnim()
    {
        if (is_Magic) animator.SetBool("Magic", true);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("M_Attack_RU") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("Magic", false);
            is_Magic = false;
        }
    }
    public void VictoryAnim()
    {
        if (is_Victory) animator.SetBool("Victory", true);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Victory1") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("Victory", false);
            is_Victory = false;
        }
    }

    void OnButtonClicked(int number)
    {
        switch (number)
        {
            case 0:
                _endPos = transform.position + _luDirection;
                LUButtonPressed = true;
                break;
            case 1:
                _endPos = transform.position + _ruDirection;
                RUButtonPressed = true;
                break;
            case 2:
                _endPos = transform.position + _ldDirection;
                LDButtonPressed = true;
                break;
            case 3:
                _endPos = transform.position + _rdDirection;
                RDButtonPressed = true;
                break;
        }
    }

    void ItemObtained()
    {
        if(ItemManager.obtainItem)
        {
            StartCoroutine(ItemSpriteActive());

            ItemManager.obtainItem = false;

            animator.SetTrigger("Obtain_Trigger");
            ArrowCanvas.gameObject.SetActive(false);

            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Obtain_Trigger") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                animator.SetTrigger("Obtain_Trigger");
                ArrowCanvas.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator PlayerStarter()
    {
        if (SceneManager.GetActiveScene().name == "DKTest"&& isStart)
        {
            if (penguinStarter.penguinReturn)
            {
                yield return new WaitForSeconds(3.0f);

                FaderAnimator.Play("FadeIn");

                yield return new WaitForSeconds(3.0f);

                animator.SetBool("Lying", false);
                GameObject startCamera = GameObject.Find("StartCamera");
                Destroy(startCamera);
                isStart = false;

                penguinStarter.penguinReturn = false;

                StatusCanvas.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator ItemSpriteActive()
    {
        yield return new WaitForSeconds(0.4f);

        ItemSpriteRenderer.sprite = ItemManager.Items[ItemManager.Items.Count - 1].ItemSprite;

        yield return new WaitForSeconds(0.5f);
        
        ItemSpriteRenderer.sprite = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlurObject")
        {
            Color _objectColor = collision.gameObject.GetComponentInParent<SpriteRenderer>().color;
            _objectColor.a = 0.2f;
            collision.gameObject.GetComponentInParent<SpriteRenderer>().color = _objectColor;

            //transform.GetComponent<SpriteRenderer>().sortingOrder = collision.gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder;
            //collision.gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlurObject")
        {
            Color _objectColor = collision.gameObject.GetComponentInParent<SpriteRenderer>().color;
            _objectColor.a = 1.0f;
            collision.gameObject.GetComponentInParent<SpriteRenderer>().color = _objectColor;

            //transform.GetComponent<SpriteRenderer>().sortingOrder = collision.gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder;
            //collision.gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }
}
