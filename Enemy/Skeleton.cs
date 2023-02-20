using System.Collections;  // Import the System.Collections namespace to use non-generic collections
using System.Collections.Generic;  // Import the System.Collections.Generic namespace to use generic collections
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header ("Stats")]
    public float currentHealth;
    public float totalHealth;
    public Image healthBar;
    public bool isDead;


    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;  // Reference to the NavMeshAgent component
    [SerializeField] private AnimationControl animControl;

    private Player player;  // Reference to the player object

    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player>();  // Find the player object in the scene
        agent.updateRotation = false;  // Disable automatic rotation updates by NavMeshAgent
        agent.updateUpAxis = false;  // Disable automatic up-axis updates by NavMeshAgent
    }

    void Update()
    {
        if(!isDead)
        {
            agent.SetDestination(player.transform.position);  // Set the destination of the NavMeshAgent to the player's position

            if(Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                animControl.PlayAnim(2); //Stop and start the attack animation
            }
            else
            {
                animControl.PlayAnim(1); //Go for the player
            }

            float posX = player.transform.position.x - transform.position.x;
            if (posX > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
    }
}
