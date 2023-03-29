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
    private float lastMoveSpeed;

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

    public void setMoveSpeed(float val)
    {
        moveSpeed = val;
    }

    public void setAnimSpeed(float val)
    {
        animator.speed = val;
    }

    public bool beWalking()
    {

        GameObject parent = GameObject.FindWithTag("Parent");
        GameObject player;
        if (parent.transform.childCount < 1)
        {
            player = null;
            moveSpeed = lastMoveSpeed;

        } else
        {
            player = parent.transform.GetChild(0).gameObject;
        }
        


        animator.SetFloat("Speed", moveSpeed);
        if (transform.position.x >= 14.0f && !transform.name.Contains("right"))
        {
            gameObject.transform.position = new Vector3(-14.11f, 0.0f, this.gameObject.transform.position.z);
            if (player != null)
            {
                if (player.tag == "Player")
                {
                    moveSpeed = Random.Range(0.4f, 0.8f);

                }
                else if (player.tag == "Player2")
                {
                    moveSpeed = Random.Range(0.4f, 2.0f);

                }
                else if (player.tag == "Player3")
                {
                    moveSpeed = Random.Range(0.8f, 2.0f);
                }
            }

            if (moveSpeed > 1.0f)
            {
                animator.speed = moveSpeed;
                moveSpeed = 1;

            }
            else
            {
                animator.speed = 1.0f;
            }

        }

        if (transform.position.x <= -15.5f && transform.name.Contains("right"))
        {
            gameObject.transform.position = new Vector3(14.25f, 0.0f, this.gameObject.transform.position.z);
            if (player != null)
            {
                if (player.tag == "Player")
                {
                    moveSpeed = Random.Range(0.4f, 0.8f);

                }
                else if (player.tag == "Player2")
                {
                    moveSpeed = Random.Range(0.4f, 2.0f);

                }
                else if (player.tag == "Player3")
                {
                    moveSpeed = Random.Range(0.8f, 2.0f);

                }
            }

            if (moveSpeed > 1.0f)
            {
                animator.speed = moveSpeed;
                moveSpeed = 1;
            }
            else
            {
                animator.speed = 1.0f;
            }
        }
        lastMoveSpeed = moveSpeed;
        return false;
    }
}
