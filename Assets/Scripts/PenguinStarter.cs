using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinStarter : MonoBehaviour
{
    GameObject player;
    Animator animator;

    float moveSpeed = 1.0f;
    Vector3 endPos;

    PlayerController controller;

    float animatorTimer = 0;
    float animatorSpeed = 1.0f;
    int animationLoopCount = 0;

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

        endPos = transform.position - new Vector3(3.5f, 1.75f, 0);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        Debug.Log(animatorTimer);
    }

    void Movement()
    {
        if(controller.animator.GetCurrentAnimatorStateInfo(0).IsName("Lying"))
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
            animator.SetBool("Walking", true);
        }

        if (transform.position == endPos)
        {
            transform.position = endPos;
            animator.SetBool("Walking", false);

            animatorTimer = animatorSpeed * Time.time;
        }

        if (animatorTimer > 7.0f)
        {
            animatorTimer = 0;
            animator.SetBool("Picking", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(gameObject.name + "_Picking") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            animator.SetBool("Picking", false);
        }
    }
}
