using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NP.FiniteStateMachine;

public class ChaseState : FSMState {

	protected override void OnStateEnter ()
	{
		base.OnStateEnter ();

		StartCoroutine (DoChasing ());
	}

	protected override void OnStateExit ()
	{
		base.OnStateExit ();
	}


	IEnumerator DoChasing(){

		Debug.Log ("Chasing target...");

		yield return new WaitForSecondsRealtime (3);


		int num = Random.Range (0, 2);

		if (num>0)
			Owner.PushInState<AttackState> ();
		else
			Owner.PopState ();


		//Owner.PushInState<AttackState> ();

	}
}
