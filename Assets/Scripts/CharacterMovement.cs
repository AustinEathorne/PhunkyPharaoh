using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	[Header("Scene reference")]
	[SerializeField]
	private GameManager gameManager;

	[SerializeField]
	private float inputTimeInterval = 1.0f;
	private float movementCounter = 0.0f;
    private float m_moveDist = 0.32f;
    private float m_timeDelta = 0.15f;
    private bool m_isAxisInUse = false;


    private bool CR_running = false;

public bool m_npcFollowing = false;

    [HideInInspector] public Vector3 m_priorLocation;

    [HideInInspector] public GameObject m_hitNPC = null;

    void Update()
    {
    	if(/*this.movementCounter >= this.inputTimeInterval &&*/ !GameManager.isInDialogue)
    	{
			this.GetInput();
    	}

		this.MovementCounter();
    }

    private void MovementCounter()
    {
    	this.movementCounter += Time.deltaTime;
    }

    private void GetInput()
    {
       

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)|| Input.GetAxisRaw("Vertical") > 0f) && !CR_running)
        {
            if (m_isAxisInUse == false)
            {
                m_priorLocation = transform.position;

                Vector3 relativeLocation = new Vector3(0f, 0.0f, m_moveDist); // changed to work on x,z plane
                Vector3 targetLocation = transform.position + relativeLocation;
                float timeDelta = 0.15f;

                StartCoroutine(SmoothMove(targetLocation, timeDelta));

                RayCastCheck(transform.forward);
                m_isAxisInUse = true;
            }

        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetAxisRaw("Vertical") < 0f) && !CR_running)
        {
            if (m_isAxisInUse == false)
            {
                m_priorLocation = transform.position;

                Vector3 relativeLocation = new Vector3(0f, 0.0f, m_moveDist); // changed to work on x,z plane
                Vector3 targetLocation = transform.position - relativeLocation;
                float timeDelta = 0.15f;

                StartCoroutine(SmoothMove(targetLocation, timeDelta));

                RayCastCheck(-transform.forward);
                m_isAxisInUse = true;
            }

        }
		else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetAxisRaw("Horizontal") > 0f) && !CR_running)
        {
            if (m_isAxisInUse == false)
            {
                m_priorLocation = transform.position;

                Vector3 relativeLocation = new Vector3(m_moveDist, 0f, 0f);
                Vector3 targetLocation = transform.position + relativeLocation;
                float timeDelta = 0.15f;

                StartCoroutine(SmoothMove(targetLocation, timeDelta));

                RayCastCheck(transform.right);
                m_isAxisInUse = true;
            }

        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetAxisRaw("Horizontal") < 0f) && !CR_running)
        {
            if (m_isAxisInUse == false)
            {
                m_priorLocation = transform.position;

                Vector3 relativeLocation = new Vector3(m_moveDist, 0f, 0f);
                Vector3 targetLocation = transform.position - relativeLocation;
                float timeDelta = 0.15f;

                StartCoroutine(SmoothMove(targetLocation, timeDelta));

                RayCastCheck(-transform.right);
                m_isAxisInUse = true;
            }

        }

        if (Input.GetAxisRaw("Vertical") == 0f && Input.GetAxisRaw("Horizontal") == 0f)
        {
            m_isAxisInUse = false;
        }

    }

    public IEnumerator SmoothMove(Vector3 target, float delta)
    {
    	// check if on beat
    	this.gameManager.OnBeatInput();
		Debug.Log(this.inputTimeInterval.ToString());
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

		// Reset movement counter
    	this.movementCounter = 0.0f;

        CR_running = false;
    }
   
    // Get/Set
    public void SetInputTimeInterval(float value)
    {
    	this.inputTimeInterval = value;
    	Debug.Log(this.inputTimeInterval.ToString());
    }

    private void RayCastCheck(Vector3 rayDirection)
    {
        Ray ray = new Ray(transform.position, rayDirection);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_moveDist))
        {
            if (hit.transform.tag == "Wall")
            {
                if(m_hitNPC != null)
                {
                    m_hitNPC.GetComponent<NpcClass>().m_playerHitWall = true;
                }
                StopAllCoroutines();
                StartCoroutine(SmoothMove(m_priorLocation, m_timeDelta));
            }
            else if(hit.transform.tag == "NPC" && !m_npcFollowing)
            {
                m_hitNPC = hit.transform.gameObject;
                StopAllCoroutines();
                StartCoroutine(SmoothMove(m_priorLocation, m_timeDelta));
                m_priorLocation = hit.transform.position;
                StartCoroutine(gameManager.RunDialogueSequence(hit.transform.GetComponent<NpcClass>()));
            }
            
        }
        else
        {
            if (m_hitNPC != null)
            {
                m_hitNPC.GetComponent<NpcClass>().m_playerHitWall = false;
            }
        }

    }

}
