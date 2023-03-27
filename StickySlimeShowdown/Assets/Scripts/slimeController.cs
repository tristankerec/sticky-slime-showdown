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
    private int pointsThreshold = 60;
    private int index = 1;
    private int numLives = 3;
    private GameObject currentModel;
    private GameObject parent;
    public GameObject spherePrefab;
    private bool slimeAlive = true;
    int aliveCond = 1;
    

    float powerUpStartTime = 0f;

    string whichPower = "";

    private bool isPowerUpActive = false;

    private float timerDuration = 240.0f;


    public static string SecondsToMinutesAndSeconds(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        int remainingSeconds = Mathf.RoundToInt(seconds % 60f);
        if (remainingSeconds == 60)
        {
            minutes++;
            remainingSeconds = 0;
        }
        return string.Format("{0:00}:{1:00}", minutes, remainingSeconds);
    }


    void Start()
    {
        controller = GetComponentInChildren<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        parent = GameObject.FindWithTag("Parent");
        currentPoints = 0;
        Invoke("GameOver", timerDuration);

    }

    public void IncreaseSpeed(){
        if (isPowerUpActive == true) {
            if (whichPower == "Inc")
            {
                RestoreIncSpeed();
            }
            else {
                RestoreDecSpeed();
            }
        }
        // Save the original moveSpeed
        float originalSpeed = moveSpeed;
        // Increase the moveSpeed
        moveSpeed += 2f;
        animator.speed = 1.3f;
        // Wait for 5 seconds
        whichPower = "Inc";
        isPowerUpActive = true;
        powerUpStartTime = Time.time;
        //Invoke("RestoreIncSpeed", 10f);
    }

    public void DecreaseSpeed(){
        if (isPowerUpActive == true)
        {
            if (whichPower == "Inc")
            {
                RestoreIncSpeed();
            }
            else
            {
                RestoreDecSpeed();
            }
        }
        whichPower = "Dec";
        // Save the original moveSpeed
        float originalSpeed = moveSpeed;
        // Increase the moveSpeed
        // moveSpeed -= f;
        animator.speed = 0.8f;
        // Wait for 5 seconds
        isPowerUpActive = true;
        powerUpStartTime = Time.time;
        //Invoke("RestoreDecSpeed", 10f);
    }

    public void AddBonus(){
        addPoints(50);
    }


    void RestoreIncSpeed(){
        moveSpeed -= 2f;
        animator.speed = 1f;
        isPowerUpActive = false;
    }

    void RestoreDecSpeed(){
        // moveSpeed += 0.5f;
        animator.speed = 1f;
        isPowerUpActive = false;
    }
    

    void GameOver() {
        SceneManager.LoadScene(2);
    }

    void Update(){
        timerDuration -= Time.deltaTime;
    }

    public int GetLives(){
        return numLives;
    }

    public void AddLife(){
        numLives = numLives + 1;
    }

    public void AddSlimeCoinBonus(){
        addPoints(5);
    }


    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 100, 0, 100, 50),"Lives: " + numLives.ToString());
        GUI.Box(new Rect(0, 0, 100, 50),"Points: " + currentPoints.ToString());
        GUI.Box(new Rect(Screen.width - (Screen.width/2) - 25, 0, 100, 50),"Time Left: " + SecondsToMinutesAndSeconds(timerDuration));
        if (isPowerUpActive){
            float remainingTime = 10f - (Time.time - powerUpStartTime);
            if (remainingTime <= 0) {
                if (whichPower == "Inc")
                {
                    RestoreIncSpeed();
                }
                else
                {
                    RestoreDecSpeed();
                }
            }
            GUI.Box(new Rect(Screen.width - (Screen.width/2) - 25, 50, 100, 30), "Power Left: " + Mathf.RoundToInt(remainingTime));
        }
    }

    public void setDead()
    {
        animator.SetFloat("Speed", 0.0f);
        slimeAlive = false;
    }

    void FixedUpdate()
    {



        if (!isAlive() && transform.childCount == 0)
        {
            Debug.Log("Dead");
            return;
        }

        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0) && aliveCond == 1)
        {
            slimeAlive = true;

        }

        if (!isAlive())
        {
            Debug.Log("Invincible. Move to disable Invincibility");
            return;
        }

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

        // If the player is outside the boundary, move them towards the boundary
        if (distanceToBoundary >= boundary.GetComponent<SphereCollider>().radius)
        {
            controller.Move(newPosition - transform.GetChild(0).position);
        }
        else
        {
            if (moveMagnitude >= 1)
            {
                //animator.SetFloat("Speed", moveSpeed);
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
        if (currentPoints >= pointsThreshold && index < 3)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            GameObject nextPlayerModel = Instantiate(models[index], transform.GetChild(0).position, transform.GetChild(0).rotation);
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
            Vector3 sphereLocation = new Vector3(transform.GetChild(0).position.x, 0.01f, transform.GetChild(0).position.z);
            GameObject nextSphere = Instantiate(spherePrefab, sphereLocation, transform.GetChild(0).rotation);
            nextSphere.transform.SetParent(nextPlayerModel.transform);

            nextPlayerModel.transform.SetParent(parent.transform);
            index++;
            pointsThreshold = 260;
        }
    }

    public void loseLife()
    {
        slimeAlive = false;
        aliveCond = 0;
        isPowerUpActive = false;
        StartCoroutine(Die());
        
        numLives--;
    }

    IEnumerator Die()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        yield return new WaitForSeconds(5);
        Vector3 newPos = new Vector3(0.0f, -0.009995013f, 0.0f);
        Quaternion newQuaternion = new Quaternion(0, 0, 0, 0);

        GameObject nextPlayerModel = Instantiate(models[index-1], newPos, newQuaternion);
        if (index == 1)
        {
            nextPlayerModel.tag = "Player";

        }
        if (index == 2)
        {
            nextPlayerModel.tag = "Player2";

        }
        else if (index == 3)
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
        Vector3 sphereLocation = new Vector3(0.0f, 0.01f, 0.0f);
        GameObject nextSphere = Instantiate(spherePrefab, sphereLocation, nextPlayerModel.transform.rotation);
        nextSphere.transform.SetParent(nextPlayerModel.transform);

        nextPlayerModel.transform.SetParent(parent.transform);
        aliveCond = 1;

    }

    public bool isAlive()
    {
        return slimeAlive;
    }


}



