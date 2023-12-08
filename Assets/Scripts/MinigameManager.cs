using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    GameObject player;
    bool restartButtonPressed;

    public Animator FaderAnimator;

    public GameObject CrackedIcePrefab;

    public Transform Stage1RestartPos;
    public Transform Stage2RestartPos;
    public Transform Stage3RestartPos;

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

    public List<GameObject> IceCracked = new List<GameObject>();

    void Awake()
    {
        FaderAnimator.Play("FadeIn");

        restartButtonPressed = false;

        player = GameObject.Find("Mao");
    }

    void Update()
    {
        CountIceCrakced();
        RestartIce();

        if(IceCracked.Count <= 0)
        {
            restartButtonPressed = false;
        }

        if(FaderAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut") && FaderAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            restartButtonPressed = true;
            FaderAnimator.Play("FadeIn");
        }
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

    void RestartIce()
    {
        if(restartButtonPressed)
        {
            for (int i = 0; i < IceCracked.Count; i++)
            {
                GameObject newIceCraked = Instantiate(CrackedIcePrefab, IceCracked[i].gameObject.transform.position, Quaternion.identity);
                newIceCraked.transform.parent = GameObject.Find("Cracks").transform;
                Destroy(IceCracked[i].gameObject);                

                IceCracked.Remove(IceCracked[i]);
            }

            if (IceCount < 25)
            {
                IceCount = 0;

                player.transform.position = Stage1RestartPos.position;
                player.GetComponent<PlayerController>().animator.Play("Idle_RU");
            }
            else if(IceCount >= 25 || IceCount < 65)
            {
                IceCount = 25;

                player.transform.position = Stage2RestartPos.position;
                player.GetComponent<PlayerController>().animator.Play("Idle_RU");
            }
            else if(IceCount >= 65 || IceCount < 143)
            {
                IceCount = 65;

                player.transform.position = Stage3RestartPos.position;
                player.GetComponent<PlayerController>().animator.Play("Idle_RU");
            }
        }
    }

    public void RestartButton()
    {        
        FaderAnimator.Play("FadeOut");
    }
}
