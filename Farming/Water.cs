using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private bool detectingPlayer; // a flag indicating whether the player is close enough to interact with the water
    [SerializeField] private int waterValue; // the amount of water that will be given to the player

    private Player_Itens player; // a reference to the player

    void Start()
    {
        player = FindObjectOfType<Player_Itens>(); // finds the Player_Itens component in the scene and stores it in player
    }

    void Update()
    {
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E)) // checks if the player is close enough and presses the E key
        {
            player.WaterLimit(waterValue); // calls the WaterLimit method of the player and passes the waterValue as a parameter
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // called when a collider enters the trigger zone
    {
        if(collision.CompareTag("Player")) // checks if the collider's tag is "Player"
        {
            detectingPlayer = true; // sets detectingPlayer to true to indicate that the player is close enough to interact with the water
        }
    }

    private void OnTriggerExit2D (Collider2D collision) // called when a collider exits the trigger zone
    {
        if(collision.CompareTag("Player")) // checks if the collider's tag is "Player"
        {
            detectingPlayer = false; // sets detectingPlayer to false to indicate that the player is no longer close enough to interact with the water
        }
    }
}
