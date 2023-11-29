using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionType : MonoBehaviour
{
    public bool isObject;
    public bool Npc;
    public bool Item;

    [SerializeField] string interactionName;

    public string GetName()
    {
        return interactionName;
    }
}
