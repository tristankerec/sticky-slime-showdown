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
    public GameObject boundary;
    

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

        // Get the distance between the player and the boundary
        float distanceToBoundary = Vector3.Distance(transform.position, boundary.transform.position);
        
        // If the player is outside the boundary, move them towards the boundary
        if (distanceToBoundary > boundary.GetComponent<SphereCollider>().radius)
        {
            Vector3 directionToBoundary = (boundary.transform.position - transform.position).normalized;
            Vector3 boundaryPosition = transform.position + directionToBoundary * (distanceToBoundary - boundary.GetComponent<SphereCollider>().radius);
            controller.Move((boundaryPosition - transform.position).normalized * moveSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(movement * Time.deltaTime);
        }

        //controller.Move(movement * Time.deltaTime);
    }
}

