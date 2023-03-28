using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class powerUpSpin : MonoBehaviour
{
    public Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.rotation = initialRotation;
    }

    void Update()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion newRotation = Quaternion.AngleAxis(100.0f * Time.deltaTime, Vector3.up);
        Quaternion finalRotation = currentRotation * newRotation;
        transform.rotation = finalRotation;
    }


}
