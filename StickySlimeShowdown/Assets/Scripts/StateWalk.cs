using UnityEngine;
using System.Collections;

public class StateWalk : State {

	public override void Execute(enemyController character)
	{

        //If player is attackable, attack
        if (character.canAttack())
        {
            character.ChangeState(new StateAttack());
            //If player is a threat, die
        }
        else if (character.canDie())
        {
            character.ChangeState(new StateDead());
            //Else, continue to walk
        }
        else
        {
            character.beWalking();
        }

	}
}
