using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorNumber;

    public void TryOpen()
    {
        PlayerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        if (inventory.HasItem(doorNumber))
        {
            inventory.RemoveItem(doorNumber);
            Destroy(gameObject);
        }
    }
}
