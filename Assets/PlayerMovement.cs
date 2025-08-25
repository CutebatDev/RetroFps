using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody rigidBody;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigidBody.MovePosition(transform.position + transform.forward);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rigidBody.MovePosition(transform.position - transform.forward);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(Vector3.up * -90));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(Vector3.up * 90));
        }
    }
}
