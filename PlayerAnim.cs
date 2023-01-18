using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator anim;
    private Fishing cast;
    

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
}
