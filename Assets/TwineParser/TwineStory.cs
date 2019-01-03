using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TwineStorydata{
	public string name;
	public int startNode;
	public string creator;
	public string creatorVersion;
	public string ifid;
	public int zoom;
	public string format;
	public string formatVersion;
	//Options
	//StyleData
	public TwinePassage[] passages;
	public TwineStyle style;
}

[System.Serializable]
public struct TwineStyle
{
	public string role;
	public string id;
	public string type;
}

[System.Serializable]
public struct TwinePassage
{
	public Vector2Int position;
	public Vector2Int size;
	public string name;
	public int pid;
	public string[] tags;
	public string[] lines;
}

public class TwineStory : ScriptableObject {
	public TwineStorydata storyData;
}
