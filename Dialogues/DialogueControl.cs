using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    // Define an enum to specify the language for the dialogue
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    // Specify the language for the dialogue
    public idiom language;

    // GameObject for the dialogue UI
    [Header("Components")]
    public GameObject dialogueObj;
    // Image for the profile picture of the actor speaking in the dialogue
    public Image profileSprite;
    // Text object to display the speech text
    public Text speechText;
    // Text object to display the name of the actor speaking in the dialogue
    public Text actorNameText;

    // Settings for the dialogue
    [Header("Settings")]
    // The speed at which the text is typed
    public float typingSpeed;
    // Boolean to indicate whether the dialogue is currently displayed on screen
    public bool isShowing;
    // Index to keep track of the current sentence in the dialogue
    private int index;
    // Array of sentences for the dialogue
    private string[] sentences;
    // Array of names of the actors speaking in the dialogue
    private string[] currentActorName;
    // Array of profile pictures of the actors speaking in the dialogue
    private Sprite[] actorSprite;

    // Reference to the player object
    private Player player;

    // Singleton instance of the dialogue controller
    public static DialogueControl instance;

    private void Awake()
    {
        // Assign the current instance to the singleton instance
        instance = this;
    }

    void Start()
    {
        // Find the player object in the scene and assign it to the player variable
        player = FindObjectOfType<Player>();
    }

    // Coroutine to type out the current sentence
    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Method to move to the next sentence in the dialogue
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                // Move to the next sentence
                index++;
                profileSprite.sprite = actorSprite[index];
                actorNameText.text = currentActorName[index];
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else 
            {
                // End the dialogue
                speechText.text = "";
                actorNameText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
                player.isPaused = false;
            }
        }
    }

    // Method to display the dialogue
    public void Speech(string[] txt, string[] actorName, Sprite[] actorProfile)
    {
        if(!isShowing)
        {
            // Display the dialogue UI
            dialogueObj.SetActive(true);
            sentences = txt;
            currentActorName = actorName;
            actorSprite = actorProfile;
            profileSprite.sprite = actorSprite[index];
            actorNameText.text = currentActorName[index];
            StartCoroutine(TypeSentence());
            isShowing = true;
            player.isPaused = true;
        }

    }
}
