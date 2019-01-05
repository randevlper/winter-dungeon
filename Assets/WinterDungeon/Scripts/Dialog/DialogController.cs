using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour {
	[SerializeField] TwineStory story;

	//Scene Items
	[SerializeField] GameObject dialogBox;
	[SerializeField] TextMeshProUGUI dialogText;

	[SerializeField] GameObject nameBox;
	[SerializeField] TextMeshProUGUI nameText;

	[SerializeField] GameObject portraits;
	[SerializeField] GameObject buttons;

	//Debug items
	[SerializeField] string startName = "start";

	//Working varibles
	TwinePassage currentPassage;
	int currentLine;

	void Awake () {
		currentPassage = story.GetPassage (startName);
		NextLine();
	}

	void Update () {
		if(Input.GetButtonDown("Fire1")){
			NextLine();
		}
	}

	void NextLine () {
		dialogText.text = currentPassage.lines[currentLine];
		currentLine++;
	}
}