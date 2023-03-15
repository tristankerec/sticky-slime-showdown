using UnityEngine;
using System.Collections;

public class AIStatus : MonoBehaviour {

	public float health = 20.0f;

	//Uncomment if we need to apply a power-up to the AI
	//private float	maxHealth = 10.0f;

	private bool dead = false;
	private AIController aiController;
	private GameObject target;
	private float maxHealth = 0.0f;


	void Start() {
		aiController = GetComponent<AIController>();
		target = GameObject.FindWithTag("Player");
		maxHealth = health;
	}

	public bool isAlive() { return !dead; }

	public void ApplyDamage(float damage) {
		//Debug.Log("Enemy NPC damage " + damage);
		//Debug.Log("Ouch! " + health);
		health -= damage;
		if (health <= 0 && !aiController.IsDead)
		{
			dead = true;
			health = 0;
			print("***********Dead!*************");
			aiController.IsDead = true;

		}
	}

	public float getHealth()
    {
		return health;
    }

	public float getMaxHealth()
    {
		return maxHealth;
    }
};
