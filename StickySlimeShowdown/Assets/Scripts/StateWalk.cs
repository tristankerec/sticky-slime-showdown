using UnityEngine;
using System.Collections;

public class StateWalk : State {

	public override void Execute(enemyController character)
	{
            character.beWalking();

	}
}
