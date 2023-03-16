using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public float			attackDistance = 1.5f;
	[Range(0.0f, 180.0f)]
	public float fieldOfView = 180.0f;
	[Range(0.0f, 100.0f)]
	public float viewDistance = 10.0f;

	//private float 			attackSpeed = 4.0f;
	private float 			gravity = 50.0f;
	private float			attackValue = 5.0f;
	
	private CharacterController controller;
	private PlayerStatus	playerStatus;
	private AIStatus aiStatus;
	private Transform		target;
	private Transform startingTargetPosition;
	private Vector3			moveDirection = new Vector3(0,0,0);
	private State			currentState;
	private Animation		anim;
	public float rotationSpeed = 4.0f;
	public float runSpeed = 3.0f;
	public float walkSpeed = 1.0f;
	public float walkDistance = 5.0f;
	private bool hasAttacked = false;



	private bool			isControllable = true;
	private bool			isDead = false;

	//This is a hack for legacy animation - we will do this properly later
	private bool			deathStarted = false; 


	public bool 	IsControllable {
		get {return isControllable;}
		set {isControllable = value;}
	}

	public bool IsDead
	{
		get { return isDead; }
		set { isDead = value; }
	}

    // Use this for initialization
    void Start () {

		controller = GetComponent< CharacterController>();
		anim = GetComponent<Animation>();
		GameObject tmp = GameObject.FindWithTag("Player");
		if (tmp != null){
			target=tmp.transform;
			playerStatus = tmp.GetComponent< PlayerStatus>();
		}
		//AnimationClip attackClip = GetComponent<Animation>().GetClip("attack");
		//AnimationEvent animEvent = new AnimationEvent();
		//animEvent.functionName = "Attack";
		//animEvent.time = 0.5f;
		//attackClip.AddEvent(animEvent);
		aiStatus = GetComponent<AIStatus>();
		//ChangeState(new StateIdle());
	}

	public float getHealth()
    {
		return aiStatus.getHealth();
    }

	public float getMaxHealth()
    {
		return aiStatus.getMaxHealth();
    }
	
	public void ChangeState(State newState){
		currentState = newState;
	}
	
	public void BeDead(){
		//This is a hack for legacy animation - we will do this properly later
		if (!deathStarted)
		{
			anim.CrossFade("die", 0.1f);
			deathStarted = true;
			CharacterController controller = GetComponent<CharacterController>();
			controller.enabled = false;
			playerStatus.AddHealth(10);

		}
		moveDirection = new Vector3(0,0,0);

		if (!anim.isPlaying)
        {
			gameObject.SetActive(false);
			this.IsControllable = false;
		}
	}
	
	public void BeIdle(){
		anim.CrossFade("idle", 0.2f);	
		moveDirection = new Vector3(0,0,0);
	}

	public void BeWalking()
    {
		//Calculate Direction
		Vector3 direction = target.position - transform.position;

		//Calculating rotation
		Vector3 rotation = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0.0f);
		float angle = Mathf.Atan2(rotation.x, rotation.z) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, angle, 0);

		//Calculating movement
		Vector3 targetDirection = target.position - transform.position;
		//If the AI is farther than the attack distance, start walking towards the player
		controller.Move(targetDirection.normalized * walkSpeed * Time.deltaTime);
		//Debug.Log("WALKING Towards PLAYER");
		anim["run"].speed = 0.5f;
		anim.CrossFade("run", 0.2f);

	}

	public void BeRunning()
    {
		//Calculate Direction
		Vector3 direction = target.position - transform.position;

		//Calculating rotation
		Vector3 rotation = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0.0f);
		float angle = Mathf.Atan2(rotation.x, rotation.z) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, angle, 0);

		//Calculating movement
		Vector3 targetDirection = target.position - transform.position;
		//If the AI is farther away than the walk distance, run towards the player
		controller.Move(targetDirection.normalized * runSpeed * Time.deltaTime);
		//Debug.Log("RUNNING Towards PLAYER");
		anim["run"].speed = 1.0f;
		anim.CrossFade("run", 0.2f);
	}

	public void BeAttacking() {
		StartCoroutine(Attack());
	}

	void FixedUpdate () {
		
		if (!isControllable)
			return;

        //currentState.Execute(this);
		if (!IsDead)
		{
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
		}
	}

	public bool CanEscape()
    {
		Vector3 distanceFromPlayer = (startingTargetPosition.position - transform.position);
		if (distanceFromPlayer.magnitude <= (viewDistance / 2))
        {
			return true;
        }
		return false;

	}

	public void Escape()
    {
		//Calculate Direction
		Vector3 awayFromPlayer = -(target.position - transform.position).normalized;
		//Calculating rotation
		Vector3 rotation = Vector3.RotateTowards(transform.forward, awayFromPlayer, rotationSpeed * Time.deltaTime, 0.0f);
		float angle = Mathf.Atan2(rotation.x, rotation.z) * Mathf.Rad2Deg;
		//Perform Rotation
		transform.rotation = Quaternion.Euler(0, angle, 0);
		//Perform Movement
		controller.Move(awayFromPlayer * runSpeed * Time.deltaTime);
		//Perform Animation
		anim["run"].speed = 1.0f;
		anim.CrossFade("run", 0.2f);
    }

	IEnumerator Attack() {
		if (!anim.IsPlaying("attack"))
		{
			anim.CrossFade("attack", 0.2f);
			hasAttacked = false;
		}
		else if (anim.IsPlaying("attack") && !hasAttacked)
		{
			hasAttacked = true;
			//anim.CrossFade("attack", 0.2f);
			yield return new WaitForSecondsRealtime(0.5f);
			Debug.Log("Attacked by: " + gameObject);
			playerStatus.ApplyDamage(attackValue);
		}

	}

	public bool EnemySeen()
    {
		if (!playerStatus.isAlive())
        {
			return false;
        }
		float angle;
		Vector3 direction;
		if (this.gameObject.tag == "EnemySpartan")
        {
			//From Lecture 1 - Asset Flow and Movement Basics (Slide 32 on Moodle)
			direction = target.position - transform.position;
			//Calculating rotation (From Lecture 4 - A Closer Look At Rotation and Orientation)
			angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

			if (angle < 0)
            {
				angle *= -1;
            }

			if (angle <= (fieldOfView) && direction.magnitude <= viewDistance)
			{
				//Debug.Log("I've Got him in my sights. angle: " + angle + " (" + this.gameObject + ")");
				startingTargetPosition = target;
				return true;

			}
			
		} else
        {
			//Calculate if the player can be seen by the NPC
			direction = (target.position - transform.position).normalized;
			angle = Vector3.Angle(transform.forward, direction);
			//Debug.Log(direction.magnitude);
			if (angle <= (fieldOfView / 2))
			{
				RaycastHit inSight;

				if (Physics.Raycast(transform.position, direction, out inSight, 100))
				{
					if (inSight.distance <= viewDistance)
					{
						//Debug.Log("I've Got him in my sights. angle: " + angle + " (" + this.gameObject + ")");
						startingTargetPosition = target;
						return true;
					}
				}
			}
		}



		return false;
    }

	public bool EnemyInWalkRange()
    {
		Vector3 targetDirection = target.position - transform.position;
		if (targetDirection.magnitude <= walkDistance)
		{
			return true;
		}
		return false;

	}

	public bool EnemyInRange()
    {
		Vector3 targetDirection = target.position - transform.position;
		if (targetDirection.magnitude <= attackDistance) {
			return true;
		}
		return false;
    }

	void OnDisable()
	{
		/*
		 * If you uncomment this, you need to somehow tell the PlayerController to update
		 * the enemies array by calling GameObject.FindGameObjectsWithTag("Enemy").
		 * Otherwise the reference to a destroyed GameObject will still be in the enemies 
		 * array and you will get null pointer exceptions if you try to access it
		 */

		Destroy(gameObject);
		GameObject.FindGameObjectsWithTag("Enemy");

	}

}