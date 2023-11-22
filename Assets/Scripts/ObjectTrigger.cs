using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    private PramController _controller;

    void Awake()
    {
        _controller = GameObject.Find("Pram_F").GetComponent<PramController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "LU_Trigger")
        {
            _controller.ArrowButtons[0].interactable = false;
        }
        else if (collision.name == "RU_Trigger")
        {
            _controller.ArrowButtons[1].interactable = false;
        }
        else if (collision.name == "LD_Trigger")
        {
            _controller.ArrowButtons[2].interactable = false;
        }
        else if (collision.name == "RD_Trigger")
        {
            _controller.ArrowButtons[3].interactable = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger")
        {
            _controller.ArrowButtons[0].interactable = true;
        }
        else if (collision.name == "RU_Trigger")
        {
            _controller.ArrowButtons[1].interactable = true;
        }
        else if (collision.name == "LD_Trigger")
        {
            _controller.ArrowButtons[2].interactable = true;
        }
        else if (collision.name == "RD_Trigger")
        {
            _controller.ArrowButtons[3].interactable = true;
        }
    }
}
