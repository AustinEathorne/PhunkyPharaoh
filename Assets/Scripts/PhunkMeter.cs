using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhunkMeter : MonoBehaviour {

   // [SerializeField] private GameManager m_gameManagerScript;

    [Header("Objects")]
	[SerializeField]
	private Slider slider;

	[Header("Values")]
	[SerializeField]
	private float maxPhunkValue;
	[SerializeField]
	private float phunkPerFrameDecrease;

	private float currentPhunkValue = 0;

	void Start () 
	{
		this.slider.minValue = 0.0f;
		this.slider.maxValue = this.maxPhunkValue;
	}

	void Update ()
	{
        if (!GameManager.isInDialogue)
        {
            this.UpdatePhunkMeter();
            this.DecreasePhunkValue(this.phunkPerFrameDecrease);
        }
	}

	public void DecreasePhunkValue(float value)
	{
		// Set phunk value to 0 if it would be less
		if(this.currentPhunkValue - value <= 0.0f)
		{
			this.currentPhunkValue = 0.0f;
		}
		else
		{
			this.currentPhunkValue -= value;
		}
	}

	public void IncreasePhunkValue(float value)
	{
		if(this.currentPhunkValue + value >= this.maxPhunkValue)
		{
			this.currentPhunkValue = this.maxPhunkValue;
		}
		else
		{
			this.currentPhunkValue += value;
		}
	}

	private void UpdatePhunkMeter()
	{
		this.slider.value = this.currentPhunkValue;
		// Debug.Log("Current Phunk: " + this.currentPhunkValue.ToString());
	}
}

