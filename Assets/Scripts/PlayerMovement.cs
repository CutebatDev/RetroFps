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
        transform.position = new Vector3(0,0.5f,0);
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

            transform.position = currentPosition;

            Debug.Log(GetMapPieceAtPosition(newPosition));
            currentPosition = newPosition;
            
            movementManager.currentStepsLeft--;
            UI.UpdateCounters();
        }

    }

    void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(Vector3.up * -90));
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(Vector3.up * 90));
        }
    }


    public GameObject GetMapPieceAtPosition(Vector3 position)
    {
        for (int i = 0; i < mapPieces.transform.childCount; i++) {
            Debug.Log(mapPieces.transform.GetChild(i).name + ":" + mapPieces.transform.GetChild(i).transform.position + ", for : " + position);
            if ((int)mapPieces.transform.GetChild(i).transform.position.x == (int)position.x && (int)mapPieces.transform.GetChild(i).transform.position.z == (int)position.z) {
                return  mapPieces.transform.GetChild(i).transform.GetChild(0).gameObject;
            }
        }

        return null;
    }
    
}
