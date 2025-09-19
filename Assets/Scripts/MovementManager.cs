using UnityEngine;
using System.Collections.Generic;

public class MovementManager : MonoBehaviour
{
    [HideInInspector] public int currentDay = 0;

    [HideInInspector] public int currentStepsLeft = 0;

    public Dictionary<int, int> dayStepsDictionary = new Dictionary<int, int>() {
        // Day , Amount of Steps
        {1 , 22},
        {2 , 15},
        {3 , 8},
        {4 , 14},
    };
    
    public UI UI;
    
    private Vector3 initialPlayerPosition = Vector3.zero;
    private Quaternion initialPlayerRotation = Quaternion.identity;

    private PlayerMovement playerMovement;
    
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        initialPlayerPosition = transform.position;
        initialPlayerRotation = transform.rotation;
        currentDay = 0;
        NextDay();
    }
    
    public void NextDay()
    {
        // Can just use the same animation from the other project
                // Just used the same animation from the other project
        
        currentDay++;
        playerMovement.currentPosition = initialPlayerPosition;
        transform.position = initialPlayerPosition;
        playerMovement.movementManager.playerMovement.movementManager.playerMovement.currentRotation = initialPlayerRotation;
        transform.rotation = initialPlayerRotation;
        currentStepsLeft = dayStepsDictionary[currentDay];
        UI.UpdateCounters();
        UI.NextDay();
    }
    
    
}
