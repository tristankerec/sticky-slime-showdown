using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeartScript : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        slimeController slime = other.GetComponentInParent<slimeController>();
        if (slime != null)
        {
            Destroy(transform.parent.gameObject);
            audioSource.Play();
            if (slime.GetLives() < 3){
                slime.AddLife();     
            } else
            {
                slime.addHeartPoints();
            }
        }
    }
}
