using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_ItemStack : MonoBehaviour
{
    [SerializeField] private GameObject _uiItemStackListItemPrefab;
    private Stack<UI_ItemStackListItem> _uiItemStackListItems = new Stack<UI_ItemStackListItem>();

    private void OnEnable()
    {
        EventHandler.DidInventoryChangeEvent += ConfigureUIForItemStack;
    }

    private void OnDisable()
    {
        EventHandler.DidInventoryChangeEvent -= ConfigureUIForItemStack;
    }

    private void ConfigureUIForItemStack()
    {
        // Clear existing ItemStack UI
        ClearItemStack();
        
        // Get inventory
        Stack<Item> inventory = GameManager.Instance.CharacterCheekPouches.GetCurrentInventory();

        // Recreate ItemStack UI from current value of inventory
        foreach (Item item in inventory)
        {
            InstantiateItemStackListItemForItem(item);
        }
    }

    private void ClearItemStack()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        _uiItemStackListItems.Clear();
    }

    private void DestroyTopItem()
    {
        // TODO: Don't destroy the entire stack every time
        Destroy(_uiItemStackListItems.Pop().gameObject);
    }

    private void InstantiateItemStackListItemForItem(Item item)
    {
        GameObject newGO = Instantiate(_uiItemStackListItemPrefab, gameObject.transform);
        UI_ItemStackListItem newScript = newGO.GetComponent<UI_ItemStackListItem>();
        newScript.ConfigureUIForItem(item);
        _uiItemStackListItems.Push(newScript);
    }

}