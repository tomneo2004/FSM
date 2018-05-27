using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NP.FiniteStateMachine;

namespace NP.FiniteStateMachine{

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

			set{
				machineName = value;
			}
		}

		//List of available state
		[SerializeField]
		List<FSMState> states = new List<FSMState>();

		/**
		 * Get current running state
		 **/
		public FSMState CurrentState{
		
			get{ 
			
				return RunningState ();
			}
		}

		//State queue
		readonly Stack<FSMState> stateQueue = new Stack<FSMState>();

		/**
		 * Get state queue
		 **/
		public Stack<FSMState> StateQueue{

			get{

				return stateQueue;
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

		/**
		 * Find state
		 **/
		private StateType FindState<StateType>() where StateType : FSMState{


			foreach (FSMState s in states) {

				if (s is StateType) {

					return (StateType)s;
				}
			}

			return null;
		}

		/**
		 * Get current running state
		 * 
		 * Return null if no running state
		 **/
		private FSMState RunningState(){

			if (stateQueue.Count > 0) {

				return stateQueue.Peek();
			}

			#if DEBUG
			Debug.LogWarning ("No running state");
			#endif

			return null;
		}

		/**
		 * Push state
		 * 
		 * Return true if successful otherwise false
		 **/
		public virtual bool PushInState<StateType>() where StateType : FSMState, new(){

			StateType inState = FindState<StateType> ();

			//Create state if not exist
			if (inState == null) {

				inState = AddState<StateType> ();
			}

			//Push in state if state is existed
			if (inState) {

				//Tell running state to exit
				FSMState runningState = RunningState ();
				if (runningState != null) {

					(runningState as IFSMState).OnExit ();
				}

				//push in new state
				stateQueue.Push (inState);

				//Tell new state to enter
				(inState as IFSMState).OnEnter ();

				return true;
			}


			return false;
		}

		/**
		 * Pop current running state
		 * 
		 * Return null if pop state fail otherwise popped state
		 **/
		public virtual FSMState PopState(){
		
			FSMState popState = RunningState ();

			//If we have current running state
			if (popState) {

				//Tell current running state to exit
				(popState as IFSMState).OnExit();

				//Pop state
				stateQueue.Pop();
			}

			//Tell next state to enter
			FSMState runningState = RunningState ();
			if (runningState) {

				(runningState as IFSMState).OnEnter ();
			}

			return popState;
		}

		protected virtual bool ChangeState(FSMState toState){

			bool successful = false;

			if (toState == null){
				#if DEBUG
				Debug.LogError("Cant not change state, given in state is null");
				#endif

				return successful;
			}

			//Tell current running state to exit and pop it
			FSMState runningState = RunningState ();
			if (runningState) {

				(runningState as IFSMState).OnExit ();
				stateQueue.Pop ();
			}

			//Push in new state and tell it to enter
			stateQueue.Push(toState);
			(toState as IFSMState).OnEnter ();

			successful = true;

			return successful;
		}

		/**
		 * Change current running state to new state
		 **/
		public virtual bool ChangeState<StateType>() where StateType : FSMState, new(){

			FSMState destState = FindState<StateType>();

			//Create state if we can't find it
			if (destState == null) {

				destState = AddState<StateType> ();
			}

			//Change state
			return ChangeState(destState);
		}

		/**
		 * Add state into state list
		 * 
		 * Return true if added otherwise false
		 **/
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

		/**
		 * Create new state and add it into state list by given state type 
		 * 
		 * New created state will be attached to gameobject that have this FSM
		 * 
		 * Return an instance of state type
		 **/
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


