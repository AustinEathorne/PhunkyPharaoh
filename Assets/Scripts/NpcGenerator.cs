using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the list of possible sprites + dialogue options & their corresponding responses

public class NpcGenerator : MonoBehaviour 
{
	private bool isInitialized = false;

	// Create new list & dictionary for new dialogue, each list holds a set of lines for the npc to say, dictionary responses (List<string>) corresponds to the lineIndex for the dialogue list
	private List<string> dialogue1 = new List<string>();
	private Dictionary<int, List<string>> dialogueResponses1 = new Dictionary<int, List<string>>();


	private IEnumerator Start()
	{
		List<string> responses = new List<string>();
		// Add new dialogue here

		//Dialogue1
		//Line0
		dialogue1.Add ("Hey, how's it going?"); // dialogue
		responses = new List<string>(); //responses
		responses.Add ("Great");
		responses.Add ("Alright");
		responses.Add ("Bad");
		dialogueResponses1.Add (0, responses); //dicitionary for responses
		//Line1
		dialogue1.Add ("What's your favourite colour?");
		responses = new List<string>();
		responses.Add ("Red");
		responses.Add ("Green");
		responses.Add ("Blue");
		dialogueResponses1.Add (1, responses);
		//Line2
		dialogue1.Add ("You have nice hair!");
		responses = new List<string>();
		responses.Add ("Nah b");
		responses.Add ("Thanks!");
		responses.Add ("Nah, you have nice hair.");
		dialogueResponses1.Add (2, responses);

		//Dialogue2


		this.isInitialized = true;
		yield return null;
	}

	public bool GetIsInitiliazed()
	{
		return this.isInitialized;
	}
}
