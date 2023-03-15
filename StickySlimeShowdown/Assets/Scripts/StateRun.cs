using UnityEngine;
using System.Collections;

public class StateRun : State {

	public override void Execute(AIController character)
	{

        if (!character.IsDead && character.EnemySeen() && character.EnemyInWalkRange())
        {
            character.ChangeState(new StateWalk()); 
        } else if (!character.IsDead && character.EnemySeen() && !character.EnemyInWalkRange())
        {
            character.BeRunning();
        } else
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
