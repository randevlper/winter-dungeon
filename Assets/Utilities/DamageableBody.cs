using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBody : MonoBehaviour, IDamageable {

	float health;
	public float maxHealth;
	public bool isIgnoreDamage;
	public Gold.Delegates.ActionValue<float> onHealthChange;
	public Gold.Delegates.ActionValue<DamageInfo> onDamage;
	public Gold.Delegates.ActionValue<DamageInfo> onDeath;
	public Gold.Delegates.ActionValue<DamageInfo> onIgnoreDamage;

	public float Health {
		get {
			return health;
		}

		set {
			health = value;
			Debug.Log(value);
			onHealthChange?.Invoke (value);
		}
	}

	// Use this for initialization
	// void Start () {
	// 	Restore ();
	// }

	public void Restore () {
		Health = maxHealth;
	}

	public void Damage (DamageInfo hit) {
		if (!isIgnoreDamage || hit.killInstantly) {
			if (hit.killInstantly) {
				Health -= maxHealth;
			} else {
				Health -= hit.damage;
			}
			//Debug.Log("Damaged");
			onDamage?.Invoke (hit);
			if (health <= 0) {
				onDeath?.Invoke (hit);
			}
		} else {
			onIgnoreDamage?.Invoke (hit);
		}

	}

	public bool IsDead () {
		return health <= 0;
	}

#if UNITY_EDITOR
	[SerializeField] bool isDebug;
	private void OnDrawGizmos () {
		if (isDebug) {
			drawString (health + "/" + maxHealth, transform.position, Color.red);
		}
	}

	static public void drawString (string text, Vector3 worldPos, Color? colour = null) {
		UnityEditor.Handles.BeginGUI ();

		var restoreColor = GUI.color;

		if (colour.HasValue) GUI.color = colour.Value;
		var view = UnityEditor.SceneView.currentDrawingSceneView;
		if (view != null) {
			Vector3 screenPos = view.camera.WorldToScreenPoint (worldPos);

			if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0) {
				GUI.color = restoreColor;
				UnityEditor.Handles.EndGUI ();
				return;
			}

			Vector2 size = GUI.skin.label.CalcSize (new GUIContent (text));
			GUI.Label (new Rect (screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
			GUI.color = restoreColor;
			UnityEditor.Handles.EndGUI ();
		}

	}
#endif
}