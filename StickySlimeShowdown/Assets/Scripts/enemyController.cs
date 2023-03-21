using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Animator animator;
    private State currentState;
    private CharacterController controller;
    private bool isControllable = true;
    private bool isDead = false;
    private float moveSpeed;

    public bool IsControllable
    {
        get { return isControllable; }
        set { isControllable = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ChangeState(new StateWalk());
        moveSpeed = Random.Range(0.4f, 0.8f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isControllable)
            return;

        currentState.Execute(this);
        if (!IsDead)
        {
            //moveDirection.y -= gravity * Time.deltaTime;
            //controller.Move(moveDirection * Time.deltaTime);
        }

    }

    public void ChangeState(State newState)
    {
        currentState = newState;
    }

    public bool canAttack()
    {
        //TODO: if player can attack, return true
        GameObject player = GameObject.FindWithTag("Player");
        //Debug.Log(this.transform.GetChild(1).gameObject.name);
        //Debug.Log("----");


        return false;
    }

    public bool canDie()
    {
        //TODO: if player is dead, return true
        GameObject player = GameObject.FindWithTag("Player");

        return false;
    }

    public bool beWalking()
    {
        //TODO: if player is dead, return false
        GameObject player = GameObject.FindWithTag("Player");
        
        animator.SetFloat("Speed", moveSpeed);
        Debug.Log(transform.name);
        if (transform.position.x >= 14.0f && !transform.name.Contains("right"))
        {
            gameObject.transform.position = new Vector3(-14.11f, 0.0f, this.gameObject.transform.position.z);
        }

        if (transform.position.x <= -15.5f && transform.name.Contains("right"))
        {
            gameObject.transform.position = new Vector3(14.25f, 0.0f, this.gameObject.transform.position.z);
        }

        return false;
    }
}
