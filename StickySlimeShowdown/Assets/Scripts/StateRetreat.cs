using UnityEngine;
using System.Collections;

public class StateRetreat : State {

	public override void Execute(AIController character)

	{
        
        if (!character.IsDead && character.CanEscape())
        {
            //If not dead, run away until reached a far enough position away
            //character.ChangeState(new StateRetreat());
            character.Escape();

        }
        else if (!character.IsDead && !character.CanEscape())
        {
            character.ChangeState(new StateIdle());
        }
        else if (character.IsDead)
        {
            //If character is dead, call dead state
            character.ChangeState(new StateDead());
        }
    }
}
