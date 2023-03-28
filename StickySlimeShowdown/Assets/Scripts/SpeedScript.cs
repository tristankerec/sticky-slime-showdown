using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedScript : MonoBehaviour
{
    public AudioSource audioSource;
    
    private void OnTriggerEnter(Collider other)
    {
        slimeController slime = other.GetComponentInParent<slimeController>();
        if (slime != null)
        {
            Destroy(transform.parent.gameObject);
            slime.IncreaseSpeed();
            audioSource.Play();
        }
    }
}
