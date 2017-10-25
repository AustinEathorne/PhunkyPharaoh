using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {



	[Header("Objs")]
	[SerializeField]
	private RectTransform centerObj;
	[SerializeField]
	private List<RectTransform> dotObjs;

	[Header("Center Variables")]
	[SerializeField]
	private float centerScaleMultiplier = 2.0f;
	[SerializeField]
	private float centerScaleSpeed = 3.0f;
	private float centerDesiredScale  = 0.0f;

	[Header("Dot Variables")]
	[SerializeField]
	private float dotSpacing = 120.0f;
	private float dotSpeed = 0.0f;
	private float resetPosition = 0.0f;

	//bools
	private bool isInitialized = false;

	//distance between dots
	//find speed from displacement and time

	private IEnumerator Start()
	{
		yield return new WaitUntil(() => AudioManager.isInitialized == true);
		yield return this.StartCoroutine(this.Initialize());
		this.isInitialized = true;

		yield return null;
	}

	private IEnumerator Initialize()
	{
		centerDesiredScale = centerObj.GetComponent<RectTransform>().localScale.x * centerScaleMultiplier;

		float xPosition = this.dotSpacing;
		Vector3 position = new Vector3(0.0f, dotObjs[0].localPosition.y, dotObjs[0].localPosition.z);
		for(int i = 0; i < dotObjs.Count; i++)
		{
			position.x += this.dotSpacing;
			this.dotObjs[i].localPosition = position;
		}

		this.dotSpeed = (this.dotSpacing - (dotObjs[0].sizeDelta.x * 2)) * AudioManager.timeBetweenBeats;
		Debug.Log("dotSpeed: " + this.dotSpeed);

		this.resetPosition = this.dotSpacing * dotObjs.Count;

		yield return null;
	}

	private void Update()
	{
		if(this.isInitialized && AudioManager.isOnBeat)
		{
			this.StartCoroutine(this.ScaleCenterObject());
		}
		if(this.isInitialized)
		{
			for(int i = 0; i < dotObjs.Count; i++)
			{
				dotObjs[i].localPosition = new Vector3(); //MAKE COROUNTINE AND USE MOVETOWARDS, USING DOTSPEED * TIME.DELTATIME, FUCK.
				if(dotObjs[i].localPosition.x <= 0.0f)
				{
					dotObjs[i].localPosition = new Vector3(this.resetPosition, dotObjs[i].localPosition.y, dotObjs[i].localPosition.z);
				}
			}
		}
	}


	private IEnumerator ScaleCenterObject()
	{
		//Debug.Log("start");

		Vector3 tempScale = centerObj.GetComponent<RectTransform>().localScale;
		Vector3 originalScale = centerObj.GetComponent<RectTransform>().localScale;
		Vector3 desiredScale = new Vector3(this.centerScaleMultiplier, this.centerScaleMultiplier, this.centerScaleMultiplier);

		while(tempScale.x < this.centerDesiredScale)
		{
			tempScale = Vector3.MoveTowards(tempScale, desiredScale, this.centerScaleSpeed * Time.deltaTime);
			centerObj.GetComponent<RectTransform>().localScale = tempScale;

			yield return null;
		}

		tempScale = desiredScale;

		while(tempScale.x > originalScale.x)
		{
			tempScale = Vector3.MoveTowards(tempScale, originalScale, (this.centerScaleSpeed * 0.25f) * Time.deltaTime);
			centerObj.GetComponent<RectTransform>().localScale = tempScale;

			yield return null;
		}

		//Debug.Log("finish");
	}
}
