using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody rigidBody;
    
    [HideInInspector] public MovementManager movementManager;

    public UI UI;

    public GameObject mapPieces;
    public float movementLengthIncrement = 2.0f; // Dependent On The size of the map pieces themselves
    public Vector3 currentPosition;
    public float distanceToPointThreshold = 0.1f;
    public float distanceToConsiderMapPiece = 0.4f;
    public float moveSpeed = 20.0f;
    

    void Start()
    {
        // Very Important For Movement Snapping
        transform.position = new Vector3(0,1.0f,0);
        currentPosition = transform.position;
        
        movementManager = transform.GetComponent<MovementManager>();
        rigidBody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }


    void MoveToPosition(Vector3 position)
    {
        transform.position = Vector3.Lerp(transform.position, position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, position) <= distanceToPointThreshold) {
            transform.position = position;
        }
    }
    
    
    void HandleMovement()
    {
        if (transform.position != currentPosition) {
            MoveToPosition(currentPosition);
        }
        
        Vector3 newPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.W)) {
            newPosition = transform.position + transform.forward * movementLengthIncrement ; // remove movement length increment for previous version
        }
        
        if (newPosition != transform.position)
        {
            if (movementManager.currentStepsLeft <= 0)
                return;

            // Previous Version : 
            //rigidBody.MovePosition(newPosition);

            if (GetMapPieceAtPosition(newPosition) != null) {
                transform.position = currentPosition;
                currentPosition = newPosition;
                CheckForItem();
            }
            else {
                // We can use MoveToPosition to animate wall collision
                // We can also create a little message like in the video "ouch"
            }

            movementManager.currentStepsLeft--;
            UI.UpdateCounters();
        }

    }

    void HandleRotation()
    {
        Quaternion? newRotation = null;
        if (Input.GetKeyDown(KeyCode.A)) {
            newRotation = rigidBody.rotation * Quaternion.Euler(Vector3.up * -90);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            newRotation = rigidBody.rotation * Quaternion.Euler(Vector3.up * 90);
        }

        if (newRotation != null) {
            rigidBody.MoveRotation((Quaternion)newRotation);
            CheckForItem();
        }
    }


    public void CheckForItem()
    {
        Vector3 checkPosition = transform.position + transform.forward * movementLengthIncrement;
        GameObject checkObject = GetMapPieceAtPosition(checkPosition);
        if (checkObject) {
            // Check for all the children of the object,
            // If one of the children of the object has a Component of "Item" there is an item
            // If there is an item -> do item logic
        }
    }
    
    
    public GameObject GetMapPieceAtPosition(Vector3 position)
    {
        for (int i = 0; i < mapPieces.transform.childCount; i++)
        {
            Transform childTransform = mapPieces.transform.GetChild(i).transform;
            // if ((int)childTransform.position.x == (int)position.x &&
            //     (int)childTransform.position.z == (int)position.z) {
            if (Vec3DistanceNoHeight(childTransform.position,position) <= distanceToConsiderMapPiece){
                return  childTransform.GetChild(0).gameObject;
            }
            // Debug.Log($"{childTransform.name} : ({childTransform.position}) for : {position}");
            // Debug.Log($"    {(int)childTransform.position.x} == {(int)position.x} : {(int)childTransform.position.x == (int)position.x} && " +
            //           $"{(int)childTransform.position.z} == {(int)position.z} : {(int)childTransform.position.z == (int)position.z}");
        }

        Debug.Log("NO MAP PIECE FOUND AT : " + position.ToString());
        return null;
    }


    public float Vec3DistanceNoHeight(Vector3 p1, Vector3 p2)
    {
        // EXPENSIVE
        //return Vector2.Distance(new Vector2(p1.x,p1.z), new Vector2(p2.x,p2.z));
        
        // CHEAPER I GUESS
        float dx = p1.x - p2.x;
        float dz = p1.z - p2.z;
        return Mathf.Sqrt(dx * dx + dz * dz);
    }
    
}
