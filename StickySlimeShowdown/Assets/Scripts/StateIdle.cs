using UnityEngine;
using System.Collections;

public class StateIdle : State {

    public override void Execute(AIController character)
    {

        //If health less than 10%, Retreat!
        if (!character.IsDead && character.EnemySeen() && character.getHealth() < character.getMaxHealth() * 0.1f)
        {
            character.ChangeState(new StateRetreat());
        }
        //If see and in range, attack
        else if (!character.IsDead && character.EnemySeen() && character.EnemyInRange())
        {
            
            character.ChangeState(new StateAttack());
            
        }
        //If see and out of range, approach
        else if (!character.IsDead && character.EnemySeen() && !character.EnemyInRange())
        {
            if (character.EnemyInWalkRange())
            {
                character.ChangeState(new StateWalk());

            }
            else
            {
                character.ChangeState(new StateRun());
            }

        }
        //Otherwise, idle away
        else
        {
            if (character.IsDead)
            {
                character.ChangeState(new StateDead());
            }
            else
            {
                character.BeIdle();
            }
        }
    }
}