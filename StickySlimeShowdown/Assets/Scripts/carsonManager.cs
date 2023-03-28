using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class carsonManager : MonoBehaviour
{
    public static carsonManager instance;
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
        if (SceneManager.GetActiveScene().name == "Menu")
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
