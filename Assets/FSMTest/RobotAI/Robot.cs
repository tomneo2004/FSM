using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NP.FiniteStateMachine;

public class Robot : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GetComponent<FSM> ().PushInState<PatrolState> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
