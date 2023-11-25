using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    private PlayerController _controller;

    void Awake()
    {
        //_controller = GameObject.Find("Pram_F").GetComponent<PlayerController>();

        if(GameObject.FindGameObjectWithTag("Pram"))
        {
            _controller = GameObject.Find("Pram").GetComponent<PlayerController>();
        }
        else if(GameObject.FindGameObjectWithTag("Mao"))
        {
            _controller = GameObject.Find("Mao").GetComponent<PlayerController>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "LU_Trigger")
        {
            _controller.ArrowButtons[0].gameObject.SetActive(false);
        }
        else if (collision.name == "RU_Trigger")
        {
            _controller.ArrowButtons[1].gameObject.SetActive(false);
        }
        else if (collision.name == "LD_Trigger")
        {
            _controller.ArrowButtons[2].gameObject.SetActive(false);
        }
        else if (collision.name == "RD_Trigger")
        {
            _controller.ArrowButtons[3].gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger")
        {
            _controller.ArrowButtons[0].gameObject.SetActive(true);
        }
        else if (collision.name == "RU_Trigger")
        {
            _controller.ArrowButtons[1].gameObject.SetActive(true);
        }
        else if (collision.name == "LD_Trigger")
        {
            _controller.ArrowButtons[2].gameObject.SetActive(true);
        }
        else if (collision.name == "RD_Trigger")
        {
            _controller.ArrowButtons[3].gameObject.SetActive(true);
        }
    }
}
