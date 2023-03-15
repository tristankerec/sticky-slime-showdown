using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float attackDistance = 2.0f;
    public CharacterController controller;
    private PlayerStatus status;

    private float gravity = -9.81f;
    public float moveSpeed = 2;
    public float attackDamage = 11.0f;
    public float rotateSpeed = 10;
    private float jumpHeight = 1;
    Vector3 playerVelocity;
    Vector3 rotateDirection;
    float yVelocity = 0;
    private new Animation animation;
    private bool isControllable = true;
    private GameObject[] enemies;
    public AnimationClip playerAttackClip;
    private bool hasAttacked = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<CharacterController>();
        status = GetComponent<PlayerStatus>();
        animation = GetComponent<Animation>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Number of Enemies: "+enemies.Length);
    }

    public bool IsControllable
    {
        get { return isControllable; }
        set { isControllable = value; }
    }

    public void Respawn()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    int FindClosest()
    {
        Transform target;
        float minDistance = 20000;
        int closest = -1;
        for (int i = 0; i < enemies.Length; i++)
        {
            AIStatus enemyStatus = enemies[i].GetComponent(typeof(AIStatus)) as AIStatus;
            if (!enemyStatus.isAlive())
                continue;
            target = enemies[i].transform;
            Vector3 toPlayer = target.position - transform.position;

            float dist = toPlayer.magnitude;

            toPlayer.y = 0;
            toPlayer = Vector3.Normalize(toPlayer);

            //Forward in world space
            Vector3 forward = transform.TransformDirection(new Vector3(0, 0, 1));
            forward.y = 0;
            forward = Vector3.Normalize(forward);

            if (dist <= attackDistance)
            {
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closest = i;
                }
            }

        }

        return closest;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 100, 0, 100, 50), " " + status.GetHealth().ToString());
    }

    public void InitAttack(int closestEnemyInd)
    {
        StartCoroutine(Attack(closestEnemyInd));
    }

    IEnumerator Attack(int closestEnemyInd)
    {
        AIStatus status = enemies[closestEnemyInd].GetComponent<AIStatus>();
        if (!animation.IsPlaying("attack"))
        {
            animation.CrossFade("attack", 0.2f);
            hasAttacked = false;
        }
        else if (animation.IsPlaying("attack") && !hasAttacked)
        {
            hasAttacked = true;
            //anim.CrossFade("attack", 0.2f);
            yield return new WaitForSecondsRealtime(0.5f);
            Debug.Log("Attacked by: " + gameObject);
            status.ApplyDamage(attackDamage);
        }
    }

    /* Update is called once per frame */
    void FixedUpdate()
    {
        if (!isControllable)
        {
            return;
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Z is 1 if moving forward, -1 otherwise
        if (Input.GetAxis("Fire2") != 0)
        {
            //animation.CrossFade("attack", 0.1f);
            int closestEnemyInd = FindClosest();
            if (closestEnemyInd >= 0)
            {
                InitAttack(closestEnemyInd);

            }
            return;
        }

        playerVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));
        if (playerVelocity.magnitude == 0)
        {
            animation.CrossFade("idle", 0.1f);
        }
        else
        {
            animation.CrossFade("walk", 0.1f);
        }

        //Transforms from local to world space
        playerVelocity = transform.TransformDirection(playerVelocity);

        //Scale by speed
        playerVelocity *= moveSpeed;

        // Changes the height position of the player..
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * (gravity));
        }
        //Apply  gravity
        yVelocity += gravity * Time.deltaTime;

        playerVelocity.y = yVelocity;

        float moveHorz = Input.GetAxis("Horizontal");
        if (moveHorz > 0) //right turn - rotate clockwise, or about +Y
            rotateDirection = new Vector3(0, 1, 0);
        else if (moveHorz < 0) //left turn – rotate counter-clockwise, or about -Y
            rotateDirection = new Vector3(0, -1, 0);
        else
            rotateDirection = new Vector3(0, 0, 0);

        controller.transform.Rotate(rotateDirection, rotateSpeed * Time.deltaTime);
        CollisionFlags flags = controller.Move(playerVelocity * Time.deltaTime);
    }
}