using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Itens : MonoBehaviour
{
    [Header("Amounts")]
    public int totalWood;
    public int totalCarrots;
    public float currentWater;
    public int totalFish;
    
    [Header("Limits")]
    public float woodLimit = 10;
    public float carrotsLimit = 5;
    public float waterLimit = 10;
    public float fishLimit = 5;

    public void WaterLimit(float water)
    {
        if(currentWater <= waterLimit)
        {
            currentWater += water;
        }
        
    }
    
}
