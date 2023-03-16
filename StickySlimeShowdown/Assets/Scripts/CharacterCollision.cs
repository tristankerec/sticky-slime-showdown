using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    private slimeController slime;
    private bool collisionHandled = false;

    void Start()
    {
        slime = GetComponentInParent<slimeController>();
        
    }
    private void OnTriggerEnter(Collider hit)
    {
        // Check if the collision is with the NPC
        if (hit.gameObject.CompareTag("NPC"))
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
                Debug.Log("Adding 10 points");
                hit.gameObject.transform.position = new Vector3(-6.85f, 0.0f, hit.gameObject.transform.position.z);
                slime.addPoints(10);


            } else if (enemyType == "Slime_03_Leaf")
            {
                if (playerType == "Player")
                {
                    Debug.Log("Player Lost a Life");
                    //Kill Player
                }
                else
                {
                    Debug.Log("Adding 20 points");
                    hit.gameObject.transform.position = new Vector3(-6.85f, 0.0f, hit.gameObject.transform.position.z);
                }

            } else if (enemyType == "Slime_03_King")
            {
                if (playerType == "Player" || playerType == "Player2")
                {
                    Debug.Log("Player Lost a Life");
                    //Kill Player
                }
                else
                {

                    Debug.Log("Adding 30 points");
                    hit.gameObject.transform.position = new Vector3(-6.85f, 0.0f, hit.gameObject.transform.position.z);
                }
            }
        }
    }
}