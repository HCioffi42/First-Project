using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    // Define public variables that can be edited in the Unity editor
    [SerializeField] private int percentage; // The percentage chance of successfully catching a fish
    [SerializeField] private GameObject fishPrefab; // The fish object to be spawned when a fish is caught

    // Define private variables that can only be accessed within this script
    private Player_Itens player; // Reference to the Player_Itens script
    private PlayerAnim playerAnim; // Reference to the PlayerAnim script

    private bool detectingPlayer; // Whether the player is detected within the fishing area

    void Start()
    {
        // Find and store a reference to the Player_Itens script
        player = FindObjectOfType<Player_Itens>();
        // Find and store a reference to the PlayerAnim script attached to the player GameObject
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    void Update()
    {
        // If the player is detected and presses the E key
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            // Trigger the OnCastingStart animation on the player
            playerAnim.OnCastingStart();
        }
    }

    // Called when the player casts their fishing line
    public void OnCasting() //CRIAR STAMINA PARA NÚMERO DE VEZES PARA PESCAR

    {
        // Generate a random number between 1 and 100
        int randomValue = Random.Range(1, 100);

        // If the random value is less than or equal to the percentage value
        if(randomValue <= percentage)
        {
            // Spawn a fishPrefab GameObject at a random position near the player
            Instantiate(fishPrefab, player.transform.position + new Vector3(Random.Range(-2f, -1f), 0f, 0f), Quaternion.identity);
        }
        else
        {
            // If the random value is greater than the percentage value, the fishing attempt is unsuccessful
            Debug.Log("Não pescou!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the GameObject that entered the trigger area has a "Player" tag
        if(collision.CompareTag("Player"))
        {
            // Set detectingPlayer to true
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        // If the GameObject that exited the trigger area has a "Player" tag
        if(collision.CompareTag("Player"))
        {
            // Set detectingPlayer to false
            detectingPlayer = false;
        }
    }
}
