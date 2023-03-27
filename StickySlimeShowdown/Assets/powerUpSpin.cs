using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSpin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.Euler(0, Time.time * 100, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
    }
}
