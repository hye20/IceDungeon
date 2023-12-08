using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    Dictionary<int, QuestData> questList;


    // 퀘스트 할때 나타 났다 사라 졌다 할 Object
    public GameObject[] questObject;
    [Header("UI on/off")]
    public GameObject textBg;
    public GameObject btnBg;
    public bool choice = false;
    public int btnChoicNum = 0;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }
    
    void GenerateData()
    {
        questList.Add(10, new QuestData("게임 타이틀A", "Gm 만나기 \n Angel 만나기"
                                        , new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("게임 타이틀B", "Gm 만나로 가기"
                                        , new int[] { 3000, 1000 }));
        questList.Add(30, new QuestData("게임 타이틀C", "선택 퀘스트!"
                                        , new int[] { 1000, 1000 }));
        questList.Add(40, new QuestData("게임 타이틀D", "선택 퀘스트!"
                                        , new int[] { 1000, 1000 }));
        questList.Add(50, new QuestData("게임 타이틀E", "선택 퀘스트!"
                                       , new int[] { 1000, 1000 }));
        questList.Add(60, new QuestData("게임 타이틀F", "선택 퀘스트!"
                                       , new int[] { 1000, 1000 }));
        questList.Add(70, new QuestData("게임 타이틀G", "선택 퀘스트!"
                               , new int[] { 1000, 1000 }));
        questList.Add(80, new QuestData("게임 타이틀H", "선택 퀘스트!"
                       , new int[] { 1000, 1000 }));

        questList.Add(90, new QuestData("게임 타이틀I", "퀘스트 클리어!", new int[] { 0 }));

    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }


        // 퀘스트 활성화 시 활성화 되어야 할것
        ControlObject();

        // 대화 완료 + 다음 퀘스트
        if (questActionIndex >= questList[questId].npcId.Length && !choice)
        {
            NextQuest();
        }
        else if(choice)
        {
            ChoiceQuest(btnChoicNum==0?1:btnChoicNum);
        }

        UiActive();

        // 퀘스트 이름 출력 
        return questList[questId].questName;
    }

    public string CheckQuest()// 퀘스트 이름 출력 
    { return questList[questId].questName; }
    public String CheckQuestTitle()
    {
        return questList[questId].questTitle;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    public void ChoiceQuest(int number)
    {
        questId += 10*number;
        questActionIndex = 0;
        //ChoiceUISet(false); 
    }

    public void ChoiceUISet(bool p_flag)
    {
        textBg.gameObject.SetActive(!p_flag);
        btnBg.gameObject.SetActive(p_flag);
        choice = p_flag;
    }

    // 필요한 물품이나 오브젝트들을 생성 혹은 켜줄때
    void ControlObject()
    {
        switch (questId)
        {
            case 10:

                if (questActionIndex == questList[questId].npcId.Length)
                {
                    questObject[0].SetActive(true);
                }
                break;
            case 20:
                if (questActionIndex == questList[questId].npcId.Length)
                {
                    questObject[0].SetActive(false);
                }
                break;
            case 50:
                if (questActionIndex == questList[questId].npcId.Length)
                {
                    ChoiceQuest(2);
                }
                break;
            case 60:
                if (questActionIndex == questList[questId].npcId.Length)
                {
                    ChoiceQuest(2);
                }
                break;
            case 70:
                if (questActionIndex == questList[questId].npcId.Length)
                {
                    ChoiceQuest(2);
                }
                break;
        }
    }

    //다음에 필요한 것을 가져와줌
    void UiActive()
    {
        switch (questId)
        {
            case 30:
                if (questActionIndex == questList[questId].npcId.Length)
                {
                    ChoiceUISet(true);
                    InteractionController.instance.Action();
                }
                break;
            case 40:
                ChoiceUISet(true);
                if (questActionIndex == questList[questId].npcId.Length)
                {
                    ChoiceUISet(false);
                }
                break;
            case 50:
                ChoiceUISet(false);

                break;
            case 60:
                ChoiceUISet(false);
                break;

        }
    }
}