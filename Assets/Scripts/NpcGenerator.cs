using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the list of possible sprites + dialogue options & their corresponding responses

public class NpcGenerator : MonoBehaviour 
{
	private bool isInitialized = false;

	// Create new list & dictionary for new dialogue, each list holds a set of lines for the npc to say, dictionary responses (List<string>) corresponds to the lineIndex for the dialogue list
	private List<string> dialogue0Baller = new List<string>();
	private Dictionary<int, List<string>> dialogueResponses0Baller = new Dictionary<int, List<string>>();

	private List<string> dialogue1Pimp = new List<string>();
	private Dictionary<int, List<string>> dialogueResponses1Pimp = new Dictionary<int, List<string>>();

    private List<string> dialogue2Skelly = new List<string>();
    private Dictionary<int, List<string>> dialogueResponses2Skelly = new Dictionary<int, List<string>>();

    private List<string> dialogue3Witch = new List<string>();
    private Dictionary<int, List<string>> dialogueResponses3Witch = new Dictionary<int, List<string>>();


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

        // Dialogue0Baller
        // Line0
        dialogue0Baller.Add ("Baller"); // dialogue
		responses = new List<string>(); //responses
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses0Baller.Add (0, responses); //dicitionary for responses
                                                     // Line1
        dialogue0Baller.Add ("What's your favourite colour?");
		responses = new List<string>();
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong"); ;
		dialogueResponses0Baller.Add (1, responses);
        // Line2
        dialogue0Baller.Add ("You have nice hair!");
		responses = new List<string>();
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses0Baller.Add (2, responses);

		responses = new List<string>();

        // Dialogue1Pimp
        // Line0
        dialogue1Pimp.Add ("Pimp"); // dialogue
		responses = new List<string>(); //responses
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses1Pimp.Add (0, responses); //dicitionary for responses
                                               // Line1
        dialogue1Pimp.Add ("Pimp");
		responses = new List<string>();
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses1Pimp.Add (1, responses);
        // Line2
        dialogue1Pimp.Add ("Test3");
		responses = new List<string>();
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses1Pimp.Add (2, responses);

        responses = new List<string>();

        // Dialogue2Skelly
        // Line0
        dialogue2Skelly.Add("Skelly"); // dialogue
        responses = new List<string>(); //responses
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses2Skelly.Add(0, responses); //dicitionary for responses
                                                  // Line1
        dialogue2Skelly.Add("Test2");
        responses = new List<string>();
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses2Skelly.Add(1, responses);
        // Line2
        dialogue2Skelly.Add("Test3");
        responses = new List<string>();
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses2Skelly.Add(2, responses);

        responses = new List<string>();

        // Dialogue3Witch
        // Line0
        dialogue3Witch.Add("Witch"); // dialogue
        responses = new List<string>(); //responses
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses3Witch.Add(0, responses); //dicitionary for responses
                                                  // Line1
        dialogue3Witch.Add("Test2");
        responses = new List<string>();
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses3Witch.Add(1, responses);
        // Line2
        dialogue3Witch.Add("Test3");
        responses = new List<string>();
        responses.Add("Right");
        responses.Add("Wrong");
        responses.Add("Wrong");
        dialogueResponses3Witch.Add(2, responses);


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
		switch(npc.GetComponent<NpcPrefabs>().m_npcIndex)
		{
		case 0:
			chosenDialogue = dialogue0Baller;
			chosenDialogueResponses = dialogueResponses0Baller;
			break;

		case 1:
			chosenDialogue = dialogue1Pimp;
			chosenDialogueResponses = dialogueResponses1Pimp;
			break;

        case 2:
            chosenDialogue = dialogue2Skelly;
            chosenDialogueResponses = dialogueResponses2Skelly;
            break;

        case 3:
            chosenDialogue = dialogue3Witch;
            chosenDialogueResponses = dialogueResponses3Witch;
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
