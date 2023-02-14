using System.Collections;  // Import the System.Collections namespace to use non-generic collections
using System.Collections.Generic;  // Import the System.Collections.Generic namespace to use generic collections
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;  // Reference to the NavMeshAgent component

    private Player player;  // Reference to the player object

    void Start()
    {
        player = FindObjectOfType<Player>();  // Find the player object in the scene
        agent.updateRotation = false;  // Disable automatic rotation updates by NavMeshAgent
        agent.updateUpAxis = false;  // Disable automatic up-axis updates by NavMeshAgent
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);  // Set the destination of the NavMeshAgent to the player's position
    }
}
