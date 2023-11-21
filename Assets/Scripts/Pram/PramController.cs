using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class PramController : MonoBehaviour
{
    private float moveSpeed = 1.0f;

    public bool IsPlayerTurn;
    public Canvas ArrowCanvas;

    private Vector3 _luDirection;
    private Vector3 _ruDirection;
    private Vector3 _ldDirection;
    private Vector3 _rdDirection;

    private Vector3 _endPos;

    Animator _pramAnimator;

    public Button[] ArrowButtons = new Button[4];

    public bool LUButtonPressed = false;
    public bool RUButtonPressed = false;
    public bool LDButtonPressed = false;
    public bool RDButtonPressed = false;

    public int DiceCount;

    void Awake()
    {
        _luDirection = new Vector3(-0.5f, 0.25f, 0);
        _ruDirection = new Vector3(0.5f, 0.25f, 0);
        _ldDirection = new Vector3(-0.5f, -0.25f, 0);
        _rdDirection = new Vector3(0.5f, -0.25f, 0);

        _pramAnimator = GetComponent<Animator>();

        for(int i = 0; i < ArrowButtons.Length; i++)
        {
            int number = i;
            ArrowButtons[i].onClick.AddListener(() => OnButtonClicked(number));
        }
    }
    
    void Update()
    {
        PlayerTurn();
    }

    void PlayerTurn()
    {
        if(IsPlayerTurn)
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
            _pramAnimator.SetTrigger("LU_Trigger");
            _pramAnimator.SetBool("Move_LU", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                LUButtonPressed = false;
                _pramAnimator.ResetTrigger("LU_Trigger");
                _pramAnimator.SetBool("Move_LU", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }
        else if (RUButtonPressed)
        {
            _pramAnimator.SetTrigger("RU_Trigger");
            _pramAnimator.SetBool("Move_RU", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                RUButtonPressed = false;
                _pramAnimator.ResetTrigger("RU_Trigger");
                _pramAnimator.SetBool("Move_RU", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }
        else if (LDButtonPressed)
        {
            _pramAnimator.SetTrigger("LD_Trigger");
            _pramAnimator.SetBool("Move_LD", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                LDButtonPressed = false;
                _pramAnimator.ResetTrigger("LD_Trigger");
                _pramAnimator.SetBool("Move_LD", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }
        else if (RDButtonPressed)
        {
            _pramAnimator.SetTrigger("RD_Trigger");
            _pramAnimator.SetBool("Move_RD", true);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (transform.position == _endPos)
            {
                RDButtonPressed = false;
                _pramAnimator.ResetTrigger("RD_Trigger");
                _pramAnimator.SetBool("Move_RD", false);
                transform.position = _endPos;

                DiceCount--;
                ArrowCanvas.gameObject.SetActive(true);
            }
        }

        if (_pramAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move_LU") ||
            _pramAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move_RU") ||
            _pramAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move_LD") ||
            _pramAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move_RD"))
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
            _objectColor.a = 0.6f;
            collision.gameObject.GetComponentInParent<SpriteRenderer>().color = _objectColor;

            transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
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

            transform.GetComponent<SpriteRenderer>().sortingOrder = 2;
            //collision.gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }
}
