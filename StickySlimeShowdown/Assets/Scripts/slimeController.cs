using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    private Animator animator;
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
        animator.SetFloat("Speed", moveSpeed);

    }
    else
    {
        animator.SetFloat("Speed", 0.0f);

    }
    if (moveMagnitude > 0)
    {

        Quaternion targetRotation = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
    }

    // Get the distance between the player and the boundary
    float distanceToBoundary = Vector3.Distance(transform.position, boundary.transform.position);

    // Calculate the maximum distance the player can move within the boundary
    float maxDistance = boundary.GetComponent<SphereCollider>().radius;

    // Calculate the direction and distance the player is trying to move
    Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
    float moveDistance = Mathf.Clamp(movement.magnitude * Time.deltaTime, 0f, maxDistance);

    // Calculate the new position of the player within the boundary
    Vector3 newPosition = transform.position + moveDirection * moveDistance;
    newPosition = boundary.transform.position + (newPosition - boundary.transform.position).normalized * Mathf.Clamp(distanceToBoundary, 0f, maxDistance);

    // Move the player to the new position within the boundary
    controller.Move(newPosition - transform.position);
}

}

