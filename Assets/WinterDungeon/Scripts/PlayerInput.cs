using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
	[SerializeField] DungeonCharacter dungeonCharacter;
	[SerializeField] GameObject arrow;

	// Update is called once per frame
	void Update () {
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetButtonDown ("Fire1")) {
			dungeonCharacter.Attack ();
		}

		if (input != Vector2.zero) {
			if (Input.GetButton ("Turn")) {
				SetArrowRotation (input);
			} else {
				dungeonCharacter.Move (input);
				SetArrowRotation (input);
			}
		}

	}

	void SetArrowRotation (Vector2 dir) {
		Vector3 newAngle = arrow.transform.eulerAngles;
		newAngle.z = Vector2.SignedAngle (transform.up, dir);
		arrow.transform.eulerAngles = newAngle;
		dungeonCharacter.FacingDir = dir;
	}
}