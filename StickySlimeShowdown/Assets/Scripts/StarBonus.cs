using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBonus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        slimeController slime = other.GetComponentInParent<slimeController>();
        if (slime != null)
        {
            Destroy(transform.parent.gameObject);
            slime.AddBonus();
        }
    }
}
