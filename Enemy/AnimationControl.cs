using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    // Reference to the transform used as the attack point.
    [SerializeField] private Transform attackPoint;
    
    // The radius of the circle used for attack detection.
    [SerializeField] private float radius;
    
    // The layer mask used to identify the player.
    [SerializeField] private LayerMask playerLayer;

    // Reference to the animator component.
    private Animator anim;
    
    // Reference to the PlayerAnim script.
    private PlayerAnim player;
    
    // Reference to the Skeleton script.
    private Skeleton skeleton;

    // Initialize references.
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>();
    }

    // Plays an animation based on an integer value.
    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    // Detects and damages the player if they are within range of the attack point.
    public void Attack()
    {
        // Only attack if the skeleton is not dead.
        if (!skeleton.isDead)
        {
            // Detect if the player is within range of the attack point.
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            // If the player is detected, damage them.
            if (hit != null)
            {
                player.OnHit();
            }
        }
    }

    // Called when the skeleton is hit.
    public void OnHit()
    {
        // If the skeleton's health is depleted, play the death animation and destroy the game object.
        if (skeleton.currentHealth <= 0)
        {
            anim.SetTrigger("death");
            skeleton.isDead = true;
            Destroy(skeleton.gameObject, 1f);
        }
        // If the skeleton is still alive, play the hit animation and decrement its health.
        else
        {
            anim.SetTrigger("hit");
            skeleton.currentHealth--;
            skeleton.healthBar.fillAmount = skeleton.currentHealth / skeleton.totalHealth;
        }
    }

    // Draws a wire sphere around the attack point in the Unity editor.
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
