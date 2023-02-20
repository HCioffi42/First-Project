using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language;


    [Header("Components")]
    public GameObject dialogueObj;  //janela do dialogo
    public Image profileSprite; // sprite do perfil
    public Text speechText; //texto da fala
    public Text actorNameText; //nome do NPC

    [Header("Settings")]
    public float typingSpeed; //velocidade da fala

    //Variáveis de controle
    public bool isShowing; //se a janela está visível
    private int index; //contagem de laço de repetição - index das sentenças
    private string [] sentences;

    public static DialogueControl instance;

    //Awake é chamado antes de todos os Start() na hierarquia de execução de scrpts
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    //pular para a próxima fala
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else //acabam as frases
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
            }
        }
    }

    //chamar a próxima fala
    public void Speech(string[] txt)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }

    }
}
