using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Drop : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeMove;

    private float timeCount;


    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount < timeMove)
        {
            transform.Translate(Vector2.right * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Itens>().totalWood++;
            Destroy(gameObject);  
        }
    }
}

