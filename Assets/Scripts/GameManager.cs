using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	NpcGenerator npcGenerator;

	[Header("NPC Stuff")]
	[SerializeField]
	private int npcCount = 10;

	[Header("Point Stuff")]
	[SerializeField]
	public static int goodPoints = 100;
	[SerializeField]
	public static int badPoints = 0;
	[SerializeField]
	public static float goodPercentage = 37.5f;

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
		// Initialize Npc Generator
		this.npcGenerator.StartCoroutine (this.npcGenerator.Initialize());
		yield return new WaitUntil (() => this.npcGenerator.GetIsInitiliazed());


		// Create and instantiate npcs
		for (int i = 0; i < npcCount; i++)
		{
			this.npcGenerator.CreateNpc ();
		}

		yield return null;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.B))
		{
			anim.GetComponent<Animator>().SetBool("isIdle", !anim.GetComponent<Animator>().GetBool(0));
		}
	}

}
