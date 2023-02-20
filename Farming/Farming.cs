using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    // These serializable fields are used to reference components and set settings in the Unity Editor
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount; //Life of the hole
    [SerializeField] private float waterAmount; //Total water for carrot

    [SerializeField] private bool detecting; // Used to determine whether the player is detecting water

    // Private fields for tracking variables related to the hole and carrot
    private int initialDigAmount;
    private float currentWater;
    private bool dugHole;

    // Reference to player items object
    Player_Itens playerItens;

    private void Start()
    {
        playerItens = FindObjectOfType<Player_Itens>();
        initialDigAmount = digAmount;
    }

    private void Update()
    {
        // If the hole has been dug
        if(dugHole)
        {
            // If the player is detecting water, increment current water
            if(detecting)
            {
                currentWater += 0.01f;
            }

            // If the current water is greater than or equal to the required water, display carrot and allow player to harvest
            if(currentWater >= waterAmount)
            {
                spriteRenderer.sprite = carrot;

                if(Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = hole;
                    playerItens.totalCarrots++;
                    currentWater = 0f;
                }
            }
        }
    }

    // This method is called when the hole is hit with a "dig" object
    public void OnHit()
    {
        digAmount--;

        // If the dig amount is below half the initial dig amount, change the sprite to the hole
        if(digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            dugHole = true;
        }

    }

    // This method is called when a trigger collider is entered
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the trigger is a "dig" object, call OnHit
        if(collision.CompareTag("Dig"))
        {
            OnHit();
        }

        // If the trigger is a "water" object, set detecting to true
        if(collision.CompareTag("Water"))
        {
            detecting = true;
        }
    }

    // This method is called when a trigger collider is exited
    private void OnTriggerExit2D(Collider2D collision)
    {
         // If the trigger is a "water" object, set detecting to false
        if(collision.CompareTag("Water"))
        {
            detecting = false;
        }
    }
}

