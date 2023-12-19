using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questTitle;
    public string questName;
    public int[] npcId;

    public QuestData(string title, string name, int[] npc)
    {
        questTitle = title;
        questName = name;
        npcId = npc;
    }

}