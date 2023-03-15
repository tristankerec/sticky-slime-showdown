using UnityEngine;
using System.Collections;

public class StateWalk : State {

	public override void Execute(AIController character)
	{
        //If health less than 10%, Retreat!
        if (!character.IsDead && character.EnemySeen() && character.getHealth() < character.getMaxHealth() * 0.1f)
        {
            character.ChangeState(new StateRetreat());
        } else if (!character.IsDead && character.EnemySeen() && character.EnemyInRange())
        {
            //If seen and in range, attack
            Debug.Log("character " + character.name+" is changing to attack!");
            character.ChangeState(new StateAttack());

        }
        else if (!character.IsDead && character.EnemySeen() && !character.EnemyInRange())
        {
            //If seen and out of range, approach
            //character.ChangeState(new StateApproach());

            if (character.EnemyInWalkRange())
            {
                //character.ChangeState(new StateWalk());
                character.BeWalking();
            }
            else
            {
                character.ChangeState(new StateRun());
            }
            
            
        }
        else
        {
            //Otherwise, idle away
            if (character.IsDead)
            {
                character.ChangeState(new StateDead());
            }
            else
            {
                character.ChangeState(new StateIdle());
            }
        }
	}
}
