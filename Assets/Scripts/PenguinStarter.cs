using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PenguinStarter : MonoBehaviour
{
    GameObject player;
    Animator animator;

    float moveSpeed = 1.0f;

    Vector3 startPos;
    Vector3 endPos;

    float animationTimer = 0;
    float timerSpeed = 1.5f;

    bool isPicking = false;
    public bool itemPicked = false;

    public SpriteRenderer PickedItem;
    public SpriteRenderer HaloLight;    

    public bool returned;
    public bool penguinReturn;

    public Animator FaderAnimator;

    PlayerController controller;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Mao") != null)
        {
            player = GameObject.FindGameObjectWithTag("Mao");
            controller = player.GetComponent<PlayerController>();
        }
        else if (GameObject.FindGameObjectWithTag("Pram"))
        {
            player = GameObject.FindGameObjectWithTag("Pram");
            controller = player.GetComponent<PlayerController>();
        }

        startPos = transform.position;
        endPos = transform.position - new Vector3(3.5f, 1.75f, 0);
        animator = GetComponent<Animator>();

        returned = true;
        penguinReturn = false;
    }

    void Update()
    {
        if (!GameManager.instance.player.controller.isStart && GameManager.instance.mode == GameManager.Mode.QuestMode)
        {
            Destroy(gameObject);
        }

        Movement();
        Picking();
        Spinning();
        Returning();
    }

    void Movement()
    {
        if(controller.animator.GetCurrentAnimatorStateInfo(0).IsName("Lying") && returned)
        {          
            transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
            animator.SetBool("Walking", true);

            if (transform.position == endPos)
            {
                animator.SetBool("Walking", false);
                returned = false;
            }
        }
    }

    void Picking()
    {
        if(!isPicking)
        {
            animationTimer = timerSpeed * Time.time;

            if (animationTimer > 10.0f)
            {
                animationTimer = 0;
                animator.SetTrigger("Picking");
                isPicking = true;
            }
        }

        if (isPicking && animator.GetCurrentAnimatorStateInfo(0).IsName(gameObject.name + "_Picking") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.ResetTrigger("Picking");
            animator.SetBool("Spinning", true);
            itemPicked = true;

            PickedItem.enabled = true;
            HaloLight.enabled = true;
        }
    }

    void Spinning()
    {
        if (itemPicked && animator.GetCurrentAnimatorStateInfo(0).IsName(gameObject.name + "_Spin") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            itemPicked = false;

            animator.SetBool("Spinning", false);
        }
    }

    void Returning()
    {
        if(!returned && animator.GetCurrentAnimatorStateInfo(0).IsName(gameObject.name + "_WalkR"))
        {
            PickedItem.enabled = false;
            HaloLight.enabled = false;

            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

            if(transform.position == startPos)
            {
                FaderAnimator.Play("FadeOut");

                penguinReturn = true;
                Destroy(gameObject);
            }
        }
    }
}
