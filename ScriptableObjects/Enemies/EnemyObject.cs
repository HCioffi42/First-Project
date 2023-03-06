using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemyAttributes", menuName = "Enemy/New Enemy")]
public class EnemyObject : ScriptableObject
{
    public string name;
    public float health;
    public float speed;
    public float damage;

}
