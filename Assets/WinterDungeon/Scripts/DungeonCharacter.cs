using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCharacter : MonoBehaviour {
	[SerializeField] TileNavAgent tileNavAgent;
	[SerializeField] DamageableBody damageableBody;
	public CharacterFaction faction;

	bool hasMoved;
	public bool HasMoved {
		get { return hasMoved; }
		set { hasMoved = value; }
	}

	public float meleeDamage = 1;
	public float meleeRange = 1.0f;

	public ContactFilter2D combatMask;
	public LayerMask navigationMask;

	[SerializeField] Vector2 facingDir;
	public Vector2 FacingDir {
		set { facingDir = value; }
		get { return facingDir; }
	}

	void Awake () {
		hits = new RaycastHit2D[4];
		damageableBody.onDeath += OnDeath;
		tileNavAgent.Move (Vector2.zero);
		damageableBody.Restore ();
	}

	void Start () {
		GameManager.instance.AddCharacter (this);
	}

	void Update()
	{
		Debug.DrawLine(transform.position, transform.position + (Vector3)FacingDir, Color.cyan);
	}

	public bool Move (Vector2 dir) {
		if (!hasMoved && (dir != Vector2.zero)) {
			//Need to check where they are moving to see if they should attack/melee
			RaycastHit2D hit = Physics2D.Raycast (transform.position, dir, 1f, navigationMask);

			if (!hit) {
				tileNavAgent.Move (dir);
				FacingDir = dir.normalized;
				//Debug invoke, should be replaced by the transition to the phase when the character moves
				hasMoved = true;
				GameManager.instance.Moved ();
				//Invoke ("EnableMove", moveCoolDown);
				return true;
			}
		}
		return false;
	}

	RaycastHit2D[] hits;
	public void Attack () {
		int numHits = Physics2D.Raycast (transform.position, FacingDir, combatMask, hits, meleeRange);
		Debug.Log(facingDir);
		for (int i = 0; i < numHits; i++) {
			Debug.Log(hits[i]);
			IDamageable damageable = hits[i].collider.gameObject.GetComponent<IDamageable> ();
			damageable?.Damage (new DamageInfo (meleeDamage));
		}
		hasMoved = true;
		GameManager.instance.Moved();
	}

	void EnableMove () {
		hasMoved = false;
	}

	void OnDeath (DamageInfo hit) {
		GameManager.instance.RemoveCharacter (this);
		gameObject.SetActive (false);
	}

	void OnValidate () {
		tileNavAgent = GetComponent<TileNavAgent> ();
		damageableBody = GetComponent<DamageableBody> ();
	}
}