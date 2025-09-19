using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody rigidBody;
    
    [HideInInspector] public MovementManager movementManager;

    public UI UI;

    public GameObject mapPieces;
    public float movementLengthIncrement = 2.0f; // Dependent On The size of the map pieces themselves
    public float distanceToPointThreshold = .1f;
    public float distanceToConsiderMapPiece = .4f;
    public float moveSpeed = 20.0f;
    public float rotationSpeed = 20.0f;
    public float angleToConsiderRotated = .1f;
    
    public Vector3 currentPosition;
    public Quaternion currentRotation;
    
    private AudioManager audioManager;
    
    void Start()
    {
        // Very Important For Movement Snapping
        transform.position = new Vector3(0,1.0f,0);
        currentPosition = transform.position;
        currentRotation = transform.rotation;
        
        movementManager = transform.GetComponent<MovementManager>();
        rigidBody = GetComponent<Rigidbody>();
        
        audioManager = AudioManager.Instance;
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
            // Possibly cause incorrect movement placement / incrementation
            // PREVIOUSLY : transform.position = position
            transform.position = GetRoundedPosition(position);
        }
    }


    
    
    void HandleMovement()
    {
        if (transform.position != currentPosition) {
            MoveToPosition(currentPosition); 
        }
        
        Vector3 newPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.W)) {
        	audioManager.PlayRandomMoveSound(); // IF MOVEMENT SUCCESS
            newPosition = transform.position + transform.forward * movementLengthIncrement ; 
        }
        
        if (newPosition != transform.position)
        {
            if (movementManager.currentStepsLeft <= 0) {
                movementManager.NextDay();
                return;
            }

            // Previous Version : 
            //rigidBody.MovePosition(newPosition);

            if (GetMapPieceAtPosition(newPosition) != null) {
                if (transform.position != currentPosition) {
                    transform.position = currentPosition;
                    return;
                }
            
                GameObject mapPiece = GetMapPieceAtPosition(newPosition);
                if (mapPiece) { // check all mapPiece children for Door
                    foreach (Transform childTransform in mapPiece.transform)
                    {
                        if (childTransform.CompareTag("Door"))
                        {
                            childTransform.GetComponent<Door>().TryOpen();
                            return; // Add animations for opening or failed opening
                        }
                    }
                    transform.position = currentPosition;
                    currentPosition = GetRoundedPosition(newPosition);
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
    }
    
    void RotateTowards(Quaternion rotation)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, rotation) <= angleToConsiderRotated) {
            transform.rotation = rotation;
        }
    }
    void HandleRotation()
    {
        if (transform.rotation != currentRotation) {
            RotateTowards(currentRotation);
        }
        
        
        float rotationDegrees = 0;
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotationDegrees = -90;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rotationDegrees = 90;
        }

        if (rotationDegrees != .0f) {
            Quaternion prevRotation = currentRotation;
            Quaternion delta = Quaternion.AngleAxis(rotationDegrees, Vector3.up);

            Quaternion resultRotation = prevRotation * delta; // <-- This is what transform.rotation would become

            currentRotation = resultRotation;
            //transform.Rotate(Vector3.up, rotationDegrees * -1);
            CheckForItem();
        }
    }


    void CheckForItem()
    {
        Vector3 checkPosition = transform.position + transform.forward * movementLengthIncrement;
        GameObject checkObject = GetMapPieceAtPosition(checkPosition);
        if (checkObject) {
            if (checkObject.transform.GetComponentInChildren<Item>())
            {
                Debug.Log("Item Found");
                // but do we need it? We can just check collision...
                // can be usefull for "Hanging on wall" items, that need "interact" button
            }
        }
    }


    GameObject GetMapPieceAtPosition(Vector3 position)  // THIS RETURNS MARKER!!!!
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


    float Vec3DistanceNoHeight(Vector3 p1, Vector3 p2)
    {
        // EXPENSIVE
        //return Vector2.Distance(new Vector2(p1.x,p1.z), new Vector2(p2.x,p2.z));
        
        // CHEAPER I GUESS
        float dx = p1.x - p2.x;
        float dz = p1.z - p2.z;
        return Mathf.Sqrt(dx * dx + dz * dz);
    }


    Vector3 GetRoundedPosition(Vector3 position)
    {
        return new Vector3(MathF.Round(position.x), MathF.Round(position.y), MathF.Round(position.z));
    }
}
