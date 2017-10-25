using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private bool CR_running = false;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !CR_running)
        {
            Vector3 relativeLocation = new Vector3(0f, 0.32f, 0f);
            Vector3 targetLocation = transform.position + relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !CR_running)
        {
            Vector3 relativeLocation = new Vector3(0f, 0.32f, 0f);
            Vector3 targetLocation = transform.position - relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !CR_running)
        {
            Vector3 relativeLocation = new Vector3(0.32f, 0f, 0f);
            Vector3 targetLocation = transform.position + relativeLocation;
            float timeDelta = 0.15f;

            StartCoroutine(SmoothMove(targetLocation, timeDelta));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !CR_running)
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
