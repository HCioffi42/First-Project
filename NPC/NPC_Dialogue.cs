using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange; // range in which the player can interact with the NPC
    public LayerMask playerLayer; // the layer where the player object is

    public DialogueSettings dialogue; // the dialogue object containing the NPC's dialogue

    bool playerHit; // whether or not the player is within range of the NPC

    // lists to store the NPC's dialogue information
    private List<string> sentences = new List<string>();
    private List<string> actorName = new List<string>();
    private List<Sprite> actorSprite = new List<Sprite>();

    private void Start()
    {
        GetNPCInfo(); // get the NPC's dialogue information and store it in the appropriate lists
    }

    void Update()
    {
        // if the player hits the "E" key and is within range of the NPC, show the dialogue
        if(Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueControl.instance.Speech(sentences.ToArray(), actorName.ToArray(), actorSprite.ToArray());
        }
    }

    void GetNPCInfo()
    {
        // loop through the NPC's dialogue and get the appropriate sentences, actor names, and sprites based on the current language setting
        for(int i = 0; i < dialogue.dialogues.Count; i++)
        {
            switch(DialogueControl.instance.language)
            {
                case DialogueControl.idiom.pt:
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);
                    break;

                case DialogueControl.idiom.eng:
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;

                case DialogueControl.idiom.spa:
                    sentences.Add(dialogue.dialogues[i].sentence.spanish);
                    break;
            }

            actorName.Add(dialogue.dialogues[i].actorName);
            actorSprite.Add(dialogue.dialogues[i].profile);
        }
    }

    // FixedUpdate is used by physics-related code
    void FixedUpdate()
    {
        ShowDialogue(); // check whether or not the player is within range of the NPC
    }

    void ShowDialogue()
    {
        // check if there is a collider within the NPC's dialogue range that belongs to the player layer
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);

        if(hit != null) // if the player is within range, set playerHit to true
        {
            playerHit = true;
        }
        else // if the player is not within range, set playerHit to false
        {
            playerHit = false;
        }
    }

    // draw the dialogue range gizmo in the Unity editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
