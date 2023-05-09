using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.Animations;
using UnityEngine.Animations;
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
    private int numLives = 1;
    private GameObject currentModel;
    private GameObject parent;
    public GameObject spherePrefab;
    public GameObject arrowPrefab;
    private bool slimeAlive = true;
    public AudioSource audioSource;
    public AudioSource slowDown;
    int aliveCond = 1;
    private float remainingTime = 0.0f;


    float powerUpStartTime = 0f;
    float messageStart = 0f;

    string whichPower = "";
    string powerMessage = "";

    private bool isPowerUpActive = false;
    private bool displayMessage = false;

    private float timerDuration = 240.0f;

    public AudioSource deathSource;
    public AudioSource respawnSource;


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
        displayMessage = true;
        messageStart = Time.time;
        powerMessage = "Speed Up!";
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
        displayMessage = true;
        messageStart = Time.time;
        powerMessage = "Speed Down!";
    }

    public void ContIncreaseSpeed(float pst, float msgstrt)
    {
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
        // Save the original moveSpeed
        float originalSpeed = moveSpeed;
        // Increase the moveSpeed
        moveSpeed += 2f;
        animator.speed = 1.3f;
        // Wait for 5 seconds
        whichPower = "Inc";
        isPowerUpActive = true;
        //powerUpStartTime = Time.time;
        powerUpStartTime = pst;
        displayMessage = true;
        //messageStart = Time.time;
        messageStart = msgstrt;
        powerMessage = "Speed Up!";
        //Invoke("RestoreIncSpeed", 10f);
    }

    public void ContDecreaseSpeed(float pst, float msgstrt)
    {
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
        //powerUpStartTime = Time.time;
        powerUpStartTime = pst;
        //Invoke("RestoreDecSpeed", 10f);
        displayMessage = true;
        //messageStart = Time.time;
        messageStart = msgstrt;
        powerMessage = "Speed Down!";
    }

    public void AddBonus(){
        addPoints(50);
        displayMessage = true;
        messageStart = Time.time;
        powerMessage = "50 Points!";
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
        PlayerPrefs.SetInt("Score", currentPoints);
        SceneManager.LoadScene(3);
    }

    void Update(){
        timerDuration -= Time.deltaTime;
    }

    public int GetLives(){
        return numLives;
    }

    public void AddLife(){
        numLives = numLives + 1;
        displayMessage = true;
        messageStart = Time.time;
        powerMessage = "Added a life!";
    }

    public void AddSlimeCoinBonus(){
        addPoints(5);
        displayMessage = true;
        messageStart = Time.time;
        powerMessage = "5 Points!";
    }

    public void addHeartPoints()
    {
        addPoints(25);
        displayMessage = true;
        messageStart = Time.time;
        powerMessage = "25 Points!";

    }

    void OnGUI()
    {
        // Set the font size for all the GUI boxes
        GUI.skin.label.fontSize = 40;
        GUI.skin.box.fontSize = 40;
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1.0f);

        // Calculate the size of the GUI boxes based on the new font size
        Vector2 livesBoxSize = GUI.skin.label.CalcSize(new GUIContent("Lives: " + numLives.ToString()));
        Vector2 pointsBoxSize = GUI.skin.label.CalcSize(new GUIContent("Points: " + currentPoints.ToString()));
        Vector2 timeBoxSize = GUI.skin.label.CalcSize(new GUIContent("Time Left: " + SecondsToMinutesAndSeconds(timerDuration)));
        Vector2 powerBoxSize = GUI.skin.label.CalcSize(new GUIContent("Power Left: 00"));

        GUI.Box(new Rect(Screen.width - livesBoxSize.x - 10, 0, livesBoxSize.x + 10, livesBoxSize.y + 10), "Lives: " + numLives.ToString());
        GUI.Box(new Rect(10, 0, pointsBoxSize.x + 10, pointsBoxSize.y + 10), "Points: " + currentPoints.ToString());
        GUI.Box(new Rect(Screen.width / 2 - timeBoxSize.x / 2, 0, timeBoxSize.x + 10, timeBoxSize.y + 10), "Time Left: " + SecondsToMinutesAndSeconds(timerDuration));
        if (isPowerUpActive)
        {
            remainingTime = 10f - (Time.time - powerUpStartTime);
            if (remainingTime <= 0)
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
            powerBoxSize = GUI.skin.label.CalcSize(new GUIContent("Power Left: " + Mathf.RoundToInt(remainingTime)));
            //GUI.Box(new Rect(Screen.width / 2 - powerBoxSize.x / 2, 30, powerBoxSize.x + 10, powerBoxSize.y + 10), "Power Left: " + Mathf.RoundToInt(remainingTime));
            GUI.Box(new Rect(Screen.width - (Screen.width / 2) - powerBoxSize.x/2, 140, powerBoxSize.x + 10, powerBoxSize.y + 10), "Power Left: " + Mathf.RoundToInt(remainingTime));
        }
        if (displayMessage)
        {
            float remainingTime = 5f - (Time.time - messageStart);
            if (remainingTime <= 0)
            {
                if (powerMessage == "Respawning in: ")
                {
                    respawnSource.Play();
                }
                displayMessage = false;
            }
            int respawnFlag = 0;
            if (powerMessage == "Respawning in: ")
            {
                powerMessage = powerMessage + SecondsToMinutesAndSeconds(remainingTime);
                respawnFlag = 1;
            }
            Vector2 messageBoxSize = GUI.skin.label.CalcSize(new GUIContent(powerMessage));

            if (isPowerUpActive)
            {
                //GUI.Box(new Rect(Screen.width / 2 - messageBoxSize.x / 2, 60, messageBoxSize.x + 10, messageBoxSize.y + 10), powerMessage);
                GUI.Box(new Rect(Screen.width - (Screen.width / 2) - messageBoxSize.x/2, 70, messageBoxSize.x + 10, messageBoxSize.y + 10), powerMessage);

            }
            else
            {
                GUI.Box(new Rect(Screen.width - (Screen.width / 2) - messageBoxSize.x / 2, 70, messageBoxSize.x + 10, messageBoxSize.y + 10), powerMessage);

                //GUI.Box(new Rect(Screen.width / 2 - messageBoxSize.x / 2, 30, messageBoxSize.x + 10, messageBoxSize.y + 10), powerMessage);
            }
            if (respawnFlag == 1)
            {
                powerMessage = "Respawning in: ";
            }
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
        //if (!isAlive())
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
            displayMessage = true;
            messageStart = Time.time;
            powerMessage = "Move to disable Invincibility";
            Debug.Log("Invincible. Move to disable Invincibility");
            return;
        }

        if (powerMessage == "Move to disable Invincibility"){
            displayMessage = false;
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

            //New Implementation
            var controller2 = Resources.Load<RuntimeAnimatorController>("Slime");
            var newAnimController = controller2 as RuntimeAnimatorController;
            //animator.runtimeAnimatorController = newAnimController;
            Animator newAnim = nextPlayerModel.AddComponent<Animator>();
            //RuntimeAnimatorController newAnimController = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Kawaii Slimes/Animator/Slime.controller");
            //newAnim.avatar = AssetDatabase.LoadAssetAtPath<Avatar>("Assets/Kawaii Slimes/Animation/Slime_Anim.fbx");
            //newAnim.avatar = Resources.Load<Avatar>("Slime_Anim.fbx");
            newAnim.applyRootMotion = true;
            newAnim.avatar = Resources.Load<Avatar>("Slime_Anim");
            newAnim.runtimeAnimatorController = newAnimController;
            controller = nextPlayerModel.GetComponentInChildren<CharacterController>();
            controller = characterController;
            //animator = nextPlayerModel.GetComponentInChildren<Animator>();
            animator = newAnim;
            

            Vector3 sphereLocation = new Vector3(transform.GetChild(0).position.x, 0.01f, transform.GetChild(0).position.z);
            GameObject nextSphere = Instantiate(spherePrefab, sphereLocation, transform.GetChild(0).rotation);
            //Vector3 arrowLocation = new Vector3(transform.GetChild(0).position.x, 0.01f, transform.GetChild(0).position.z);
            Vector3 arrowLocation = transform.GetChild(0).position + transform.GetChild(0).forward * 0.48f + new Vector3(0.0f, 0.02f, 0.0f);

            Quaternion rotationVal = nextPlayerModel.transform.rotation * Quaternion.AngleAxis(90.0f, Vector3.up);
            GameObject nextArrow = Instantiate(arrowPrefab, arrowLocation, rotationVal);
            nextArrow.transform.SetParent(nextSphere.transform);
            nextSphere.transform.SetParent(nextPlayerModel.transform);
            
            nextPlayerModel.transform.SetParent(parent.transform);
            if (remainingTime <= 0)
            {
                if (whichPower == "Inc")
                {
                    RestoreIncSpeed();
                }
                else if (whichPower == "Dec")
                {
                    RestoreDecSpeed();
                }
            } else
            {
                if (whichPower == "Inc")
                {
                    //RestoreIncSpeed();
                    ContIncreaseSpeed(powerUpStartTime,messageStart);
                }
                else if (whichPower == "Dec")
                {
                    //RestoreDecSpeed();
                    ContDecreaseSpeed(powerUpStartTime, messageStart);
                    
                }

            }
            index++;
            pointsThreshold = 260;
        }
    }

    public void loseLife()
    {
        slimeAlive = false;
        aliveCond = 0;
        isPowerUpActive = false;
        displayMessage = false;

        deathSource.Play();
        StartCoroutine(Die());
        numLives--;
        if (numLives < 1)
        {
            Invoke("GameOver", 0.0f);
        }
        displayMessage = true;
        messageStart = Time.time;
        powerMessage = "Respawning in: ";
        
        
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

        //New Implementation
        var controller2 = Resources.Load<RuntimeAnimatorController>("Slime");
        var newAnimController = controller2 as RuntimeAnimatorController;
        //animator.runtimeAnimatorController = newAnimController;
        Animator newAnim = nextPlayerModel.AddComponent<Animator>();
        //RuntimeAnimatorController newAnimController = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Kawaii Slimes/Animator/Slime.controller");
        //newAnim.avatar = AssetDatabase.LoadAssetAtPath<Avatar>("Assets/Kawaii Slimes/Animation/Slime_Anim.fbx");
        //newAnim.avatar = Resources.Load<Avatar>("Slime_Anim.fbx");
        newAnim.applyRootMotion = true;
        newAnim.avatar = Resources.Load<Avatar>("Slime_Anim");
        newAnim.runtimeAnimatorController = newAnimController;
        controller = nextPlayerModel.GetComponentInChildren<CharacterController>();
        controller = characterController;
        //animator = nextPlayerModel.GetComponentInChildren<Animator>();
        animator = newAnim;

        Vector3 sphereLocation = new Vector3(0.0f, 0.01f, 0.0f);
        GameObject nextSphere = Instantiate(spherePrefab, sphereLocation, nextPlayerModel.transform.rotation);
        Vector3 arrowLocation = new Vector3(0.0f + 0.022f, 0.01f, 0.01f + 0.48f);
        Quaternion rotationVal = nextPlayerModel.transform.rotation * Quaternion.AngleAxis(90.0f, Vector3.up);
        GameObject nextArrow = Instantiate(arrowPrefab, arrowLocation, rotationVal);
        nextArrow.transform.SetParent(nextSphere.transform);
        nextSphere.transform.SetParent(nextPlayerModel.transform);

        nextPlayerModel.transform.SetParent(parent.transform);
        aliveCond = 1;

    }

    public bool isAlive()
    {
        return slimeAlive;
    }


}



