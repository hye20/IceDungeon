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

    private void Update()
    {
        
        TextCp();  
    }

    void TextCp()
    {
        if (isDialogue && interactionController.npcInter ==true)
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
        SettinUI(false);
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        interactionController.SettingUI(false);
    }

    // 글자가 들어가게 함
    // 글자의 색상도 들어감
    IEnumerator TypeWriter()
    {
        SettinUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("`", ",");
        // 엔터 기능 구현
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");

        txt_Name.text = dialogues[lineCount].name;
        // 글자색 인식
        bool t_white = false, t_yellow = false, t_cyan = false;
        // 특수 문자 사용 확인
        bool t_ignre = false;
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch (t_ReplaceText[i])
            {
                case 'ⓦ': t_white = true; t_yellow = false; t_cyan = false; t_ignre = true; break;
                case 'ⓨ': t_white = false; t_yellow = true; t_cyan = false; t_ignre = true; break;
                case 'ⓒ': t_white = false; t_yellow = false; t_cyan = true; t_ignre = true; break;
            }
            //색상 변화
            string t_letter = t_ReplaceText[i].ToString();

            if (!t_ignre)
            {
                if (t_white) { t_letter = "<color=#ffffff>" + t_letter + "</color>"; }
                else if (t_yellow) { t_letter = "<color=#ffff00>" + t_letter + "</color>"; }
                else if (t_cyan) { t_letter = "<color=#45dee3>" + t_letter + "</color>"; }
                txt_Dialogue.text += t_letter; 
            }
            t_ignre = false;
            yield return new WaitForSeconds(textDelay);
        }        
        isNext = true;
        yield return null;
    }

    void SettinUI(bool p_flag)
    {
        go_npcTalk.SetActive(p_flag);

        if (p_flag)
        {
            if (dialogues[lineCount].name == "")
            {
                interactionController.go_npcName.SetActive(false);
            }
            else
            {
                interactionController.go_npcName.SetActive(true);
                txt_Name.text = "";
            }
        }
        else
        {
            go_npcTalk.SetActive(false);
        }
    }
}
