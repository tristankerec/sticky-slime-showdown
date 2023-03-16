using UnityEngine;
using System.Collections;

public class StateAttack : State {

	public override void Execute(enemyController character)
	{
        
        ////If health less than 10%, Retreat!
        //if (!character.IsDead && character.EnemySeen() && character.getHealth() < character.getMaxHealth() * 0.1f)
        //{
        //    character.ChangeState(new StateRetreat());
        //}
        //else if (!character.IsDead && character.EnemySeen() && character.EnemyInRange())
        //{
        //    //If see and in range, attack
        //    //character.ChangeState(new StateAttack());
        //    character.BeAttacking();
        //    //If see and out of range, approach
        //}
        //else if (!character.IsDead && character.EnemySeen() && !character.EnemyInRange())
        //{
        //    if (character.EnemyInWalkRange())
        //    {
        //        character.ChangeState(new StateWalk());
        //    }
        //}
        //else
        //{
        //    //Otherwise, idle away
        //    if (character.IsDead)
        //    {
        //        character.ChangeState(new StateDead());
        //    }
        //    else
        //    {
        //        character.ChangeState(new StateIdle());
        //    }
        //}
	}
}
