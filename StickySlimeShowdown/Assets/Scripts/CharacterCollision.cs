using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    private slimeController slime;
    private AudioSource audioSource;
    private AudioSource slowDown;

    void Start()
    {
        slime = GetComponentInParent<slimeController>();
        audioSource = slime.audioSource;
        slowDown = slime.slowDown;
        
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
            enemyController enemyController = hit.gameObject.GetComponent<enemyController>();
            float moveSpeed = 0.0f;
            if (enemyType == "Slime_01")
            {
                if (playerType == "Player3" || playerType == "Player2")
                {
                    //20% of the time, the player will get a slow down
                    if (Random.value < 0.2f)
                    {
                        slime.DecreaseSpeed();
                        slowDown.Play();

                    }
                    else
                    {
                        audioSource.Play();

                    }
                } else
                {
                    audioSource.Play();


                }
                Debug.Log("Adding 10 points");
                if (hit.gameObject.transform.name.Contains("right"))
                {
                    hit.gameObject.transform.position = new Vector3(14.25f, 0.0f, hit.gameObject.transform.position.z);
                }
                else
                {
                    hit.gameObject.transform.position = new Vector3(-14.11f, 0.0f, hit.gameObject.transform.position.z);
                }

                if (playerType == "Player")
                {
                    moveSpeed = Random.Range(0.4f, 0.8f);
                }
                else if (playerType == "Player2")
                {
                    moveSpeed = Random.Range(0.4f, 2.0f);
                }
                else if (playerType == "Player3")
                {
                    moveSpeed = Random.Range(0.8f, 2.0f);
                }

                if (moveSpeed > 1.0f)
                {
                    enemyController.setAnimSpeed(moveSpeed);
                    enemyController.setMoveSpeed(1.0f);

                }
                else
                {
                    enemyController.setAnimSpeed(1.0f);
                    enemyController.setMoveSpeed(moveSpeed);
                }

                slime.addPoints(10);


            } else if (enemyType == "Slime_03_Leaf")
            {
                if (playerType == "Player")
                {
                    Debug.Log("Player Lost a Life");
                    slime.setDead();
                    slime.loseLife();
                }
                else
                {
                    //20% of the time, the player will get a slow down
                    if (Random.value < 0.2f)
                    {
                        slime.DecreaseSpeed();
                        slowDown.Play();

                    } else
                    {
                        audioSource.Play();

                    }

                    
                    Debug.Log("Adding 20 points");
                    if (hit.gameObject.transform.name.Contains("right"))
                    {
                        hit.gameObject.transform.position = new Vector3(14.25f, 0.0f, hit.gameObject.transform.position.z);


                    }
                    else
                    {
                        hit.gameObject.transform.position = new Vector3(-14.11f, 0.0f, hit.gameObject.transform.position.z);
                    }

                    if (playerType == "Player")
                    {
                        moveSpeed = Random.Range(0.4f, 0.8f);
                    }
                    else if (playerType == "Player2")
                    {
                        moveSpeed = Random.Range(0.4f, 2.0f);
                    }
                    else if (playerType == "Player3")
                    {
                        moveSpeed = Random.Range(0.8f, 2.0f);
                    }

                    if (moveSpeed > 1.0f)
                    {
                        enemyController.setAnimSpeed(moveSpeed);
                        enemyController.setMoveSpeed(1.0f);

                    }
                    else
                    {
                        enemyController.setAnimSpeed(1.0f);
                        enemyController.setMoveSpeed(moveSpeed);
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
                    //20% of the time, the player will get a slow down
                    if (Random.value < 0.2f)
                    {
                        slime.DecreaseSpeed();
                        slowDown.Play();

                    }
                    else
                    {
                        audioSource.Play();

                    }
                    Debug.Log("Adding 30 points");
                    if (hit.gameObject.transform.name.Contains("right"))
                    {
                        hit.gameObject.transform.position = new Vector3(14.25f, 0.0f, hit.gameObject.transform.position.z);
                    }
                    else
                    {
                        hit.gameObject.transform.position = new Vector3(-14.11f, 0.0f, hit.gameObject.transform.position.z);
                    }

                    if (playerType == "Player")
                    {
                        moveSpeed = Random.Range(0.4f, 0.8f);
                    }
                    else if (playerType == "Player2")
                    {
                        moveSpeed = Random.Range(0.4f, 2.0f);
                    }
                    else if (playerType == "Player3")
                    {
                        moveSpeed = Random.Range(0.8f, 2.0f);
                    }

                    if (moveSpeed > 1.0f)
                    {
                        enemyController.setAnimSpeed(moveSpeed);
                        enemyController.setMoveSpeed(1.0f);

                    }
                    else
                    {
                        enemyController.setAnimSpeed(1.0f);
                        enemyController.setMoveSpeed(moveSpeed);
                    }

                    slime.addPoints(30);
                }
            }
        }
    }
}