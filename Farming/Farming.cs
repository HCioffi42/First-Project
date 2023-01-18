using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount; //"vida" do buraco
    [SerializeField] private float waterAmount; //Total de água para Cenoura

    [SerializeField] private bool detecting;

    private int initialDigAmount;
    private float currentWater;

    private bool dugHole;

    Player_Itens playerItens;

    private void Start()
    {
        playerItens = FindObjectOfType<Player_Itens>();
        initialDigAmount = digAmount;
    }

    private void Update()
    {
        if(dugHole)
        {
            if(detecting)
            {
                currentWater += 0.01f;
            }

            //encheu de água suficiente
            if(currentWater >= waterAmount)
            {
                spriteRenderer.sprite = carrot;

                if(Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = hole;
                    playerItens.totalCarrots++;
                    currentWater = 0f;
                }
            }
        }
    }

    public void OnHit()
    {
        digAmount--;

        if(digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            dugHole = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Dig"))
        {
            OnHit();
        }

        if(collision.CompareTag("Water"))
        {
            detecting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         if(collision.CompareTag("Water"))
        {
            detecting = false;
        }
    }
}
