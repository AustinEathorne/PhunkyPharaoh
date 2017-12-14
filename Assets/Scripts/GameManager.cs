using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[Header("Player")]
	[SerializeField]
	private CharacterMovement characterController;

	[Header("NPC Stuff")]
	NpcGenerator npcGenerator;
	[SerializeField]
	private int npcCount = 10;
	[SerializeField]
	private List<GameObject> npcSpawnPositions;
	private List<NpcClass> activeNpcs;

	[Header("Point Stuff")]
	[SerializeField]
	private PhunkMeter phunkMeter;
	[SerializeField]
	private float onBeatMovePoints = 50.0f;
	[SerializeField]
	public static float answerPoints = 50.0f;
	[SerializeField]
	public static float goodPoints = 50.0f;
	[SerializeField]
	public static float badPoints = -25.0f;
	[SerializeField]
	public static float goodPercentage = 25.0f;

	[Header("Audio")]
	[SerializeField]
	private AudioManager audioManager;

	[Header("Dialogue")]
	[SerializeField]
	private DialogueManager dialogueManager;
	public static bool isInDialogue = false;

	[Header("Test")]
	[SerializeField]
	private GameObject anim;
	private bool isIdle = true;

    private bool m_isClubFloor1Active = true;
    private bool m_isClubFloor2Active = false;

    [HideInInspector] public bool m_isDanceGame = false;

    [SerializeField] private GameObject m_clubFloor1Obj;
    [SerializeField] private GameObject m_clubFloor2Obj;
    [SerializeField] private GameObject m_danceGameObj;
    [SerializeField] private GameObject m_danceFloorObj1;
    [SerializeField] private GameObject m_danceFloorObj2;

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
			this.activeNpcs[i].transform.position = npcSpawnPositions[i].transform.position;
		}

		this.characterController.SetInputTimeInterval(AudioManager.timeBetweenBeats * 0.5f);

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
            //this.StartCoroutine(this.RunDialogueSequence());
		}
		if(Input.GetKeyDown(KeyCode.M))
		{
			this.IncreasePlayerPhunk(50.0f);
		}

        if(AudioManager.isOnBeat && !m_isDanceGame)
        {
            if(m_isClubFloor1Active)
            {
                m_clubFloor1Obj.SetActive(false);
                m_danceFloorObj1.SetActive(false);
                m_clubFloor2Obj.SetActive(true);
                m_danceFloorObj2.SetActive(true);
                m_isClubFloor2Active = true;
                m_isClubFloor1Active = false;
            }
            else if(m_isClubFloor2Active)
            {
                m_clubFloor2Obj.SetActive(false);
                m_danceFloorObj2.SetActive(false);
                m_clubFloor1Obj.SetActive(true);
                m_danceFloorObj1.SetActive(true);
                m_isClubFloor1Active = true;
                m_isClubFloor2Active = false;
            }
        }
        if(m_isDanceGame)
        {
            m_danceFloorObj1.SetActive(false);
            m_danceFloorObj2.SetActive(false);
            m_danceGameObj.SetActive(true);
        }
    }

	public IEnumerator RunDialogueSequence(NpcClass npc)
	{
		this.dialogueManager.ResetConversation();
		isInDialogue = true;
		this.dialogueManager.EnableDialogueUI(true);
		yield return this.dialogueManager.StartCoroutine(this.dialogueManager.Converse(npc));
		yield return new WaitUntil(() => (this.dialogueManager.GetCurrentLineIndex() >= this.dialogueManager.GetCurrentLineCount()) == true);

		isInDialogue = false;
		this.dialogueManager.EnableDialogueUI(false);
		Debug.Log("Done Run Dialogue Sequence");
	}

	public void IncreasePlayerPhunk(float phunkValue)
	{
		// Debug.Log("PHUNK UP " + phunkValue.ToString());
		this.phunkMeter.IncreasePhunkValue(phunkValue);
	}

    public void DecreasePlayerPhunk(float phunkValue)
    {
        // Debug.Log("PHUNK UP " + phunkValue.ToString());
        this.phunkMeter.DecreasePhunkValue(phunkValue);
    }

    // For player movement onBeat check
    public void OnBeatInput()
	{
		this.IncreasePlayerPhunk(audioManager.GetPointValue());
	}
}
