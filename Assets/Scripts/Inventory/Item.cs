using Unity.VisualScripting;
using UnityEngine;

public class Item
{
    private SO_Item _soItem;

    public Item(SO_Item soItem)
    {
        _soItem = soItem;
    }

    public string GetName() => _soItem.itemName;
    public GameObject GetGameObject() => _soItem.worldGO;
}