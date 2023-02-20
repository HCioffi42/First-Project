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
    public float woodLimit;
    public float carrotsLimit;
    public float waterLimit;
    public float fishLimit;

    public void WaterLimit(float water)
    {
        if(currentWater <= waterLimit)
        {
            currentWater += water;
        }
    }
    
}
