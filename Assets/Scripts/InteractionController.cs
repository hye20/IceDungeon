using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    public static InteractionController instance;
    public GManager gManager;
    
    [Header("이름 창")]
    public GameObject go_npcTalk;
    public GameObject go_npcTalkImg;
    [SerializeField] Text txt_TargetName;
    [SerializeField] Text txt_TargetTalk;

    string npcChildName = "Quset";


    [Header("Npc 접속 가능 확인")]
    public bool npcInter = false;

    // Npc 인식
    public GameObject trigObject;
    // Npc 상호 작용 가능한지 확인 하는 것
    GameObject ChildQ;

    public void SettingUI(bool p_flag)
    {
        go_npcTalk.SetActive(p_flag);
        go_npcTalkImg.SetActive(p_flag);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&& npcInter == true)
        {
            Action();
        }
    }
    

    public void Action()
    {
        SettingUI(npcInter);
        gManager.Action(trigObject);
    }

    /// <summary>
    /// interaction을 tag로 소지한 객체의 collision 충돌시 작동
    /// trigObject 는 충돌한 Npc를 gameObject로 인식을 함
    /// ChildQ는 gameObject로 이미지 값을 가져와서 작업
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interaction")
        {
            if (trigObject == null)
            {
                npcInter = true;
                Transform toq = collision.gameObject.transform.parent.parent;

                if (toq!=null)  
                {
                    trigObject = toq.gameObject;

                    ChildQ = toq.Find(npcChildName).gameObject;
                    ChildQ.SetActive(true);
                }
                else
                {
                    Debug.Log("not toq:" + toq);
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interaction")
        {
            if (trigObject != null && npcInter == true)
            {
                npcInter = false;
                trigObject = null;
                ChildQ.SetActive(false);
                ChildQ = null;
                Debug.Log("Npc 접촉 해제");
            }
        }
    }
}
