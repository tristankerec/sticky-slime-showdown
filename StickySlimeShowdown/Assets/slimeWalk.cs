using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeWalk : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    public GameObject boundary;

    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.position += movement;

        if (movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }

        // Limit the player's movement to the boundaries of the boundary object
        Vector3 boundaryCenter = boundary.transform.position;
        float boundaryRadius = boundary.GetComponent<SphereCollider>().radius;
        float playerRadius = GetComponent<CharacterController>().radius;

        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, boundaryCenter.x - boundaryRadius + playerRadius, boundaryCenter.x + boundaryRadius - playerRadius),
            Mathf.Clamp(transform.position.y, boundaryCenter.y - boundaryRadius + playerRadius, boundaryCenter.y + boundaryRadius - playerRadius),
            Mathf.Clamp(transform.position.z, boundaryCenter.z - boundaryRadius + playerRadius, boundaryCenter.z + boundaryRadius - playerRadius)
        );

        if (Vector3.Distance(transform.position, clampedPosition) > boundary.GetComponent<SphereCollider>().radius)
        {
            // Move the player to the closest point on the boundary if they move too far
            Vector3 closestPoint = boundary.GetComponent<SphereCollider>().ClosestPoint(transform.position);
            transform.position = closestPoint;
        }
        else
        {
            transform.position = clampedPosition;
        }

    }
}
