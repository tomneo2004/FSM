using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NP.FiniteStateMachine;

public class PatrolState : FSMState {

	protected override void OnStateEnter ()
	{
		base.OnStateEnter ();

		StartCoroutine (DoPatrol ());
	}

	protected override void OnStateExit ()
	{
		base.OnStateExit ();
	}


	IEnumerator DoPatrol(){

		Debug.Log ("Patroling...");

		yield return new WaitForSecondsRealtime (5);

		Owner.PushInState<ChaseState> ();

	}
}
