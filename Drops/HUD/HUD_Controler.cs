using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controler : MonoBehaviour
{
    [Header("Items")]
    // [SerializeField] private Image axeUI;
    // [SerializeField] private Image shovelUI;
    // [SerializeField] private Image bucketUI;
    // [SerializeField] private Image rodUI;
    [SerializeField] private Image woodUIBAR;
    [SerializeField] private Image carrotUIBAR;
    [SerializeField] private Image waterUIBAR;
    [SerializeField] private Image fishUIBAR;

    [Header("Tools")]
    public List<Image> toolsUI = new List<Image>();
    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;

    private Player_Itens playerItens; // reference to Player_Itens script
    private Player player; // reference to Player script

    private void Awake()
    {
        playerItens = FindObjectOfType<Player_Itens>(); // finds an object with Player_Itens component in the scene
        player = playerItens.GetComponent<Player>(); // gets a reference to the Player script attached to the Player_Itens object
    }

    void Start()
    {
        woodUIBAR.fillAmount = 0f; // sets the fill amount of the wood UI bar to 0
        carrotUIBAR.fillAmount = 0f; // sets the fill amount of the carrot UI bar to 0
        waterUIBAR.fillAmount = 0f; // sets the fill amount of the water UI bar to 0
        fishUIBAR.fillAmount = 0f; // sets the fill amount of the fish UI bar to 0
    }

    void Update()
    {
        woodUIBAR.fillAmount = playerItens.totalWood / playerItens.woodLimit; // sets the fill amount of the wood UI bar to the current amount of wood divided by the maximum amount of wood
        carrotUIBAR.fillAmount = playerItens.totalCarrots / playerItens.carrotsLimit; // sets the fill amount of the carrot UI bar to the current amount of carrots divided by the maximum amount of carrots
        waterUIBAR.fillAmount = playerItens.currentWater / playerItens.waterLimit; // sets the fill amount of the water UI bar to the current amount of water divided by the maximum amount of water
        fishUIBAR.fillAmount = playerItens.totalFish / playerItens.fishLimit; // sets the fill amount of the fish UI bar to the current amount of fish divided by the maximum amount of fish

        // toolsUI[player.handlingObj].color = selectColor;

        for (int i = 0; i < toolsUI.Count; i++) // loops through the tools UI list
        {
            if (i == player.handlingObj) // checks if the current tool UI matches the player's handling object
            {
                toolsUI[i].color = selectColor; // sets the color of the current tool UI to the select color
            }
            else
            {
                toolsUI[i].color = alphaColor; // sets the color of the current tool UI to the alpha color
            }
        }
    }
}
