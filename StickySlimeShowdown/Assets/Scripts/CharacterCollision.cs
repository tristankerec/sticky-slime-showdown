using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    private slimeController slime;

    void Start()
    {
        slime = GetComponentInParent<slimeController>();
        
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the collision is with the NPC
        if (hit.gameObject.CompareTag("NPC"))
        {
            // Log a message to the console
            //Debug.Log("Player collided with NPC!");
            Debug.Log("Player collided with " + hit.gameObject.transform.GetChild(1).gameObject.name);
            string enemyType = hit.gameObject.transform.GetChild(1).gameObject.name;
            string playerType = this.gameObject.transform.GetChild(1).gameObject.name;
            if (enemyType == "Slime_01")
            {
                Debug.Log("Adding 10 points");
                slime.addPoints(10);


            } else if (enemyType == "Slime_03_Leaf")
            {
                if (playerType == "Slime_01")
                {
                    Debug.Log("Player Lost a Life");
                }
                else
                {
                    Debug.Log("Adding 20 points");
                }

            } else if (enemyType == "Slime_03_King")
            {
                if (playerType == "Slime_01" || playerType == "Slime_03_Leaf")
                {
                    Debug.Log("Player Lost a Life");
                }
                else
                {

                    Debug.Log("Adding 30 points");
                }
            }
        }
    }
}