using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheekPouches : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private SphereCollider _pickupArea;
    [SerializeField] private Transform _itemParent;
    
    private void Start()
    {
        _inventory = new Inventory();
    }

    private void Update()
    {
        CheckForInteract();
        CheckForSpitUp();
        CheckForInventoryDebug();
    }
    
    public bool AttemptAddItem(Item itemToAdd)
    {
        return _inventory.Add(itemToAdd);
    }

    private void CheckForInteract()
    {
        if (Input.GetButtonDown("Fire1") == false) return;
        List<GameObject> foundInteractablesGOs = GetInteractableGameObjectsInPickupArea();
        if (foundInteractablesGOs == null) return;
        IInteractable interactable = GetClosestInteractable(foundInteractablesGOs);
        interactable.OnInteract();
    }

    private List<GameObject> GetInteractableGameObjectsInPickupArea()
    {
        Collider[] allColliders = Physics.OverlapSphere(_pickupArea.gameObject.transform.position, _pickupArea.radius);
        if (allColliders.Length <= 0) return null;

        List<GameObject> foundInteractablesGOs = new List<GameObject>();
        foreach (Collider collider in allColliders)
        {
            if (collider.gameObject.GetComponent<IInteractable>() != null) foundInteractablesGOs.Add(collider.gameObject);
        }
        
        if (foundInteractablesGOs.Count <= 0) return null;
        return foundInteractablesGOs;
    }

    private IInteractable GetClosestInteractable(List<GameObject> interactableGOsToSearch)
    {
        GameObject closestInteractableGO = null;
        float closestDistance = float.PositiveInfinity;
        
        foreach (GameObject thisInteractableGO in interactableGOsToSearch)
        {
            if (float.IsPositiveInfinity(closestDistance))
            {
                closestInteractableGO = thisInteractableGO;
                closestDistance =
                    Vector3.Distance(thisInteractableGO.transform.position, gameObject.transform.position);
                continue;
            }
            
            float thisDistance = Vector3.Distance(thisInteractableGO.transform.position, gameObject.transform.position);
            if (thisDistance < closestDistance)
            {
                closestDistance = thisDistance;
                closestInteractableGO = thisInteractableGO;
            }
        }
        
        return closestInteractableGO.GetComponent<IInteractable>();
    }

    private void CheckForSpitUp()
    {
        if (Input.GetButtonDown("Fire2") == false) return;
        if (_inventory.HasItems()) SpitUpItem();
    }

    private void SpitUpItem()
    {
        Item itemToSpitUp = _inventory.Pop();
        GameObject.Instantiate(itemToSpitUp.GetGameObject(), _pickupArea.gameObject.transform.position, Quaternion.identity,
            _itemParent);
    }
    
    private void CheckForInventoryDebug()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(_inventory.Print());
        }
    }
}
