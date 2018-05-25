using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NP.FSM;

public class DefaultState : FSMState {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log ("Default state update");

		if (Input.GetKey (KeyCode.W))
			Owner.ChangeState<WalkingState> ();
	}

	public override void Initialize (FSM owner)
	{
		base.Initialize (owner);

		Debug.Log ("Default state init");
	}

	protected override void OnStateEnter ()
	{
		base.OnStateEnter ();

		Debug.Log ("Default state enter");
	}

	protected override void OnStateExit ()
	{
		base.OnStateExit ();

		Debug.Log ("Default state exit");
	}
}
