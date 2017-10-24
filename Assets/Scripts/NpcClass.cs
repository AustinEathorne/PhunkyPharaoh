using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcClass : MonoBehaviour 
{
	private string m_name = "";
	private Sprite m_sprite = null;
	private List<string> m_dialogueLines = new List<string>();
	private Dictionary<int, List<string>> m_dialogueOptions = new Dictionary<int, List<string>>();

	// Initialized by NpcGenerator
	public void InitializeNpc(string name, Sprite sprite, List<string> dialogueLines, Dictionary<int, List<string>> dialogueOptions)
	{
		this.m_name = name;
		this.m_sprite = sprite;
		this.m_dialogueLines = dialogueLines;
		this.m_dialogueOptions = dialogueOptions;
	}

	// Return the desired dialogue line
	private string GetDialogue(int lineIndex)
	{
		return this.m_dialogueLines[lineIndex];
	}

	// Return the desired list of possible responses
	private List<string> GetDialogueResponses(int dialogueIndex)
	{
		return this.m_dialogueOptions[dialogueIndex];
	}
}
