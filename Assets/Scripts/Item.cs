using System;
using UnityEngine;

public class Item : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private PlayerInventory playerInventory;
    private MeshFilter meshFilter;
    
    public int ItemNumber; // temporary, maybe doors check for this?
    public Texture2D image;// can be changed to mesh if/when we do those
    
    public AudioClip itemPickupSound;
    
    // with this item can be placed anywhere on the map, and not be teleported to (0, y, 0)
    private Vector3 originalPosition;
    
    #region Animation Variables
    public float heightOffset = .5f;
    private float time = .0f;
    public float heightGlideScale = .2f;
    public float heightGlideSpeed = 1f;
    public float rotateSpeed = .2f;
    #endregion
    
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerInventory = player.GetComponent<PlayerInventory>();
        
        originalPosition = transform.position;
        transform.position = new Vector3(originalPosition.x, heightOffset, originalPosition.z);
        
        GetComponent<MeshRenderer>().sharedMaterials[0].mainTexture = image; // Change to "set mesh" if we have custom meshes
    }

    void Update()
    {
        if ((int)transform.position.x == (int)playerMovement.currentPosition.x ||
                (int)transform.position.z == (int)playerMovement.currentPosition.z) {
            time += Time.deltaTime;
            transform.position = new Vector3(originalPosition.x, heightOffset + Mathf.Sin(time * heightGlideSpeed) * heightGlideScale,originalPosition.z);
            transform.Rotate(Vector3.up, rotateSpeed);
        }
    }
    
    // we could use "CheckForItem", but this is better, no?
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFXOneShot(itemPickupSound);
            playerInventory.AddItem(this);
            Destroy(gameObject);
        }
    }
    
}
