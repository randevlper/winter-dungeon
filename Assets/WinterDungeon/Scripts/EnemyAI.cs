using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	[SerializeField] DungeonCharacter dungeonCharacter;
	
	
	// Update is called once per frame
	void Update () {
		dungeonCharacter.Move(Vector2.up);
	}
}
