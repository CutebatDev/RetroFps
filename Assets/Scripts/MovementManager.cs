using UnityEngine;
using System.Collections.Generic;
using DefaultNamespace;

public class MovementManager : MonoBehaviour
{
    [HideInInspector] public int currentDay = 0;

    [HideInInspector] public int currentStepsLeft = 0;

    public Dictionary<int, int> dayStepsDictionary = new Dictionary<int, int>() {
        // Day , Amount of Steps
        {1 , 45},
        {2 , 32},
        {3 , 29},
        {4 , 31},
        {5 , 34},
        {6 , 41},
        {7, 45}
    };
    
    public UI UI;
    
    private Vector3 initialPlayerPosition = Vector3.zero;
    private Quaternion initialPlayerRotation = Quaternion.identity;

    private PlayerMovement playerMovement;

    public SceneHandler sceneHandler;
    
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
        if (!dayStepsDictionary.ContainsKey(currentDay)) {
            sceneHandler.ChangeScene(CutsceneType.SlumberEnding);
            return;
        }
        playerMovement.currentPosition = initialPlayerPosition;
        transform.position = initialPlayerPosition;
        playerMovement.movementManager.playerMovement.movementManager.playerMovement.currentRotation = initialPlayerRotation;
        transform.rotation = initialPlayerRotation;
        currentStepsLeft = dayStepsDictionary[currentDay];
        UI.UpdateCounters();
        UI.NextDay();
    }
    
    
}
