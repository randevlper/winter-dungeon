using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gold {
	[System.Serializable]
	public class FiniteStateMachineNode {
		public System.Action OnEnter;
		public System.Action OnStay;
		public System.Action OnExit;

		FiniteStateMachineNode () { }

		public FiniteStateMachineNode (System.Action onStay, System.Action onEnter = null, System.Action onExit = null) {
			OnEnter += onEnter;
			OnStay += onStay;
			OnExit += onExit;

			OnEnter += Empty;
			OnStay += Empty;
			OnExit += Empty;
		}

		void Empty () { }
	}

	[System.Serializable]
	public class FiniteStateMachine {

		FiniteStateMachineNode _currentState;
		Dictionary<int, FiniteStateMachineNode> _states;
		bool isRunning;

		int _currentStateID;
		public int CurrentState {
			get { return _currentStateID; }
		}

		public FiniteStateMachine () {
			_states = new Dictionary<int, FiniteStateMachineNode> ();
		}

		public bool Start (int key) {
			FiniteStateMachineNode retval;
			if (_states.TryGetValue (key, out retval)) {
				_currentState = retval;
				_currentStateID = key;
				isRunning = true;
				return true;
			}
			Debug.LogError ("WARNING - " + key + " does not exsist! Cannot start the StateMachine!");
			return false;
		}

		public void Tick () {
			if (isRunning) {
				_currentState.OnStay ();
			}
		}

		public bool Add (FiniteStateMachineNode state, int key) {
			FiniteStateMachineNode retval;
			if (_states.TryGetValue (key, out retval)) {
				return false;
			} else {
				_states.Add (key, state);
				return true;
			}
		}

		public bool ChangeState (int key) {
			FiniteStateMachineNode retval;
			if (_states.TryGetValue (key, out retval)) {
				_currentState.OnExit ();
				_currentState = retval;
				_currentStateID = key;
				_currentState.OnEnter ();
				return true;
			}
			return false;
		}
	}
}