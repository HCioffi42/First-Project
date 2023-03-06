using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private int fishValue; // the value of the fish
    private Player_Itens player; // reference to the player's item inventory

    void Start()
    {
        player = FindObjectOfType<Player_Itens>(); // get a reference to the player's item inventory
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the fish collides with the player and the player's fish limit is not reached
        if(collision.CompareTag("Player") && player.totalFish < player.fishLimit)
        {
            player.FishLimit(fishValue); // increase the fish count
            player.totalFish++; // increment the total fish count
            Destroy(gameObject); // destroy the fish object
        }
    }
}
