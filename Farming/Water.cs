using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private bool detectingPlayer; //boolean to detect if Player is in range of the selected area
    [SerializeField] private int waterValue; //amount of water Player gets when interacting with the water source

    private Player_Itens player;

    void Start()
    {
        player = FindObjectOfType<Player_Itens>();
    }

    void Update()
    {
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            player.WaterLimit(waterValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
