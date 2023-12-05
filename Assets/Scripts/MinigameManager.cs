using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    GameObject player;

    public Transform Stage1RestartPos;
    public Transform Stage2RestartPos;
    //public Transform Stage3RestartPos;

    public int IceCount;

    [Header("Stage1")]
    public Animator Stage1ClearIceAnim1;
    public Animator Stage1ClearIceAnim2;

    [Header("Stage2")]
    public Animator Stage2ClearIceAnim1;
    public Animator Stage2ClearIceAnim2;

    [Header("Stage3")]
    public Animator Stage3ClearIceAnim1;
    public Animator Stage3ClearIceAnim2;

    void Awake()
    {
        player = GameObject.Find("Mao");
    }

    void Update()
    {
        CountIceCrakced();
    }

    void CountIceCrakced()
    {
        if(IceCount == 25)
        {
            Stage1ClearIceAnim1.SetBool("Clear", true);
            Stage1ClearIceAnim2.SetBool("Clear", true);
        }

        if(IceCount == 65)
        {
            Stage2ClearIceAnim1.SetBool("Clear", true);
            Stage2ClearIceAnim2.SetBool("Clear", true);
        }

        if(IceCount == 143)
        {
            Stage3ClearIceAnim1.SetBool("Clear", true);
            Stage3ClearIceAnim2.SetBool("Clear", true);
        }
    }

    public void RestartButton()
    {
        if(IceCount < 25)
        {
            player.transform.position = Stage1RestartPos.position;
            player.GetComponent<PlayerController>().animator.Play("Idle_RU");
        }
        else if(IceCount <= 25 || IceCount < 65)
        {
            player.transform.position = Stage2RestartPos.position;
            player.GetComponent<PlayerController>().animator.Play("Idle_RU");
        }
    }
}
