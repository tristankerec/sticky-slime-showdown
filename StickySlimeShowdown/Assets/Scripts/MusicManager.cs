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
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void OnDisable()
    {
        audioSource.Stop();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "mainscene")
        {
            audioSource.Stop();
            instance = null;
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        audioSource.Play();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
