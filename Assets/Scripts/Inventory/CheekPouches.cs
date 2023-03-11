using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CheekPouches : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private SphereCollider _pickupArea;
    [SerializeField] private Transform _itemParent;
    private List<GameObject> _previouslyFocusedGameObjects;
    [SerializeField] private Material _outlineMaterial;
    
    private void Start()
    {
        _inventory = new Inventory();
        _previouslyFocusedGameObjects = new List<GameObject>();
    }

    private void Update()
    {
        HighlightInteractableItem();
        CheckForInteract();
        CheckForSpitUp();
        CheckForInventoryDebug();
    }
    
    public bool AttemptAddItem(Item itemToAdd)
    {
        return _inventory.Add(itemToAdd);
    }

    public Stack<Item> GetCurrentInventory()
    {
        return _inventory.GetInventory();
    }

    private void HighlightInteractableItem()
    {
        List<GameObject> foundInteractablesGOs = GetInteractableGameObjectsInPickupArea();
        if (foundInteractablesGOs == null)
        {
            ClearAllPreviouslyHighlightedInteractableItems();
            return;
        }
        GameObject interactableGameObject = getClosestInteractableGameObject(foundInteractablesGOs);
        if (_previouslyFocusedGameObjects.Contains(interactableGameObject)) return;

        ClearAllPreviouslyHighlightedInteractableItems();
        // add a material to existing materials
        Material[] existingMaterials = interactableGameObject.GetComponent<Renderer>().materials;
        Material[] materialsPlusOutline = new Material[existingMaterials.Length + 1];
        for (var i = 0; i < existingMaterials.Length; i++)
        {
            materialsPlusOutline[i] = existingMaterials[i];
        }
        materialsPlusOutline[materialsPlusOutline.Length - 1] = _outlineMaterial;
        interactableGameObject.GetComponent<Renderer>().materials = materialsPlusOutline;
        
        _previouslyFocusedGameObjects.Add(interactableGameObject);
    }

    private void ClearAllPreviouslyHighlightedInteractableItems()
    {
        if (_previouslyFocusedGameObjects.Count == 0) return;
        
        foreach (GameObject previouslyFocusedGameObject in _previouslyFocusedGameObjects)
        {
            // Remove outline material
            Material[] materialsWithOutline = previouslyFocusedGameObject.GetComponent<Renderer>().materials;
            Material[] materialsWithoutOutline = new Material[materialsWithOutline.Length - 1];
            for (var i = 0; i < materialsWithoutOutline.Length; i++)
            {
                materialsWithoutOutline[i] = materialsWithOutline[i];
            }
            previouslyFocusedGameObject.GetComponent<Renderer>().materials = materialsWithoutOutline;
        }
        _previouslyFocusedGameObjects.Clear();
    }

    private void CheckForInteract()
    {
        if (Input.GetButtonDown("Fire1") == false) return;
        List<GameObject> foundInteractablesGOs = GetInteractableGameObjectsInPickupArea();
        if (foundInteractablesGOs == null) return;
        IInteractable interactable = getClosestInteractableGameObject(foundInteractablesGOs).GetComponent<IInteractable>();
        ClearAllPreviouslyHighlightedInteractableItems();
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

    private GameObject getClosestInteractableGameObject(List<GameObject> interactableGOsToSearch)
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

        return closestInteractableGO;
    }

    private void CheckForSpitUp()
    {
        if (Input.GetButtonDown("Fire2") == false) return;
        if (_inventory.HasItems()) SpitUpItem();
    }

    private void SpitUpItem()
    {
        Item itemToSpitUp = _inventory.Pop();
        GameObject itemGO = GameObject.Instantiate(itemToSpitUp.GetGameObject(), _pickupArea.gameObject.transform.position, Quaternion.identity,
            _itemParent);
        Rigidbody itemRigidBody = itemGO.GetComponent<Rigidbody>();
        itemRigidBody.AddForce(GetComponent<CharacterMovement>().GetCharacterForwardVector() * 4f, ForceMode.Impulse);
    }
    
    private void CheckForInventoryDebug()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(_inventory.Print());
        }
    }
}
