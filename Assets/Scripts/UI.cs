using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public MovementManager movementManager;
    
    public TextMeshProUGUI text;
    
    public Image[] inventorySlots;
    
    private Animator animator;
    public TextMeshProUGUI dayTextLabel;
    public Image dayCounterAnimationImage;
    
    private void Start()
    {
        dayCounterAnimationImage.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        NextDay();
        UpdateInventory();
    }

    private void Start()
    {
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        PlayerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory._inventory.Count)
            {
                inventorySlots[i].sprite = Sprite.Create(inventory._inventory[i].image,
                    new Rect(0, 0, inventory._inventory[i].image.width, inventory._inventory[i].image.height),
                    new Vector2(0.5f, 0.5f));
                ;
            }
            else
                inventorySlots[i].sprite = null;
        }
    }
    public void UpdateCounters()
    {
        text.text = "Day : " + movementManager.currentDay.ToString() + "\nStep : " + movementManager.currentStepsLeft.ToString();
    }
    
    
    public void NextDay()
    {
        dayTextLabel.text = "Day " + movementManager.currentDay.ToString();
        animator.SetTrigger("Next Day");
    }
}
