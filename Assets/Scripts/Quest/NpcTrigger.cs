using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcTrigger : MonoBehaviour
{
    // Quest game Manager
    GManager gManager;
    InteractionController interactionController;
    
    // interactionType 가 소지하고있는 이름 혹은 그 값들
    InteractionType interactionType;
    public GameObject player;
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
            player = collision.gameObject.transform.parent.parent.gameObject;
            interactionController = player.GetComponent<InteractionController>();
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
        interactionController.Action();
    }
}
