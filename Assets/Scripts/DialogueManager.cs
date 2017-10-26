using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	[Header("UI Objects")]
	[SerializeField]
	private Text dialogueText;
	[SerializeField]
	private List<Text> responseTexts;
	[SerializeField]
	private List<RectTransform> responseTextParents;
	[SerializeField]
	private GameObject responseTextsContainer;

	[Header("Positioning & Scale")]
	[SerializeField]
	private Vector3 leftPosition;
	[SerializeField]
	private Vector3 centerPosition;
	[SerializeField]
	private Vector3 rightPosition;
	[SerializeField]
	private float centerScale;
	[SerializeField]
	private float backgroundScale;
	[SerializeField]
	private float buttonMoveSpeed = 5.0f;
	[SerializeField]
	private float buttonScaleSpeed = 5.0f;


	[Header("YEAAAAAAAAHHHH")]
	[SerializeField]
	private GameManager gameManager;

	private int currentLineIndex = 0;
	private int currentResponseIndex = 0;
	private bool isWaitingForResponse = true;

	private IEnumerator Initialize()
	{
		this.currentLineIndex = 0;

		yield return null;
	}

	// Controls the flow of the conversation
	public IEnumerator Converse(NpcClass npc)
	{
		Debug.Log("line index: " + this.currentLineIndex);

		// Npc Speaks
		yield return this.StartCoroutine(this.SayLine(npc));

		// Update responses
		yield return this.StartCoroutine(this.UpdateResponseText(npc));

		// Wait for player input & immediately reset bool for the next question
		yield return new WaitUntil(() => this.isWaitingForResponse == false);
		this.isWaitingForResponse = true;

		// Pick a random corerct answer
		int correctAnswer = Random.Range(0, npc.GetDialogueResponses(this.currentLineIndex).Count);

		// Update player phunk meter if chosen answer matches correct answer
		if(this.currentResponseIndex == correctAnswer)
		{
			this.gameManager.IncreasePlayerPhunk(GameManager.answerPoints);
			Debug.Log("Correct Answer");
		}

		// Increase counter
		this.currentLineIndex++;

		// Call converse if there are more dialogue lines
		if(this.currentLineIndex < npc.GetDialogueCount())
		{
			this.StartCoroutine(this.Converse(npc));
		}
		else
		{
			this.EnableDialogueUI(false);
		}

		yield return null;
	}

	// Get NPC Line and update UI dialogue text
	private IEnumerator SayLine(NpcClass npc)
	{
		this.dialogueText.text = npc.GetDialogue(currentLineIndex);
		yield return null;
	}

	// Get possible responses and update response text objects
	private IEnumerator UpdateResponseText(NpcClass npc)
	{
		List<string> responses = npc.GetDialogueResponses(this.currentLineIndex);

		for(int i = 0; i < responses.Count; i++)
		{
			this.responseTexts[i].text = responses[i];
		}

		yield return null;
	}

	// Call from Button? when an answer is selected.... YES.
	public void Respond(int responseIndex)
	{
		this.currentResponseIndex = responseIndex;
		this.isWaitingForResponse = false;
	}

	// Enable/Disable dialogue UI
	public void EnableDialogueUI(bool isEnabled)
	{
		this.dialogueText.gameObject.SetActive(isEnabled);
		this.responseTextsContainer.SetActive(isEnabled);
	}

	private void Update()
	{
		if(AudioManager.isOnHalfBeat && GameManager.isInDialogue)
		{
			this.StartCoroutine(this.CycleResponseButtons());
		}
	}

	// Start OnBeat
	public IEnumerator CycleResponseButtons()
	{
		for(int i = 0; i < responseTextParents.Count; i++)
		{
			// Move left response to the right
			if(responseTextParents[i].localPosition.x < 0.0f)
			{
				this.StartCoroutine(this.MoveReponseButton(this.responseTextParents[i], this.rightPosition));
			}
			// Move right response to the center, scale up, set interactable
			else if (responseTextParents[i].localPosition.x > 0.0f)
			{
				this.StartCoroutine(this.MoveReponseButton(this.responseTextParents[i], this.centerPosition));
				this.StartCoroutine(this.ScaleResponseButton(this.responseTextParents[i], this.centerScale, true));
				this.responseTextParents[i].GetComponent<Button>().interactable = true;
			}
			// Move center response to the left, scale down, set not interactable
			else
			{
				this.StartCoroutine(this.MoveReponseButton(this.responseTextParents[i], this.leftPosition));
				this.StartCoroutine(this.ScaleResponseButton(this.responseTextParents[i], this.backgroundScale, false));
				this.responseTextParents[i].GetComponent<Button>().interactable = false;
			}
		}

		yield return null;
	}

	// Move button towards the desired position
	private IEnumerator MoveReponseButton(RectTransform rectTransform, Vector3 desiredPosition)
	{
		while(Vector3.Distance(rectTransform.localPosition, desiredPosition) >= 0.01f)
		{
			rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, desiredPosition, this.buttonMoveSpeed * Time.deltaTime);
			yield return null;
		}
			
		yield return null;
	}

	// Scale button to desired scale
	private IEnumerator ScaleResponseButton(RectTransform rectTransform, float desiredScale, bool isScalingUp)
	{
		float tempScale = rectTransform.localScale.x;

		if(isScalingUp)
		{
			while(tempScale < desiredScale)
			{
				tempScale = Mathf.MoveTowards(tempScale, desiredScale, this.buttonScaleSpeed * Time.deltaTime);
				rectTransform.localScale = new Vector3(tempScale, tempScale, 1.0f);
				yield return null;
			}
		}
		else
		{
			while(tempScale > desiredScale)
			{
				tempScale = Mathf.MoveTowards(tempScale, desiredScale, this.buttonScaleSpeed * Time.deltaTime);
				rectTransform.localScale = new Vector3(tempScale, tempScale, 1.0f);
				yield return null;
			}
		}

		yield return null;
	}
}
