using System;
using UnityEngine;

public class Item : MonoBehaviour
{

    public PlayerMovement playerMovement;
    private MeshFilter meshFilter;
    

    #region Animation Variables
    public float heightOffset = 0.5f;
    private float time = .0f;
    public float heightGlideScale = .2f;
    public float heightGlideSpeed = 1f;
    public float rotateSpeed = .2f;
    #endregion
    
    void Start()
    {
        transform.position = new Vector3(0, heightOffset, 0);
    }

    void Update()
    {
        if ((int)transform.position.x == (int)playerMovement.currentPosition.x ||
                (int)transform.position.z == (int)playerMovement.currentPosition.z) {
            time += Time.deltaTime;
            transform.position = new Vector3(0, heightOffset + Mathf.Sin(time * heightGlideSpeed) * heightGlideScale,0);
            transform.Rotate(Vector3.up, rotateSpeed);
        }
    }
    
}
