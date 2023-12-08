using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour
{
    public Button DoorButton;
    SpriteRenderer spriteRenderer;
    public Sprite DoorOpenSprite;

    bool doorOpened;

    ItemManager itemManager;

    Transform playerPos;
    public Animator FaderAnimator;

    void Start()
    {
        if(GameObject.Find("Mao"))
        {
            playerPos = GameObject.Find("Mao").transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();

        doorOpened = false;
    }

    void Update()
    {
        if (FaderAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut") && FaderAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            FaderAnimator.Play("FadeIn");
            playerPos.position = new Vector3(7.5f, 5, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger" || collision.name == "RU_Trigger" || collision.name == "LD_Trigger" || collision.name == "RD_Trigger")
        {
            DoorButton.interactable = true;
            DoorButton.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger" || collision.name == "RU_Trigger" || collision.name == "LD_Trigger" || collision.name == "RD_Trigger")
        {
            DoorButton.interactable = false;
            DoorButton.GetComponent<Image>().color = Color.clear;
        }
    }

    public void DoorButtonClikced()
    {
        if (!doorOpened)
        {
            if (itemManager.Items == null)
                return;
            else
            {
                for (int i = 0; i < itemManager.Items.Count; i++)
                {
                    if (itemManager.Items[i].ItemName == "Key")
                    {
                        spriteRenderer.sprite = DoorOpenSprite;
                        itemManager.Items.RemoveAt(i);
                        doorOpened = true;
                    }
                }
            }
        }
        else
        {
            FaderAnimator.Play("FadeOut");
        }
    }
}
