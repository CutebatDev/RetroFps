using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    public MovementManager movementManager;
    
    public TextMeshProUGUI text;
    
    
    
    public void UpdateCounters()
    {
        text.text = "Day : " + movementManager.currentDay.ToString() + "\nStep : " + movementManager.currentStepsLeft.ToString();
    }
}
