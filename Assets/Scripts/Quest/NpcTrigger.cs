using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcTrigger : MonoBehaviour
{
    InteractionType interactionType;
    public TextMeshPro NpcName;

    public Button NpcButton;


    void Start()
    {
        interactionType = GetComponent<InteractionType>();
        NpcName.text = interactionType.NpcName;
        NpcButton.interactable = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger" || collision.name == "RU_Trigger" || collision.name == "LD_Trigger" || collision.name == "RD_Trigger")
        {
            NpcName.GetComponent<TextMeshPro>().color = new Vector4(1, 1, 1, 1);
            NpcButton.interactable = true;
            NpcButton.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger" || collision.name == "RU_Trigger" || collision.name == "LD_Trigger" || collision.name == "RD_Trigger")
        {
            NpcName.GetComponent<TextMeshPro>().color = Color.clear;
            NpcButton.interactable = false;
            NpcButton.GetComponent<Image>().color = Color.clear;
        }
    }

    public void TalkNpc()
    {

    }
}
