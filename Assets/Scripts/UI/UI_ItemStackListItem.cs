using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_ItemStackListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;

    public void ConfigureUIForItem(Item item)
    {
        _itemName.text = item.GetName();
    }
}
