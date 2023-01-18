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
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Itens>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        // Criar um botão de ação especifico para animação e ação de pesca junto com a de coelta de água (Press E: coleta água/Press LeftClick: pesca)
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            playerAnim.OnCastingStart();
        }
    }

    public void OnCasting()
    {
        //criar Stamina para número de vezes para pescar
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
