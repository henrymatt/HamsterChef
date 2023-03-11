using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory
{
    private Stack<Item> _inventory = new Stack<Item>();
    private const int MAXINVENTORYSIZE = 9;
    
    public bool Add(Item itemToAdd)
    {
        if (_inventory.Count >= MAXINVENTORYSIZE) return false;
        
        _inventory.Push(itemToAdd);
        return true;
    }

    public Item Pop() => _inventory.Count <= 0 ? null : _inventory.Pop();

    public string Print()
    {
        string stringBuilder = ""; 
        foreach (Item item in _inventory)
        {
            stringBuilder += item.GetName() + " ";
        }

        return stringBuilder;
    }

    public bool HasItems() => _inventory.Count > 0;

}
