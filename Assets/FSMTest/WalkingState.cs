using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NP.FiniteStateMachine;

public class WalkingState : FSMState {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log ("Walking state update");

		if (Input.GetKeyUp (KeyCode.W)) {
			//Owner.ChangeState<DefaultState> ();
			Owner.PopState();
		}

		if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.LeftShift)) {
			Owner.ChangeState<RunningState> ();
		}
		
	}

	public override void Initialize (FSM owner)
	{
		base.Initialize (owner);

		Debug.Log ("Walking state init");
	}

	protected override void OnStateEnter ()
	{
		base.OnStateEnter ();

		Debug.Log ("Walking state enter");
	}

	protected override void OnStateExit ()
	{
		base.OnStateExit ();

		Debug.Log ("Walking state exit");
	}
}
