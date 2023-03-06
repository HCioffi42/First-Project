// Importing required libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defining PlayerAnim class
public class PlayerAnim : MonoBehaviour
{
    // Defining attack settings
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    // Initializing variables
    private Player player;
    private Animator anim;
    private Fishing cast;
    private bool isHitting;
    private float recoveryTime = 1f;
    private float timeCount;

    // Start function
    void Start()
    {
        // Getting references to the player and animator components
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();

        // Finding the Fishing component in the scene
        cast = FindObjectOfType<Fishing>();
    }

    // Update function
    void Update()
    {
        // Handling movement animations
        OnMove();
        OnRun();

        // Handling hit recovery time
        if(isHitting)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
    }

    #region Movement

    // Handling movement animations
    void OnMove()
    {
        if(player.direction.sqrMagnitude > 0)
        {
            if(player.isRolling)
            {
                if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
                {
                    anim.SetTrigger("isRoll");
                }
            }else
            {
                anim.SetInteger("Transition", 1);
            }
            
        }
        else
        {
            anim.SetInteger("Transition", 0);
        }

        if(player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.direction.x < 0 )
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (player.isCutting)
        {
            anim.SetInteger("Transition", 3);
        }

        if (player.isDigging)
        {
            anim.SetInteger("Transition", 4);
        }
        if (player.isWatering)
        {
            anim.SetInteger("Transition", 5);
        }        
        if (player.isAttaking)
        {
            anim.SetInteger("Transition", 6);
        }
    }

    // Handling running animations
    void OnRun()
    {
        if(player.direction.sqrMagnitude > 0)
        {
            if(player.isRunning)
            {
                anim.SetInteger("Transition", 2);
            }
        }
    }
    #endregion

    #region Attack

    // Handling attack animations
    public void OnAttack()
    {
        // Checking for enemy collision
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if(hit != null)
        {
            //enemy hit
            hit.GetComponentInChildren<AnimationControl>().OnHit();
            Debug.Log("enemy hit");
        }
    }

    // Drawing attack radius gizmo in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    #endregion

    // Handling fishing animations
    public void OnCastingStart()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }

    public void OnCastingEnd()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

	// This method is called when the player starts hammering.
	public void OnHammeringStarted()
	{
		anim.SetBool("Hammering", true);
	}
	
	// This method is called when the player ends hammering.
	public void OnHammeringEnded()
	{
		anim.SetBool("Hammering", false);
	}
	
	// This method is called when the player hits something.
	public void OnHit()
	{
		if(!isHitting)
		{
			anim.SetTrigger("Hit");
			isHitting = true;
		}
	}
}
