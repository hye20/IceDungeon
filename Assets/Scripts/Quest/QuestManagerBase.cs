using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestManagerBase : MonoBehaviour
{
    protected int questId;
    protected int questActionIndex;
    protected Dictionary<int, QuestData> questList;

    public virtual int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public virtual string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        // 대화 완료 + 다음 퀘스트
        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }

        // 퀘스트 활성화 시 활성화되어야 할 것
        ControlObject();

        // 퀘스트 이름 출력 
        return questList[questId].questName;
    }

    public virtual string CheckQuest()
    {
        return questList[questId].questName;
    }

    protected virtual void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    protected abstract void ControlObject();
}
