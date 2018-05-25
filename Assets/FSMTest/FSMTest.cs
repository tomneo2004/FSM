using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NP.FSM;

public class FSMTest : MonoBehaviour {

	FSM stateMachine;

	// Use this for initialization
	void Start () {

		stateMachine = GetComponent<FSM> ();

		if (stateMachine) {

			//add default state
			stateMachine.AddState<DefaultState>();

			//add walking state
			stateMachine.AddState<WalkingState>();

			//change state to default at start
			stateMachine.ChangeState<DefaultState>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
