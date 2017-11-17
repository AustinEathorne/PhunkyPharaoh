using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the list of possible sprites + dialogue options & their corresponding responses

public class NpcGenerator : MonoBehaviour 
{
	private bool isInitialized = false;

	// Create new list & dictionary for new dialogue, each list holds a set of lines for the npc to say, dictionary responses (List<string>) corresponds to the lineIndex for the dialogue list
	private List<string> dialogue0 = new List<string>();
	private Dictionary<int, List<string>> dialogueResponses0 = new Dictionary<int, List<string>>();

	private List<string> dialogue1 = new List<string>();
	private Dictionary<int, List<string>> dialogueResponses1 = new Dictionary<int, List<string>>();

	private int dialogueCount = 2;

	[SerializeField]
	private GameObject npcPrefab;

    [Header("Npcs")]
    [SerializeField]
    private List<GameObject> NPCs = new List<GameObject>();

    private GameManager gameManager;


	public IEnumerator Initialize()
	{
		gameManager = this.GetComponent<GameManager>();

		// Add new dialogue here
		List<string> responses = new List<string>();

		// Dialogue0
		// Line0
		dialogue0.Add ("Hey, how's it going?"); // dialogue
		responses = new List<string>(); //responses
		responses.Add ("Great");
		responses.Add ("Alright");
		responses.Add ("Bad");
		dialogueResponses0.Add (0, responses); //dicitionary for responses
		// Line1
		dialogue0.Add ("What's your favourite colour?");
		responses = new List<string>();
		responses.Add ("Red");
		responses.Add ("Green");
		responses.Add ("Blue");
		dialogueResponses0.Add (1, responses);
		// Line2
		dialogue0.Add ("You have nice hair!");
		responses = new List<string>();
		responses.Add ("Nah b");
		responses.Add ("Thanks!");
		responses.Add ("Nah, you have nice hair.");
		dialogueResponses0.Add (2, responses);

		responses = new List<string>();

		// Dialogue1
		// Line0
		dialogue1.Add ("Test1"); // dialogue
		responses = new List<string>(); //responses
		responses.Add ("Great");
		responses.Add ("Alright");
		responses.Add ("Bad");
		dialogueResponses1.Add (0, responses); //dicitionary for responses
		// Line1
		dialogue1.Add ("Test2");
		responses = new List<string>();
		responses.Add ("Red");
		responses.Add ("Green");
		responses.Add ("Blue");
		dialogueResponses1.Add (1, responses);
		// Line2
		dialogue1.Add ("Test3");
		responses = new List<string>();
		responses.Add ("Nah b");
		responses.Add ("Thanks!");
		responses.Add ("Nah, you have nice hair.");
		dialogueResponses1.Add (2, responses);


		this.isInitialized = true;
		yield return null;
	}

	public bool GetIsInitiliazed()
	{
		return this.isInitialized;
	}

	// Called from GameManager x amount of times
	public void CreateNpc()
	{


		// Instantiate & set name
		GameObject BaseNpc = GameObject.Instantiate(npcPrefab, this.transform.position, Quaternion.identity);
        GameObject npc = NPCs[Random.Range(0, NPCs.Count)];

        BaseNpc.name = npc.name;

		// Set dialogue
		List<string> chosenDialogue = new List<string>();
		Dictionary<int, List<string>> chosenDialogueResponses = new Dictionary<int, List<string>>();
		switch(Random.Range(0, dialogueCount))
		{
		case 0:
			chosenDialogue = dialogue0;
			chosenDialogueResponses = dialogueResponses0;
			break;

		case 1:
			chosenDialogue = dialogue1;
			chosenDialogueResponses = dialogueResponses1;
			break;

		default:
			break;
		}

		// Initialize
		NpcClass npcClass = BaseNpc.GetComponent<NpcClass> ();
        npcClass.Initialize(BaseNpc.name, npc.GetComponent<SpriteRenderer>().sprite, npc.GetComponent<Animator>(), chosenDialogue, chosenDialogueResponses);

        this.gameManager.AddActiveNpc(npcClass);
	}
}
