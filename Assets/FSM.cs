using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NP.FSM;

namespace NP.FSM{

	public class FSM : MonoBehaviour {

		[SerializeField]
		string machineName = "";

		/**
		 * Get name of FSM
		 **/
		public string Name{
		
			get{
				return machineName;
			}
		}

		//List of available state
		List<FSMState> states = new List<FSMState>();

		//Current state that is running
		[SerializeField]
		FSMState currentState;

		/**
		 * Get current state that is running
		 **/
		public FSMState CurrentState{
		
			get{

				return currentState;
			}
		}

		void Awake(){

			//Default state for state machine
			ChangeState<FSMRootState> ();
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}
			

		protected virtual bool ChangeState(FSMState toState){

			bool successful = false;

			if (toState == null){
				#if DEBUG
				Debug.LogError("Cant not change state, given in state is null");
				#endif

				return successful;
			}

			#if DEBUG
			if (currentState == null) {
				Debug.LogWarning ("Current state is null");
			}
			#endif


			if (toState && toState != currentState) {

				//If we have current state then tell current state to exit
				if (currentState) {

					(currentState as IFSMState).OnExit ();
				}

				//Set to current state
				currentState = toState;

				//Tell new state to enter state
				(toState as IFSMState).OnEnter();

				successful = true;
			}
				
		
			return successful;
		}

		public virtual bool ChangeState<StateType>() where StateType : FSMState, new(){

			FSMState destState = null;

			//If state is existed in list
			foreach (FSMState s in states) {
			
				if (s is StateType) {
				
					destState = s;

					break;
				}
			}

			//Create state if we can't find it
			if (destState == null) {

				destState = AddState<StateType> ();
			}

			//Change state
			return ChangeState(destState);
		}

		protected virtual bool AddState(FSMState newState){

			bool successful = false;

			if (newState == null) {

				#if DEBUG
				Debug.LogError ("Cnat not add state, given state is null");
				#endif

				successful = false;
			} else {
			
				//Add state to list
				states.Add (newState);

				successful = true;
			}

			return successful;
		}

		public virtual StateType AddState<StateType>() where StateType : FSMState, new(){

			//If state has been added
			foreach (FSMState s in states) {
			
				//State is existed
				if (s is StateType) {

					#if DEBUG
					Debug.LogWarning("Can not add state, state is already existed.");
					#endif

					return s as StateType;
				}
			}


			//Create new state
			StateType state = gameObject.AddComponent<StateType>();

			//Init state
			(state as IFSMState).Initialize (this);

			if (AddState (state))
				return state;

			return null;

		}
	}
}


