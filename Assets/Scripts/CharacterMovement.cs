using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private bool CR_running = false;


    // Update is called once per frame
    void Update()
    {
		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !CR_running)
        {
            Vector3 relativeLocation = new Vector3(0f, 0.0f, 0.32f); // changed to work on x,z plane
            Vector3 targetLocation = transform.position + relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));
        }
		else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !CR_running)
        {
			Vector3 relativeLocation = new Vector3(0f, 0.0f, 0.32f); // changed to work on x,z plane
            Vector3 targetLocation = transform.position - relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));
        }
		else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !CR_running)
        {
            Vector3 relativeLocation = new Vector3(0.32f, 0f, 0f);
            Vector3 targetLocation = transform.position + relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));
        }
		else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !CR_running)
        {
            Vector3 relativeLocation = new Vector3(0.32f, 0f, 0f);
            Vector3 targetLocation = transform.position - relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));
        }
    }

    IEnumerator SmoothMove(Vector3 target, float delta)
    {
        CR_running = true;
        float closeEnough = 0.2f;
        float distance = (transform.position - target).magnitude;

        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while (distance >= closeEnough)
        {
            Debug.Log("Executing Movement");

            transform.position = Vector3.Lerp(transform.position, target, delta);
            yield return wait;

            distance = (transform.position - target).magnitude;
        }

        transform.position = target;

        CR_running = false;
        Debug.Log("Movement Complete");
    }
}
