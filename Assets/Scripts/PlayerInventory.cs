using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public UI UI;
    public List<Item> _inventory = new List<Item>();

    public void AddItem(Item addition)
    {
        _inventory.Add(addition);
        Debug.Log("Item added");
        UI.InfoPopup($"Picked Up Key with number {addition.ItemNumber}");
        UI.UpdateInventory();
    }
    public void RemoveItem(int searchNumber)
    {
        Item removeItem = null;
        foreach (Item item in _inventory)
        {
            if (item.ItemNumber == searchNumber)
                removeItem = item;
        }
        if(removeItem  is not null)
        {
            _inventory.Remove(removeItem);
        UI.UpdateInventory();
        Debug.Log("Item removed");
        UI.InfoPopup($"Used Key with number {searchNumber}");
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