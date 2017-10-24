using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	NpcGenerator npcGenerator;

	[Header("Variables")]
	[SerializeField]
	private int npcCount = 10;

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

	void Update () 
	{
		
	}
}
