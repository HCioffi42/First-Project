using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeAmount;


    private bool detectingPlayer;
    private Player_Itens player;
    private PlayerAnim playerAnim;
    private float timeCount;
    private bool isBegining;

    void start()
    {
        player = FindObjectOfType<Player_Itens>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }


    void Update()
    {
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            isBegining = true;
            playerAnim.OnHammeringStarted();
            houseSprite.color = startColor;
            player.transform.position = point.position;
        }

        if(isBegining)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= timeAmount)
            {
                //casa finalizada
            playerAnim.OnHammeringEnded();
            houseSprite.color = endColor;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}

