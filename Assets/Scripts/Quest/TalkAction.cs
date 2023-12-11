using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkAction : MonoBehaviour
{
    InteractionController interactionController;

    private void Awake()
    {
        interactionController = GameObject.Find("Parm")?.GetComponent<InteractionController>();

        if (interactionController == null)
        {
            interactionController = GameObject.Find("Mao")?.GetComponent<InteractionController>();
        }
    }

    public void Action()
    {
        interactionController.Action();
    }
}
