using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    private slimeController slime;
    private AudioSource audioSource;

    void Start()
    {
        slime = GetComponentInParent<slimeController>();
        audioSource = slime.audioSource;
        
    }
    private void OnTriggerEnter(Collider hit)
    {
        // Check if the collision is with the NPC
        if (hit.gameObject.CompareTag("NPC") && slime.isAlive())
        {
            
            // Log a message to the console
            //Debug.Log("Player collided with NPC!");
            Debug.Log("Player collided with " + hit.gameObject.transform.GetChild(1).gameObject.name);
            
            string enemyType = hit.gameObject.transform.GetChild(1).gameObject.name;
            //string playerType = this.gameObject.transform.GetChild(1).gameObject.name;
            string playerType = this.gameObject.tag;
            //Debug.Log(this.gameObject.transform.GetChild(1).gameObject.tag);
            Debug.Log(this.gameObject.tag);
            //Debug.Log("Player type is "+playerType);
            if (enemyType == "Slime_01")
            {
                audioSource.Play();
                Debug.Log("Adding 10 points");
                if (hit.gameObject.transform.name.Contains("right"))
                {
                    hit.gameObject.transform.position = new Vector3(14.25f, 0.0f, hit.gameObject.transform.position.z);


                }
                else
                {
                    hit.gameObject.transform.position = new Vector3(-14.11f, 0.0f, hit.gameObject.transform.position.z);
                }
                slime.addPoints(10);


            } else if (enemyType == "Slime_03_Leaf")
            {
                if (playerType == "Player")
                {
                    Debug.Log("Player Lost a Life");
                    //Kill Player
                    slime.setDead();
                    slime.loseLife();


                }
                else
                {
                    audioSource.Play();
                    Debug.Log("Adding 20 points");
                    if (hit.gameObject.transform.name.Contains("right"))
                    {
                        hit.gameObject.transform.position = new Vector3(14.25f, 0.0f, hit.gameObject.transform.position.z);


                    }
                    else
                    {
                        hit.gameObject.transform.position = new Vector3(-14.11f, 0.0f, hit.gameObject.transform.position.z);
                    }
                    slime.addPoints(20);
                }

            } else if (enemyType == "Slime_03_King")
            {
                if (playerType == "Player" || playerType == "Player2")
                {
                    Debug.Log("Player Lost a Life");
                    //Kill Player
                    slime.setDead();
                    slime.loseLife();
                }
                else
                {
                    audioSource.Play();
                    Debug.Log("Adding 30 points");
                    if (hit.gameObject.transform.name.Contains("right"))
                    {
                        hit.gameObject.transform.position = new Vector3(14.25f, 0.0f, hit.gameObject.transform.position.z);


                    }
                    else
                    {
                        hit.gameObject.transform.position = new Vector3(-14.11f, 0.0f, hit.gameObject.transform.position.z);
                    }
                    slime.addPoints(30);
                }
            }
        }
    }
}