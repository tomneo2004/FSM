using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NP.FiniteStateMachine;

namespace NP.FiniteStateMachine{

	public interface IFSMState{

		void Initialize (FSM owner);
		void OnEnter();
		void OnExit ();

	}

	public class FSMState : MonoBehaviour, IFSMState {

		string stateName = "";

		/**
		 * Get name of state
		 **/
		public string Name{
		
			get{

				return stateName;
			}
		}

		//State machine that own this state
		FSM stateMachine;

		/**
		 * Get state machine that own this state
		 **/
		public FSM Owner{
		
			get{

				return stateMachine;
			}
		}

		public virtual void Initialize(FSM owner){

			stateMachine = owner;

			Debug.Log ("base state init");
		}
			

		void IFSMState.OnEnter(){
		
			enabled = true;
			OnStateEnter ();
		}

		void IFSMState.OnExit(){
			OnStateExit ();
			enabled = false;
		}

		protected virtual void OnStateEnter(){
		}

		protected virtual void OnStateExit(){
		}

		public virtual void OnEnable(){

			if (stateMachine == null)
				enabled = false;

			//Make sure state is not enabled if it is not current state
			if (stateMachine && this != stateMachine.CurrentState)
				enabled = false;
		}

		public virtual void OnDisable(){

			if (stateMachine == null)
				enabled = false;
			
			if (stateMachine && stateMachine.CurrentState == null) {

				enabled = false;
				return;
			}
				
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}
			
	}

	public class FSMRootState: FSMState{
	
		public override void OnEnable(){
		
			base.OnEnable ();
		}

		public override void OnDisable(){

			base.OnDisable ();
		}
	}

}


