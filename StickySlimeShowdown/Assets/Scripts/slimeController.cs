using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class slimeController : MonoBehaviour
{
    public float moveSpeed = 1f;
    private CharacterController controller;
    private Animator animator;
    public GameObject boundary;
    public GameObject[] models;
    private int currentPoints = 0;
    private int pointsThreshold = 10;
    private int index = 1;
    private GameObject currentModel;
    private GameObject parent;

    private float timerDuration = 15.0f;

    private int roundedTime  = 0;


    void Start()
    {
        controller = GetComponentInChildren<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        parent = GameObject.FindWithTag("Parent");
        currentPoints = 0;
        Invoke("GameOver", timerDuration);

    }

    void GameOver() {
        SceneManager.LoadScene(2);
    }

    void Update(){
        timerDuration -= Time.deltaTime;
        roundedTime = Mathf.RoundToInt(timerDuration);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 100, 0, 100, 50),"Lives");
        GUI.Box(new Rect(0, 0, 100, 50),"Points: " + currentPoints.ToString());
        GUI.Box(new Rect(Screen.width - (Screen.width/2) - 25, 0, 100, 50),"Time Left:" + roundedTime.ToString());
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed;
        float moveMagnitude = movement.magnitude;

        float distanceToBoundary = Vector3.Distance(transform.GetChild(0).position, boundary.transform.position);
        float maxDistance = boundary.GetComponent<SphereCollider>().radius;
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        float moveDistance = Mathf.Clamp(movement.magnitude * Time.deltaTime, 0f, maxDistance);
        Vector3 newPosition = transform.GetChild(0).position + moveDirection * moveDistance;
        newPosition = boundary.transform.position + (newPosition - boundary.transform.position).normalized * Mathf.Clamp(distanceToBoundary, 0f, maxDistance);
        
        //if (currentPoints > pointsThreshold)
        //{
        //    foreach (Transform child in transform)
        //    {
        //        Destroy(child.gameObject);
        //    }
        //    GameObject nextPlayerModel = Instantiate(models[index], transform.GetChild(0).position, transform.GetChild(0).rotation);
        //    //nextPlayerModel.name = "Slime_03_Leaf";
        //    if (index == 1)
        //    {
        //        nextPlayerModel.tag = "Player2";

        //    } else if (index == 2) {
        //        nextPlayerModel.tag = "Player3";

        //    }
        //    CharacterController characterController = nextPlayerModel.AddComponent<CharacterController>();
        //    CharacterCollision characterCollision = nextPlayerModel.AddComponent<CharacterCollision>();
        //    characterController.center = new Vector3(0, 0.4f, 0);
        //    characterController.radius = 0.31f;
        //    characterController.height = 0.1f;
        //    Animator newAnim = nextPlayerModel.AddComponent<Animator>();
        //    RuntimeAnimatorController newAnimController = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Kawaii Slimes/Animator/Slime.controller");
        //    newAnim.avatar = AssetDatabase.LoadAssetAtPath<Avatar>("Assets/Kawaii Slimes/Animation/Slime_Anim.fbx");
        //    newAnim.applyRootMotion = true;
        //    newAnim.runtimeAnimatorController = newAnimController;
        //    controller = nextPlayerModel.GetComponentInChildren<CharacterController>();
        //    controller = characterController;
        //    animator = nextPlayerModel.GetComponentInChildren<Animator>();
        //    nextPlayerModel.transform.SetParent(parent.transform);
            

        //    index = index + 1;
        //    pointsThreshold = 5000;
        //}

        // If the player is outside the boundary, move them towards the boundary
        if (distanceToBoundary >= boundary.GetComponent<SphereCollider>().radius)
        {
            //animator.SetFloat("Speed", 0.0f);
            //Debug.Log("HIT BOUNDARY");
            controller.Move(newPosition - transform.GetChild(0).position);
        }
        else
        {
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
                transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, targetRotation, 0.1f);
            }
        }
        
        

    }

    public void addPoints(int value)
    {
        currentPoints = currentPoints + value;
        if (currentPoints >= pointsThreshold)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            GameObject nextPlayerModel = Instantiate(models[index], transform.GetChild(0).position, transform.GetChild(0).rotation);
            //nextPlayerModel.name = "Slime_03_Leaf";
            if (index == 1)
            {
                nextPlayerModel.tag = "Player2";

            }
            else if (index == 2)
            {
                nextPlayerModel.tag = "Player3";

            }
            CharacterController characterController = nextPlayerModel.AddComponent<CharacterController>();
            CharacterCollision characterCollision = nextPlayerModel.AddComponent<CharacterCollision>();
            characterController.center = new Vector3(0, 0.4f, 0);
            characterController.radius = 0.31f;
            characterController.height = 0.1f;
            Animator newAnim = nextPlayerModel.AddComponent<Animator>();
            RuntimeAnimatorController newAnimController = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Kawaii Slimes/Animator/Slime.controller");
            newAnim.avatar = AssetDatabase.LoadAssetAtPath<Avatar>("Assets/Kawaii Slimes/Animation/Slime_Anim.fbx");
            newAnim.applyRootMotion = true;
            newAnim.runtimeAnimatorController = newAnimController;
            controller = nextPlayerModel.GetComponentInChildren<CharacterController>();
            controller = characterController;
            animator = nextPlayerModel.GetComponentInChildren<Animator>();
            nextPlayerModel.transform.SetParent(parent.transform);


            index = index + 1;
            pointsThreshold = 5000;
        }
    }

    public void killCharacter()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        print("Dead!");
        yield return new WaitForSeconds(5);
        print("Alive!");
    }
}

