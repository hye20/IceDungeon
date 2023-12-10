using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestTrigger : MonoBehaviour
{
    public Button ChestButton;
    SpriteRenderer spriteRenderer;
    public Sprite ChestOpenSprite;

    [SerializeField]
    ItemManager itemManager;
    public ItemDatabase ItemDatabase;

    AudioSource audioSource;
    void Start()
    {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(itemManager.obtainItem)
        {
            ChestButton.GetComponent<Image>().color = Color.clear;
        }

        if (collision.name == "LU_Trigger" || collision.name == "RU_Trigger" || collision.name == "LD_Trigger" || collision.name == "RD_Trigger")
        {
            ChestButton.interactable = true;
            ChestButton.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger" || collision.name == "RU_Trigger" || collision.name == "LD_Trigger" || collision.name == "RD_Trigger")
        {
            ChestButton.interactable = false;
            ChestButton.GetComponent<Image>().color = Color.clear;
        }
    }

    public void ChestClicked()
    {
        audioSource.Play();

        itemManager.obtainItem = true;
        itemManager.Items.Add(ItemDatabase);
        spriteRenderer.sprite = ChestOpenSprite;
        ChestButton.gameObject.SetActive(false);
    }
}
