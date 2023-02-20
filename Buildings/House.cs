using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    // Amount of wood required to build the house
    [Header("Amounts")]
    [SerializeField] private int woodAmount;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    // Time it takes to build the house
    [SerializeField] private float timeAmount;

    // Components of the house game object
    [Header ("Components")]
    [SerializeField] private GameObject houseCollider;
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point;

    // Variables to track player and construction progress
    private bool detectingPlayer;
    private Player player;
    private PlayerAnim playerAnim;
    private Player_Itens playerItens;
    private float timeCount;
    private bool isBegining;

    void Start()
    {
        // Find the player game object and get its components
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
        playerItens = player.GetComponent<Player_Itens>();
    }

    void Update()
    {
        // Check if player is detected and has enough wood to build the house
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E) && playerItens.totalWood >= woodAmount)
        {
            // Start construction of the house
            isBegining = true;
            playerAnim.OnHammeringStarted();
            houseSprite.color = startColor;
            player.transform.position = point.position;
            player.isPaused = true;
            playerItens.totalWood -= woodAmount;
        }

        // If construction has begun, update time count and change house sprite color until construction is complete
        if(isBegining)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= timeAmount)
            {
                // Construction is complete
                playerAnim.OnHammeringEnded();
                houseSprite.color = endColor;
                player.isPaused = false;
                houseCollider.SetActive(true);
            }
        }
    }

    // Detect when player enters the house collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    // Detect when player exits the house collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
