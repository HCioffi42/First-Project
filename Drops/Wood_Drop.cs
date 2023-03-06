using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Drop : MonoBehaviour
{
    [SerializeField] private float speed; // the speed at which the wood drop moves
    [SerializeField] private float timeMove; // the amount of time the wood drop moves

    private float timeCount; // the amount of time that has elapsed since the wood drop was created
    private int woodValue; // the value of the wood drop
    private Player_Itens player; // a reference to the player's inventory

    void Start()
    {
        player = FindObjectOfType<Player_Itens>(); // find the player's inventory in the scene
    }

    void Update()
    {
        timeCount += Time.deltaTime; // increment the elapsed time
        if (timeCount < timeMove) // if the wood drop hasn't moved for the full duration
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed); // move the wood drop to the right
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.totalWood < player.woodLimit) // if the wood drop collides with the player and the player's inventory has space for more wood
        {
            player.WoodLimit(woodValue); // add the wood drop's value to the player's inventory
            player.totalWood++; // increment the player's wood count
            Destroy(gameObject); // destroy the wood drop
        }
    }
}
