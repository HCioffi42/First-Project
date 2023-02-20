using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private int percentage; //chance de pegar peixe
    [SerializeField] private GameObject fishPrefab;

    private Player_Itens player;
    private PlayerAnim playerAnim;

    private bool detectingPlayer;

    void Start()
    {
        player = FindObjectOfType<Player_Itens>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    void Update()
    {
        // CRIAR UM BOTÃO DE AÇÃO ESPECIFICO PARA ANIMAÇÃO E AÇÃO DE PESCA JUNTO COM A DE COELTA DE ÁGUA (PRESS E: COLETA ÁGUA/PRESS LEFTCLICK: PESCA)
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            playerAnim.OnCastingStart();
        }
    }

    public void OnCasting()
    {
        //CRIAR STAMINA PARA NÚMERO DE VEZES PARA PESCAR
        int randomValue = Random.Range(1, 100);

        if(randomValue <= percentage)
        {
            //pegou peixe
            Instantiate(fishPrefab, player.transform.position + new Vector3(Random.Range(-2f, -1f), 0f, 0f), Quaternion.identity);
            Debug.Log("Pescou!");
        }
        else
        {
            //não pegou
            Debug.Log("Não pescou!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
