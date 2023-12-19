using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionType : MonoBehaviour
{
    public bool isObject;
    public bool isNpc;
    public bool isItem;
    public bool isPlayerTb;

    public string NpcName;
    public int id;

    public string GetName()
    {
        return NpcName;
    }

    public int GetIndex()
    {
        return id;
    }
}
