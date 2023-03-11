using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingHole : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        GameManager.Instance.CharacterHiding.Hide(gameObject.transform.position);
    }
}
