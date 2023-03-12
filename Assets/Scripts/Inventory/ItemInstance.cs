using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance : MonoBehaviour, IInteractable
{
    [SerializeField] private SO_Item _soItem;

    public void OnInteract()
    {
        if (GameManager.Instance.CharacterCheekPouches.AttemptAddItem(new Item(_soItem)))
        {
            EventHandler.CallDidPickUpIngredientEvent();
            Destroy(gameObject);
        }
    }
}
