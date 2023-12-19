using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "ScriptableObject/Item Data", order = int.MaxValue)]
public class ItemDatabase : ScriptableObject
{
    [SerializeField]
    private Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; } }

    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField]
    private string itemDescription;
    public string ItemDescription { get { return itemDescription; } }
}
