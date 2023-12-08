using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "LU_Trigger")
        {
            GameManager.instance.player.controller.ArrowButtons[0].gameObject.SetActive(false);
        }
        else if (collision.name == "RU_Trigger")
        {
            GameManager.instance.player.controller.ArrowButtons[1].gameObject.SetActive(false);
        }
        else if (collision.name == "LD_Trigger")
        {
            GameManager.instance.player.controller.ArrowButtons[2].gameObject.SetActive(false);
        }
        else if (collision.name == "RD_Trigger")
        {
            GameManager.instance.player.controller.ArrowButtons[3].gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger")
        {
            GameManager.instance.player.controller.ArrowButtons[0].gameObject.SetActive(true);
        }
        else if (collision.name == "RU_Trigger")
        {
            GameManager.instance.player.controller.ArrowButtons[1].gameObject.SetActive(true);
        }
        else if (collision.name == "LD_Trigger")
        {
            GameManager.instance.player.controller.ArrowButtons[2].gameObject.SetActive(true);
        }
        else if (collision.name == "RD_Trigger")
        {
            GameManager.instance.player.controller.ArrowButtons[3].gameObject.SetActive(true);
        }
    }
}
