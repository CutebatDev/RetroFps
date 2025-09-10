using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public UI UI;
    public List<Item> _inventory = new List<Item>();

    public void AddItem(Item addition)
    {
        if (_inventory.Count < 5)
        {
            _inventory.Add(addition);
            Debug.Log("Item added");
            UI.UpdateInventory();
        }
        else
        {
            // "replace item" menu?
        }
    }

    // check if player has an item in inventory
    public bool HasItem(int searchNumber)
    {
        foreach (Item item in _inventory)
        {
            if(item.ItemNumber == searchNumber)
                return true;
        }
        return false;
    }
    
}