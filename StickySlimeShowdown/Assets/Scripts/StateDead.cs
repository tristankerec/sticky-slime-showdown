using UnityEngine;
using System.Collections;

public class StateDead : State
{

	public override void Execute(enemyController character)
	{

        //if (!character.IsDead)
        //{
        //    //If character is alive, call alive state
        //    character.ChangeState(new StateIdle());
        //}
        //else if (character.IsDead)
        //{
        //    //If character is dead, call dead state
        //    character.BeDead();
        //}
        
	}
}
