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

    private bool CR_running = false;

    private Vector3 m_priorLocation;

    void Update()
    {
    	if(this.movementCounter >= this.inputTimeInterval && !GameManager.isInDialogue)
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
       

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !CR_running)
        {
            m_priorLocation = transform.position;

            Vector3 relativeLocation = new Vector3(0f, 0.0f, m_moveDist); // changed to work on x,z plane
            Vector3 targetLocation = transform.position + relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));

            RayCastCheck(transform.forward);

        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !CR_running)
        {
            m_priorLocation = transform.position;

            Vector3 relativeLocation = new Vector3(0f, 0.0f, m_moveDist); // changed to work on x,z plane
            Vector3 targetLocation = transform.position - relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));

            RayCastCheck(-transform.forward);

        }
		else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !CR_running)
        {
            m_priorLocation = transform.position;

            Vector3 relativeLocation = new Vector3(m_moveDist, 0f, 0f);
            Vector3 targetLocation = transform.position + relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));

            RayCastCheck(transform.right);

        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !CR_running)
        {
            m_priorLocation = transform.position;

            Vector3 relativeLocation = new Vector3(m_moveDist, 0f, 0f);
            Vector3 targetLocation = transform.position - relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));

            RayCastCheck(-transform.right);

        }
    }

    IEnumerator SmoothMove(Vector3 target, float delta)
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
                StopAllCoroutines();
                StartCoroutine(SmoothMove(m_priorLocation, m_timeDelta));
            }
            else if(hit.transform.tag == "NPC")
            {
                StopAllCoroutines();
                StartCoroutine(SmoothMove(m_priorLocation, m_timeDelta));
                StartCoroutine(gameManager.RunDialogueSequence());
            }
        }
    }

}
