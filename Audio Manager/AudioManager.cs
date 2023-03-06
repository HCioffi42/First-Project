using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for managing audio in the game.
public class AudioManager : MonoBehaviour
{
    // This static field holds a reference to the single instance of the AudioManager in the game.
    public static AudioManager instance;

    // This field holds a reference to the AudioSource component used to play audio in the game.
    [SerializeField] private AudioSource audioSource;

    // This method is called when the AudioManager object is first created.
    private void Awake()
    {
        // If no AudioManager instance exists yet, set this object as the instance and mark it as persistent across scene changes.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // If another AudioManager instance already exists, destroy this instance to ensure that only one AudioManager exists at any time.
        else
        {
            Destroy(gameObject);
        }
    }

    // This method plays the specified AudioClip as background music.
    public void PlayBGM(AudioClip audio)
    {
        // Set the AudioClip to be played by the AudioSource.
        audioSource.clip = audio;

        // Play the AudioSource.
        audioSource.Play();
    }
}
