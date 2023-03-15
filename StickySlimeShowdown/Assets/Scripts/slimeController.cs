using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    private Animator animator;
    public AnimationClip idleClip;
    public AnimationClip walkClip;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed;
        float moveMagnitude = movement.magnitude;

        if (moveMagnitude >= 1)
        {
            //animator.SetFloat("Speed", 4f);
            animator.SetFloat("Speed", 1.0f);

        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
            //animator.SetFloat("Speed", 0.0f);

        }
        if (moveMagnitude > 0)
        {

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }


        //controller.Move(movement * Time.deltaTime);
    }
}

