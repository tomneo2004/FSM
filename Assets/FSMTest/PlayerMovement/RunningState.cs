using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NP.FiniteStateMachine;

public class RunningState : FSMState {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log ("Running state update");

		if (Input.GetKeyUp (KeyCode.W)) {
			Owner.PopState();
			return;
		}

		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			Owner.PopState();
			return;
		}
	}

	public override void Initialize (FSM owner)
	{
		base.Initialize (owner);

		Debug.Log ("Running state init");
	}

	protected override void OnStateEnter ()
	{
		base.OnStateEnter ();

		Debug.Log ("Running state enter");
	}

	protected override void OnStateExit ()
	{
		base.OnStateExit ();

		Debug.Log ("Running state exit");
	}
}
