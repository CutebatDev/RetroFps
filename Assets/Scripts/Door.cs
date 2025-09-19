using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorNumber;
    public AudioClip doorOpenSound;
    
    public void TryOpen()
    {
        PlayerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        if (inventory.HasItem(doorNumber))
        {
            AudioManager.Instance.PlaySFXOneShot(doorOpenSound);
            inventory.RemoveItem(doorNumber);
            Destroy(gameObject);
        }
    }
}
