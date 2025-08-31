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
        Quaternion newRotation = new Quaternion();
        if (Input.GetKeyDown(KeyCode.A)) {
            newRotation = rigidBody.rotation * Quaternion.Euler(Vector3.up * -90);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            newRotation = rigidBody.rotation * Quaternion.Euler(Vector3.up * 90);
        }

        if (newRotation != new Quaternion()) {
            rigidBody.MoveRotation(newRotation);
            CheckForItem();
        }
    }


    public void CheckForItem()
    {
        Vector3 checkPosition = transform.position + transform.forward * movementLengthIncrement;
        if (GetMapPieceAtPosition(checkPosition) != null) {
            GameObject checkObject = GetMapPieceAtPosition(checkPosition);
            // Check for all the children of the object,
            // If one of the children of the object has a Component of "Item" there is an item
            // If there is an item -> do item logic
        }
    }
    
    
    public GameObject GetMapPieceAtPosition(Vector3 position)
    {
        for (int i = 0; i < mapPieces.transform.childCount; i++) {
            if ((int)mapPieces.transform.GetChild(i).transform.position.x == (int)position.x &&
                (int)mapPieces.transform.GetChild(i).transform.position.z == (int)position.z) {
                Debug.Log(mapPieces.transform.GetChild(i).name + ":" + mapPieces.transform.GetChild(i).transform.position + ", for : " + position);
                return  mapPieces.transform.GetChild(i).transform.GetChild(0).gameObject;
            }
        }

        Debug.Log("NO MAP PIECE FOUND AT : " + position.ToString());
        return null;
    }
    
}
