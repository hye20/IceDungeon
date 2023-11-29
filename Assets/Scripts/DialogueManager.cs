using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("대화창")]
    [SerializeField] GameObject go_npcTalk;

    [SerializeField] Text txt_Name;
    [SerializeField] Text txt_Dialogue;

    Dialogue[] dialogues;
    bool isDialogue = false; // 대화중이면 true
    bool isNext = false; // 특정 키 입력 대기

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0;
    int contextCount = 0;// 대사 카운트


    InteractionController interactionController;

    void Start()
    {
        interactionController = FindObjectOfType<InteractionController>();    
    }

    void Update()
    {
        TextCp();
    }

    void TextCp()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isNext = false;
                    txt_Dialogue.text = "";

                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());
                        }
                        else
                        {
                            EndDialogue();
                        }
                    }
                }
            }
        }
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";
        interactionController.SettingUI(true);
        dialogues = p_dialogues;

        StartCoroutine(TypeWriter());
    }

    void EndDialogue()
    {
        Debug.Log("EndDialogue");
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        interactionController.SettingUI(false);
        SettinUI(false);
    }

    IEnumerator TypeWriter()
    {
        SettinUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("`", ",");

        txt_Name.text = dialogues[lineCount].name;
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            txt_Dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }        
        isNext = true;
        yield return null;
    }

    void SettinUI(bool p_flag)
    {
        go_npcTalk.SetActive(p_flag);
    }
}
