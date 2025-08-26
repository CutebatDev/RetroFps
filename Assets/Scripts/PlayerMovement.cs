using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody rigidBody;
    
    [HideInInspector] public MovementManager movementManager;

    public UI UI;

    void Start()
    {
        movementManager = transform.GetComponent<MovementManager>();
        rigidBody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }
    
    
    void HandleMovement()
    {
        Vector3 newPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.W)) {
            newPosition = transform.position + transform.forward;
        }
        
        if (newPosition != transform.position)
        {
            if (movementManager.currentStepsLeft <= 0)
                return;

            rigidBody.MovePosition(newPosition);
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
}
