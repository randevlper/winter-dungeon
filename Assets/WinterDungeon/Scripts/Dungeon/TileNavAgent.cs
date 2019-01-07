using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileNavAgent : MonoBehaviour {
	public Tilemap navmap;

	public void Move (Vector2 dir) {
		//Check if the tile I want to move to is there
		//if true move to that tile
		Vector3Int newPos = Vector3Int.FloorToInt (navmap.WorldToLocal (transform.position + (Vector3) dir.normalized));
		TileBase tile = navmap.GetTile (newPos);
		Debug.Log (tile);
		if (tile != null) {
			transform.position = GetCellCenter (transform.position + (Vector3) dir);
			//Debug.Log(dir + " Pos: " + navmap.GetCellCenterWorld(newPos));
			//transform.position = navmap.GetCellCenterWorld(navmap.WorldToCell(newPos));
			//transform.position = newPos;
		}
	}

	Vector3 GetCellCenter (Vector3 worldPos) {
		return navmap.layoutGrid.GetCellCenterWorld (navmap.layoutGrid.WorldToCell (worldPos));
	}
}