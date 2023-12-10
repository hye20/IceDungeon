using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenguinTrigger : MonoBehaviour
{
    public Button PenguinButton;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger" || collision.name == "RU_Trigger" || collision.name == "LD_Trigger" || collision.name == "RD_Trigger")
        {
            PenguinButton.interactable = true;
            PenguinButton.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger" || collision.name == "RU_Trigger" || collision.name == "LD_Trigger" || collision.name == "RD_Trigger")
        {
            PenguinButton.interactable = false;
            PenguinButton.GetComponent<Image>().color = Color.clear;
        }
    }
}
