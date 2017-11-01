using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	NpcGenerator npcGenerator;

	[Header("NPC Stuff")]
	[SerializeField]
	private int npcCount = 10;
	[SerializeField]
	private List<Vector3> npcSpawnPositions;
	private List<NpcClass> activeNpcs;

	[Header("Point Stuff")]
	[SerializeField]
	private PhunkMeter phunkMeter;
	[SerializeField]
	public static int answerPoints = 20;
	[SerializeField]
	public static int goodPoints = 100;
	[SerializeField]
	public static int badPoints = 0;
	[SerializeField]
	public static float goodPercentage = 37.5f;

	[Header("Dialogue")]
	[SerializeField]
	private DialogueManager dialogueManager;
	public static bool isInDialogue = false;

	[Header("Test")]
	[SerializeField]
	private GameObject anim;
	private bool isIdle = true;

	void Awake()
	{
		npcGenerator = this.GetComponent<NpcGenerator> ();
		this.StartCoroutine (this.SetUpGame());
	}

	private IEnumerator SetUpGame()
	{
		this.activeNpcs = new List<NpcClass>();

		// Initialize Npc Generator
		this.npcGenerator.StartCoroutine (this.npcGenerator.Initialize());
		yield return new WaitUntil (() => this.npcGenerator.GetIsInitiliazed());

		// Create and instantiate npcs
		for (int i = 0; i < npcCount; i++)
		{
			this.npcGenerator.CreateNpc ();
		}

		// Position NPCs
		for(int i = 0; i < npcCount; i++)
		{
			this.activeNpcs[i].transform.position = npcSpawnPositions[i];
		}

		yield return null;
	}

	public void AddActiveNpc(NpcClass npc)
	{
		this.activeNpcs.Add(npc);
	}

	private void Update()
	{
		// Testing
		if(Input.GetKeyDown(KeyCode.B))
		{
			anim.GetComponent<Animator>().SetBool("isIdle", !anim.GetComponent<Animator>().GetBool(0));
		}
		if(Input.GetKeyDown(KeyCode.N))
		{
			this.StartCoroutine(this.RunDialogueSequence());
		}
		if(Input.GetKeyDown(KeyCode.M))
		{
			this.IncreasePlayerPhunk(50.0f);
		}
	}

	private IEnumerator RunDialogueSequence()
	{
		this.dialogueManager.ResetConversation();
		isInDialogue = true;
		this.dialogueManager.EnableDialogueUI(true);
		yield return this.dialogueManager.StartCoroutine(this.dialogueManager.Converse(this.activeNpcs[0]));
	}

	public void IncreasePlayerPhunk(float phunkValue)
	{
		Debug.Log("PHUNK UP " + phunkValue.ToString());
		this.phunkMeter.IncreasePhunkValue(phunkValue);
	}
}
