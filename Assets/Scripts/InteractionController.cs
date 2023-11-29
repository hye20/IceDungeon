using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    PlayerController playerController;
    DialogueManager dialogueM;


    [Header("이름 창")]
    [SerializeField] GameObject go_npcName;
    [SerializeField] Text txt_TargetName;

    string npcChildName = "Quset";
    bool isContact = false;

    [Header("Npc 접속 가능 확인")]
    public bool npcInter;
    public static bool isCollide = false;

    // Npc 인식
    public GameObject trigObject;
    // Npc 상호 작용 가능한지 확인 하는 것
    GameObject ChildQ;

    void Start()
    {
        dialogueM = FindObjectOfType<DialogueManager>();
    }


    void CheckObject()
    {
        if (trigObject != null)
        { // 인식 하면
            Contact();
        }
        else
        { 
            // 인식 못 하면
            NotContact();
        }
    }

    private void Contact()
    {
        if (trigObject.transform.CompareTag("Interaction"))
        {
            txt_TargetName.text = trigObject.transform.GetComponent<InteractionType>().GetName();
            if (!isContact)
            {
                isContact = true;
                InteractionEvent tempEvent = trigObject.transform.GetComponent<InteractionEvent>();
                if(tempEvent != null)
                {
                    dialogueM.ShowDialogue(tempEvent.GetDialogues());

                }
                else
                {
                    Debug.Log("null");
                }
            } 
        }
        else
        {
            NotContact();
        }
    }

    private void NotContact()
    {
        if (isContact)
        {
            isContact = false;
        }
    }


    public void SettingUI(bool p_flag)
    {
        go_npcName.SetActive(p_flag);
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        // interaction을 tag로 소지한 객체의 충돌시 작동
        if (collision.gameObject.tag == "Interaction")
        {
            CheckObject();

            if (trigObject == null)
            {
                npcInter = true;

                Transform parentObject = collision.gameObject.transform.parent.parent;
                if (parentObject != null)
                {
                    trigObject = parentObject.gameObject;
                    ChildQ = trigObject.transform.Find(npcChildName).gameObject;
                    ChildQ.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interaction")
        {
            if (trigObject != null)
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
