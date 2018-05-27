using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NP.FiniteStateMachine;

public class AttackState : FSMState{

	protected override void OnStateEnter ()
	{
		base.OnStateEnter ();

		StartCoroutine (DoAttacking ());
	}

	protected override void OnStateExit ()
	{
		base.OnStateExit ();
	}


	IEnumerator DoAttacking(){

		Debug.Log ("Attacking target...");

		yield return new WaitForSecondsRealtime (7);


		int num = Random.Range (0, 2);

		if (num > 0)
			Owner.PopState ();
		else
			Owner.PopStateUntil<PatrolState> ();

		//Owner.PopStateUntil<PatrolState> ();

	}
}
