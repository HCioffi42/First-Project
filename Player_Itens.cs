using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Itens : MonoBehaviour
{
    // These variables store the amounts of different items collected by the player.
    [Header("Amounts")]
    public float totalWood;
    public float totalCarrots;
    public float currentWater;
    public float totalFish;
    
    // These variables define the limits for each type of item that the player can carry.
    [Header("Limits")]
    public float woodLimit;
    public float carrotsLimit;
    public float waterLimit;
    public float fishLimit;

    // This method is used to add water to the player's inventory, up to the water limit.
    public void WaterLimit(float water)
    {
        // If the current water amount is less than the water limit, add the given amount of water to the inventory.
        if(currentWater < waterLimit)
        {
            currentWater += water;
        }
    }

    // This method is used to add wood to the player's inventory, up to the wood limit.
    public void WoodLimit(float wood)
    {
        // If the total wood amount is less than the wood limit, add the given amount of wood to the inventory.
        if(totalWood < woodLimit)
        {
            totalWood += wood;
        }
    }

    // This method is used to add fish to the player's inventory, up to the fish limit.
    public void FishLimit(float fish)
    {
        // If the total fish amount is less than the fish limit, add the given amount of fish to the inventory.
        if(totalFish < fishLimit)
        {
            totalFish += fish;
        }
    }

    // This method is used to add carrots to the player's inventory, up to the carrots limit.
    public void CarrotsLimit(float carrots)
    {
        // If the total carrot amount is less than the carrot limit, add the given amount of carrots to the inventory.
        if(totalCarrots < carrotsLimit)
        {
            totalCarrots += carrots;
        }
    }
}
