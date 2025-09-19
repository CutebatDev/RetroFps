using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorNumber;
    public AudioClip doorOpenSound;
    
    [SerializeField] private GameObject doorObject;
    
    public bool isAnimating = false;
    [SerializeField] private float openSpeed = 2.0f;
    [SerializeField] private float zDegreesClosed = .0f;
    [SerializeField] private float angleDegreesToConsiderClosed = 2.0f;
    
    public void TryOpen()
    {
        PlayerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        if (inventory.HasItem(doorNumber))
        {
            AudioManager.Instance.PlaySFXOneShot(doorOpenSound);
            inventory.RemoveItem(doorNumber);
            
            // TODO-hl
            // PLEASE LET ME KNOW IF THIS WORKS 
            // DONT WANT TO DESTROY THE OBJECT SO IT'S STILL VISIBLE
            gameObject.tag = "Untagged"; // So player movement doesn't detect it anymore
            isAnimating = true;
            //Destroy(gameObject);
        }
    }


    void Update()
    {
        if (!isAnimating)
            return;
        
        transform.Rotate(Vector3.up, openSpeed * Time.deltaTime);
        if (Math.Abs(transform.rotation.eulerAngles.z - zDegreesClosed) <= angleDegreesToConsiderClosed) {
            isAnimating = false;
        }
    }
}
