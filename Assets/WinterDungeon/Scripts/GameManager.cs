using System.Collections;
using System.Collections.Generic;
using Gold;
using UnityEngine;

public enum CharacterFaction {
	NONE = 0,
	PLAYER = 1,
	FRIENDLY = 2,
	ENEMY = 3
}

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public float waitTime = 0.1f;

	public CharacterFaction DungeonTurn {
		get { return (CharacterFaction) dungeonStateMachine.CurrentState; }
	}

	List<DungeonCharacter> Players;
	List<DungeonCharacter> Friendlies;
	List<DungeonCharacter> Enemies;
	FiniteStateMachine dungeonStateMachine;

	void Awake () {
		if (instance == null) {
			instance = this;
			//dungeonTurn = CharacterFaction.PLAYER;
			Players = new List<DungeonCharacter> ();
			Friendlies = new List<DungeonCharacter> ();
			Enemies = new List<DungeonCharacter> ();
			dungeonStateMachine = new FiniteStateMachine ();

			dungeonStateMachine.Add (new FiniteStateMachineNode (PlayerTurn, PlayerTurnEnter, PlayerTurnExit), (int) CharacterFaction.PLAYER);
			dungeonStateMachine.Add (new FiniteStateMachineNode (FriendlyTurn, FriendlyTurnEnter, FriendlyTurnExit), (int) CharacterFaction.FRIENDLY);
			dungeonStateMachine.Add (new FiniteStateMachineNode (EnemyTurn, EnemyTurnEnter, EnemyTurnExit), (int) CharacterFaction.ENEMY);
			dungeonStateMachine.Start ((int) CharacterFaction.PLAYER);
		}
	}

	public void AddCharacter (DungeonCharacter character) {
		switch (character.faction) {
			case CharacterFaction.PLAYER:
				Players.Add (character);
				break;
			case CharacterFaction.FRIENDLY:
				Friendlies.Add (character);
				break;
			case CharacterFaction.ENEMY:
				Enemies.Add (character);
				break;
			default:
				break;
		}
	}

	public void RemoveCharacter (DungeonCharacter character) {

	}

	//Called whenever a character is moved/attacked etc
	public void Moved () {
		dungeonStateMachine.Tick ();
	}

	void PlayerTurn () {
		if (HasAllMoved (Players)) {
			Debug.Log ("FRIENDLY");

			dungeonStateMachine.ChangeState ((int) CharacterFaction.FRIENDLY);
		}
	}

	void PlayerTurnEnter () {
		AllowAllMove (Players);
	}

	void PlayerTurnExit () {

	}

	void FriendlyTurn () {
		if (HasAllMoved (Friendlies)) {
			Debug.Log ("ENEMY");

			dungeonStateMachine.ChangeState ((int) CharacterFaction.ENEMY);
		}
	}

	void FriendlyTurnEnter () {
		AllowAllMove (Friendlies);
		if (Friendlies.Count == 0) {
			//dungeonTurn = CharacterFaction.FRIENDLY;
			dungeonStateMachine.ChangeState ((int) CharacterFaction.ENEMY);
		}

	}

	void FriendlyTurnExit () {

	}

	void EnemyTurn () {

		if (HasAllMoved (Enemies)) {
			Debug.Log ("PLAYER");

			//dungeonTurn = CharacterFaction.PLAYER;
			StartCoroutine (WaitPlayerState ());
		}
	}

	void EnemyTurnEnter () {
		AllowAllMove (Enemies);
		if (Enemies.Count == 0) {
			dungeonStateMachine.ChangeState ((int) CharacterFaction.PLAYER);
		}
	}

	void EnemyTurnExit () {

	}

	IEnumerator WaitPlayerState () {
		yield return new WaitForSeconds (waitTime);
		dungeonStateMachine.ChangeState ((int) CharacterFaction.PLAYER);
	}

	bool HasAllMoved (List<DungeonCharacter> characters) {
		for (int i = 0; i < characters.Count; i++) {
			if (!characters[i].HasMoved) {
				return false;
			}
		}
		return true;
	}

	void AllowAllMove (List<DungeonCharacter> characters) {
		for (int i = 0; i < characters.Count; i++) {
			characters[i].HasMoved = false;
		}
	}

	//Character tells GM what it is
	//Needs to know what each character is done moving/action
	//Character tells GM
}