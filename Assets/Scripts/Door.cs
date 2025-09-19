using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorNumber;
    public AudioClip doorOpenSound;
    
    [SerializeField] private GameObject doorObject;
    
    [HideInInspector] public bool isAnimating = false; // Public in case we want to block player when opening
    public bool openInOtherDirection = false;
    [SerializeField] private float rotationSpeedDegreesPerSecond = 45.0f;
    [SerializeField] private float angleDegreesToConsiderClosed = .1f;
    private Quaternion targetRotation;
    public Vector3 axis = Vector3.up;
    
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
            StartRotation();
            //Destroy(gameObject);
        }
    }

    private void StartRotation()
    {
        float degrees = openInOtherDirection ? 90f : -90f;
        targetRotation = doorObject.transform.rotation * Quaternion.AngleAxis(degrees, axis);
        isAnimating = true;
    }
    

    void Update()
    {
        if (!isAnimating)
            return;
        
        doorObject.transform.rotation = Quaternion.RotateTowards(
            doorObject.transform.rotation,
            targetRotation,
            rotationSpeedDegreesPerSecond * Time.deltaTime
        );
        
        if (Quaternion.Angle(doorObject.transform.rotation, targetRotation) <= angleDegreesToConsiderClosed) {
            doorObject.transform.rotation = targetRotation; 
            isAnimating = false;
        }
    }
}
