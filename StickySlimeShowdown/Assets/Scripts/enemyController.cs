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
        //TODO: if player is dead, return false
        GameObject player = GameObject.FindWithTag("Player");
        //Debug.Log(this.transform.GetChild(1).gameObject.name);
        //Debug.Log("----");


        return false;
    }

    public bool canDie()
    {
        //TODO: if player is dead, return false
        GameObject player = GameObject.FindWithTag("Player");

        return false;
    }

    public bool beWalking()
    {
        //TODO: if player is dead, return false
        GameObject player = GameObject.FindWithTag("Player");

        return false;
    }
}
