using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	
	public float	health = 100.0f;
	//private float maxHealth = 1000.0f;
	private bool dead = false;
	private Animation anim;
	private PlayerController playerController;
	private bool hasAnimated = false;
	
	public void AddHealth(float moreHealth){
		health += moreHealth;
	}
	
	public float GetHealth(){
		return health;
	}

    void Start()
    {
        playerController = GetComponent<PlayerController>();
		anim = GetComponent<Animation>();
	}

    public bool isAlive() {return !dead;}
	
	public void ApplyDamage(float damage){
		health -= damage;
		Debug.Log("Ouch! the enemy did " + damage +" damage!");
		if (health <= 0){
			health = 0;
			StartCoroutine(Die());
		}
	}
    
	IEnumerator  Die(){
		dead = true;
		print("Dead!");
		if (!hasAnimated)
		{
			anim.CrossFade("die", 0.2f);
		}
		hasAnimated = true;
		HideCharacter();
		yield return new WaitForSeconds(10);
		print("Alive!");
		playerController.Respawn();
		hasAnimated = false;
		ShowCharacter();
		anim.CrossFade("idle", 0.2f);
		health = 100.0f;
		dead = false;
	}
	
	void HideCharacter(){	
	

		playerController.IsControllable = false;
		
	}
	
	
	
	void ShowCharacter(){


		playerController.IsControllable = true;
	}

	void KillCharacter()
    {
		StartCoroutine(Die());
    }
	
}
