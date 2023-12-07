using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTrigger : MonoBehaviour
{
    public TextMeshPro ItemName;

    public Button ItemButton;

    public ItemManager ItemManager;
    public ItemDatabase ItemDatabase;

    void Start()
    {
        ItemButton.interactable = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger" || collision.name == "RU_Trigger" || collision.name == "LD_Trigger" || collision.name == "RD_Trigger")
        {
            ItemName.GetComponent<TextMeshPro>().color = new Vector4(1, 1, 1, 1);
            ItemButton.interactable = true;
            ItemButton.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "LU_Trigger" || collision.name == "RU_Trigger" || collision.name == "LD_Trigger" || collision.name == "RD_Trigger")
        {
            ItemName.GetComponent<TextMeshPro>().color = Color.clear;
            ItemButton.interactable = false;
            ItemButton.GetComponent<Image>().color = Color.clear;
        }
    }

    public void ObtainItem()
    {
        ItemManager.obtainItem = true;        
        ItemManager.Items.Add(ItemDatabase);
        Destroy(gameObject);
    }
}
