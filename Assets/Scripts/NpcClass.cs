using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcClass : MonoBehaviour 
{
	private string m_name = "";

	[SerializeField]
	private List<string> m_dialogueLines = new List<string>();
	private Dictionary<int, List<string>> m_dialogueOptions = new Dictionary<int, List<string>>();

	// Initialized by NpcGenerator
	public void Initialize(string name, Sprite sprite, List<string> dialogueLines, Dictionary<int, List<string>> dialogueOptions)
	{
		this.m_name = name;
		this.m_dialogueLines = dialogueLines;
		this.m_dialogueOptions = dialogueOptions;

		this.GetComponent<SpriteRenderer> ().sprite = sprite;
	}

	// Return the desired dialogue line
	public string GetDialogue(int lineIndex)
	{
		return this.m_dialogueLines[lineIndex];
	}

	// Return the desired list of possible responses
	public List<string> GetDialogueResponses(int dialogueIndex)
	{
		return this.m_dialogueOptions[dialogueIndex];
	}

	public int GetDialogueCount()
	{
		return this.m_dialogueLines.Count;
	}
}
