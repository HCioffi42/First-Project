using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip CarrotSFX;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount;
    [SerializeField] private float waterAmount;

    [SerializeField] private bool detecting;
    private bool isPlayer;

    private int initialDigAmount;
    private float currentWater;
    private bool dugHole;
    private bool soundCarrot;
    private int carrotValue;
    private Player_Itens player;

    private void Start()
    {
        player = FindObjectOfType<Player_Itens>();
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

            if(currentWater >= waterAmount && !soundCarrot)
            {
                audioSource.PlayOneShot(holeSFX);
                spriteRenderer.sprite = carrot;
                soundCarrot = true;
            }
            if(Input.GetKeyDown(KeyCode.E) && soundCarrot && isPlayer && player.totalCarrots < player.carrotsLimit)
            {
                player.CarrotsLimit(carrotValue);
                audioSource.PlayOneShot(CarrotSFX);
                spriteRenderer.sprite = hole;
                player.totalCarrots++;
                currentWater = 0f;
                soundCarrot = false;
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
        if(collision.CompareTag("Player"))
        {
            isPlayer = true;
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
        if(collision.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}

