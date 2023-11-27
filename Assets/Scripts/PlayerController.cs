using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 1.0f;

    public bool IsPlayerTurn;
    public Canvas ArrowCanvas;

    private Vector3 _luDirection;
    private Vector3 _ruDirection;
    private Vector3 _ldDirection;
    private Vector3 _rdDirection;

    private Vector3 _endPos;

    Animator _animator;

    public Button[] ArrowButtons = new Button[4];

    public bool LUButtonPressed = false;
    public bool RUButtonPressed = false;
    public bool LDButtonPressed = false;
    public bool RDButtonPressed = false;

    [Header("Dice")]
    public Button diceButton;
    public Text diceText;

    // 다이스에서 나온 숫자
    public int maxDiceCount;
    public int DiceCount;

    void Awake()
    {
        _luDirection = new Vector3(-0.5f, 0.25f, 0);
        _ruDirection = new Vector3(0.5f, 0.25f, 0);
        _ldDirection = new Vector3(-0.5f, -0.25f, 0);
        _rdDirection = new Vector3(0.5f, -0.25f, 0);

        _animator = GetComponent<Animator>();

        for(int i = 0; i < ArrowButtons.Length; i++)
        {
            int number = i;
            ArrowButtons[i].onClick.AddListener(() => OnButtonClicked(number));
        }
    }

    private void Update()
    {
        PlayerTurn();
    }

    #region PlayerAnim테스트
    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void Mattack()
    {
        _animator.SetTrigger("MAttack");
    }

    public void Guard()
    {
        _animator.SetTrigger("Guard");
    }

    public void Battle()
    {
        _animator.SetTrigger("Battle");
    }

    public void Faill()
    {
        _animator.SetTrigger("Falling");
    }

    public void Win()
    {
        _animator.SetTrigger("Win");
    }
    #endregion
    public void RandomDice()
    {
        if (DiceCount != 0 ||
            ArrowCanvas.gameObject.activeSelf == true ||
            IsPlayerTurn == true)
        {
            return;
        }

        IsPlayerTurn = true;
        DiceCount = Random.Range(1, maxDiceCount+1);
        diceText.text = DiceCount.ToString();

        PlayerTurn();
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

        PramMove();

        if(DiceCount == 0)
        {
            IsPlayerTurn = false;
        }
    }


    void PramMove()
    {
        if (LUButtonPressed)
        {
            _animator.SetTrigger("LU_Trigger");
            _animator.SetBool("Move_LU", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                LUButtonPressed = false;
                _animator.ResetTrigger("LU_Trigger");
                _animator.SetBool("Move_LU", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }
        else if (RUButtonPressed)
        {
            _animator.SetTrigger("RU_Trigger");
            _animator.SetBool("Move_RU", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                RUButtonPressed = false;
                _animator.ResetTrigger("RU_Trigger");
                _animator.SetBool("Move_RU", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }
        else if (LDButtonPressed)
        {
            _animator.SetTrigger("LD_Trigger");
            _animator.SetBool("Move_LD", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                LDButtonPressed = false;
                _animator.ResetTrigger("LD_Trigger");
                _animator.SetBool("Move_LD", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }
        else if (RDButtonPressed)
        {
            _animator.SetTrigger("RD_Trigger");
            _animator.SetBool("Move_RD", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                RDButtonPressed = false;
                _animator.ResetTrigger("RD_Trigger");
                _animator.SetBool("Move_RD", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Move_LU") ||
            _animator.GetCurrentAnimatorStateInfo(0).IsName("Move_RU") ||
            _animator.GetCurrentAnimatorStateInfo(0).IsName("Move_LD") ||
            _animator.GetCurrentAnimatorStateInfo(0).IsName("Move_RD"))
        {
            ArrowCanvas.gameObject.SetActive(false);
        }
    }    

    void OnButtonClicked(int number)
    {
        switch(number)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BlurObject")
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
        if(collision.gameObject.tag == "BlurObject")
        {
            Color _objectColor = collision.gameObject.GetComponentInParent<SpriteRenderer>().color;
            _objectColor.a = 1.0f;
            collision.gameObject.GetComponentInParent<SpriteRenderer>().color = _objectColor;

            //transform.GetComponent<SpriteRenderer>().sortingOrder = collision.gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder;
            //collision.gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }
}
