using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;

    public AudioSource audioSource;
    void Start()
    {
        // Start playing the audio if it's not already playing
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void OnDisable()
    {
        // Stop the audio playback when the object is disabled
        audioSource.Stop();
    }

    void Update()
    {
        // Stop the audio playback when the scene changes to "mainscene"
        if (SceneManager.GetActiveScene().name == "mainscene")
        {
            audioSource.Stop();
        }
    }

    void Awake()
    {
        // Check if an instance of the MusicManager already exists
        if (instance == null)
        {
            // If not, set the instance to this object
            instance = this;

            // Set the object to not be destroyed when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this object
            Destroy(gameObject);
        }
    }

}
