using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    // A serialized field for the background music AudioClip.
    [SerializeField] private AudioClip bgmMusic;

    // A private reference to the AudioManager class.
    private AudioManager audioM;

    void Start()
    {
        // Finds the AudioManager object in the scene and assigns it to the audioM variable.
        audioM = FindObjectOfType<AudioManager>();

        // Calls the PlayBGM method of the AudioManager and passes in the bgmMusic AudioClip as a parameter.
        audioM.PlayBGM(bgmMusic);
    }
}
