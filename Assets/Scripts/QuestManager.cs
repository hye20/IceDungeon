using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    Dictionary<int, QuestData> questList;

    public GameObject[] questObject;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을 사람들과 대화하기"
                                        , new int[] {1000, 2000}));
        questList.Add(20, new QuestData("Gm의 심부름 처리"
                                        , new int[] {3000, 1000 }));
        questList.Add(30, new QuestData("퀘스트 클리어!"
                                        , new int[] {0 }));

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

        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }

        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch(questId)
        {
            case 10:
                if(questActionIndex == questList[questId].npcId.Length)
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
        }
    }
}
