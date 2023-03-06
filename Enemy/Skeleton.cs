    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;
    using UnityEngine.UI;

    public class Skeleton : MonoBehaviour
    {
        public EnemyObject enemySettings;  // Reference to the enemy object settings

        [Header ("Stats")]
        public float radius;  // Detection radius of the skeleton
        public LayerMask layer;  // Layermask used for detection of the player
        public float currentHealth;  // Current health of the skeleton
        public float totalHealth;  // Total health of the skeleton
        public Image healthBar;  // Reference to the UI health bar
        public bool isDead;  // Flag indicating if the skeleton is dead or not


        [Header("Components")]
        [SerializeField] private NavMeshAgent agent;  // Reference to the NavMeshAgent component
        [SerializeField] private AnimationControl animControl;  // Reference to the AnimationControl component

        private Player player;  // Reference to the player object
        private bool detectPlayer;  // Flag indicating if the skeleton detected the player or not
        private int index;  // Current index of the waypoint the skeleton is moving towards
        public List<Transform> paths = new List<Transform>();  // List of waypoints the skeleton can move towards
        //private Vector3 initialPosition;  // Store the initial position of the skeleton


        void Start()
        {
            currentHealth = totalHealth;  // Set the current health of the skeleton to its total health
            player = FindObjectOfType<Player>();  // Find the player object in the scene
            agent.updateRotation = false;  // Disable automatic rotation updates by NavMeshAgent
            agent.updateUpAxis = false;  // Disable automatic up-axis updates by NavMeshAgent
            //initialPosition = transform.position; // Store the initial position of the skeleton
        }

        void Update()
        {
            if(!isDead && detectPlayer)  // If the skeleton is not dead and detects the player
            {
                agent.isStopped = false;  // Enable NavMeshAgent movement
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

        private void FixedUpdate()
        {
            DetectPlayer();  // Detect if the player is within detection radius
        }

        public void DetectPlayer()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position , radius, layer);  // Check if player is within detection radius
            if(hit != null)
            {
                detectPlayer = true;  // Set flag to true if player is detected
            }
            else
            {
                detectPlayer = false;  // Set flag to false if player is not detected
                agent.isStopped = false;
                animControl.PlayAnim(1); //walking animation
                //agent.SetDestination(initialPosition);  // Set the destination of the NavMeshAgent to the initial position
                transform.position = Vector2.MoveTowards(transform.position, paths[index].position, agent.speed * Time.deltaTime);  // Move the skeleton towards the current waypoint

                if(Vector2.Distance(transform.position, paths[index].position) < 0.1f)
                {
                    if(index < paths.Count - 1)
                    {
                        index++;
                        //index = Random.Range(0, paths.Count - 1);  
                    }
                    else
                    {
                        index = 0;
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }