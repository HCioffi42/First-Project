using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controler : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private Image woodUIBAR;
    [SerializeField] private Image carrotUIBAR;
    [SerializeField] private Image waterUIBAR;
    [SerializeField] private Image fishUIBAR;

    [Header("Tools")]
    // [SerializeField] private Image axeUI;
    // [SerializeField] private Image shovelUI;
    // [SerializeField] private Image bucketUI;
    // [SerializeField] private Image rodUI;
    public List<Image> toolsUI = new List<Image>();
    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;

    private Player_Itens playerItens;
    private Player player;

    private void Awake()
    {
        playerItens = FindObjectOfType<Player_Itens>();
        player = playerItens.GetComponent<Player>();
    }

    void Start()
    {
        woodUIBAR.fillAmount = 0f;
        carrotUIBAR.fillAmount = 0f;
        waterUIBAR.fillAmount = 0f;
        fishUIBAR.fillAmount = 0f;
    }

    
    void Update()
    {
        woodUIBAR.fillAmount = playerItens.totalWood / playerItens.woodLimit;
        carrotUIBAR.fillAmount = playerItens.totalCarrots / playerItens.carrotsLimit;
        waterUIBAR.fillAmount = playerItens.currentWater / playerItens.waterLimit;
        fishUIBAR.fillAmount = playerItens.totalFish / playerItens.fishLimit;

       // toolsUI[player.handlingObj].color = selectColor;

        for (int i = 0; i < toolsUI.Count; i++)
        {
            if (i == player.handlingObj)
            {
                toolsUI[i].color = selectColor;
            }
            else
            {
                toolsUI[i].color = alphaColor;
            }
        }
    }
}
