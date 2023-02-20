using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settins")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;
    private Animator anim;
    private Fishing cast;
    
    private bool isHitting;
    private float recoveryTime = 1f;
    private float timeCount;

    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();

        cast = FindObjectOfType<Fishing>();
    }

    
    void Update()
    {
        OnMove();
        OnRun();

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

    void OnMove()
    {
        if(player.direction.sqrMagnitude > 0)
        {
            if(player.isRolling)
            {
                anim.SetTrigger("isRoll");
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
    }

    void OnRun()
    {
        if(player.isRunning)
        {
            anim.SetInteger("Transition", 2);
        }

    }
    #endregion

    #region Attack

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if(hit != null)
        {
            //attack hit
            hit.GetComponentInChildren<AnimationControl>().OnHit();
            Debug.Log("enemy hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    #endregion


    //é chamado quando press E no lago
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

    public void OnHammeringStarted()
    {
        anim.SetBool("Hammering", true);
    }

    public void OnHammeringEnded()
    {
        anim.SetBool("Hammering", false);
    }

    public void OnHit()
    {
        if(!isHitting)
        {
            anim.SetTrigger("Hit");
            isHitting = true;
        }
    }
}
