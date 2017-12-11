using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcClass : MonoBehaviour 
{
	private string m_name = "";

    private bool CR_running = false;
    [HideInInspector] public bool m_playerHitWall = false;

    private float m_timeDelta = 0.15f;

    public int m_correctDialogueCount;

	[SerializeField]
	private List<string> m_dialogueLines = new List<string>();
	private Dictionary<int, List<string>> m_dialogueOptions = new Dictionary<int, List<string>>();

    private GameObject m_playerObj;
    private CharacterMovement m_player;

    private void Awake()
    {
        m_playerObj = GameObject.Find("Char_ProtoType");
        m_player = m_playerObj.GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if(m_correctDialogueCount >= 3 && !m_playerHitWall)
        {
            StartCoroutine(SmoothMove(m_player.m_priorLocation, m_timeDelta));
            m_player.m_npcFollowing = true;
        }
    }

    // Initialized by NpcGenerator
    public void Initialize(string name, Sprite sprite, Animator animator, List<string> dialogueLines, Dictionary<int, List<string>> dialogueOptions)
	{
		this.m_name = name;
		this.m_dialogueLines = dialogueLines;
		this.m_dialogueOptions = dialogueOptions;

		this.GetComponent<SpriteRenderer> ().sprite = sprite;
        this.GetComponent<Animator>().runtimeAnimatorController = animator.runtimeAnimatorController;
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

    IEnumerator SmoothMove(Vector3 target, float delta)
    {
        CR_running = true;

        float closeEnough = 0.2f;
        float distance = (transform.position - target).magnitude;

        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while (distance >= closeEnough)
        {
            transform.position = Vector3.Lerp(transform.position, target, delta);
            yield return wait;

            distance = (transform.position - target).magnitude;
        }

        transform.position = target;

        CR_running = false;
    }
}
