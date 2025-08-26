using UnityEngine;
using System.Collections.Generic;

public class MovementManager : MonoBehaviour
{
    [HideInInspector] public int currentDay = 0;

    [HideInInspector] public int currentStepsLeft = 0;

    public Dictionary<int, int> dayStepsDictionary = new Dictionary<int, int>() {
        // Day , Amount of Steps
        {1 , 10},
        {2 , 12},
        {3 , 8},
        {4 , 14},
    };
    
    public UI UI;

    void Start()
    {
        currentDay = 0;
        NextDay();
    }
    
    public void NextDay()
    {
        // Can just use the same animation from the other project
        currentDay++;
        currentStepsLeft = dayStepsDictionary[currentDay];
        UI.UpdateCounters();
    }
    
    
}
