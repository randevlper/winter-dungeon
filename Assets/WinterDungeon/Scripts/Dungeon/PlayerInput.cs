using System.Collections;
using System.Collections.Generic;
using Gold;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
	[SerializeField] DungeonCharacter dungeonCharacter;
	[SerializeField] GameObject arrow;

	FiniteStateMachine stateMachine;

	enum PlayerInputStates {
		MOVE,
		TURN
	}

	private void Awake () {
		stateMachine = new FiniteStateMachine ();
		stateMachine.Add (new FiniteStateMachineNode (Move), (int) PlayerInputStates.MOVE);
		stateMachine.Add (new FiniteStateMachineNode (Turn, TurnEnter, TurnExit), (int) PlayerInputStates.TURN);
		stateMachine.Start ((int) PlayerInputStates.MOVE);
	}

	// Update is called once per frame
	Vector2 input;
	void Update () {
		input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetButtonDown ("Fire1")) {
			dungeonCharacter.Attack ();
		}

		stateMachine.Tick ();

		// if (input != Vector2.zero) {
		// 	if (Input.GetButton ("Turn")) {
		// 		SetArrowRotation (input);
		// 	} else {
		// 		dungeonCharacter.Move (input);
		// 		SetArrowRotation (input);
		// 	}
		// }

	}

	void Move () {
		if (Input.GetButtonDown ("Turn")) {
			stateMachine.ChangeState ((int) PlayerInputStates.TURN);
		}

		SetArrowRotation (input);
		dungeonCharacter.Move (input);
	}

	void Turn () {
		if (Input.GetButtonDown ("Turn")) {
			stateMachine.ChangeState ((int) PlayerInputStates.MOVE);
		}

		SetArrowRotation (input);
	}

	void TurnEnter () {
		arrow.SetActive (true);
	}

	void TurnExit () {
		arrow.SetActive (false);
	}

	void SetArrowRotation (Vector2 dir) {
		if (input != Vector2.zero) {
			Vector3 newAngle = arrow.transform.eulerAngles;
			newAngle.z = Vector2.SignedAngle (transform.up, dir);
			arrow.transform.eulerAngles = newAngle;
			dungeonCharacter.FacingDir = dir;
		}
	}
}