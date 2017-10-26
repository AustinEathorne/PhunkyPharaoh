using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour 
{
	[Header("Audio Sources")]
	[SerializeField]
	private AudioSource mainSource;
	[SerializeField]
	private AudioSource subSource;
	[SerializeField]
	private AudioSource sfxSource;

	[Header("Audio Clips")]
	[SerializeField]
	private int selectedSongIndex = 0;
	[SerializeField]
	private List<AudioClip> levelAudioClips;
	[SerializeField]
	private List<AudioClip> danceAudioClips;
	[SerializeField]
	private List<AudioClip> sfxAudioClips;

	[Header("Audio BPM")]
	[SerializeField]
	private List<int> levelAudioBpm;
	[SerializeField]
	private List<int> danceAudioBpm;

	// Static beat checks
	public static bool isOnBeat = false;
	public static bool isOnHalfBeat = false;
	public static bool isOnQuarterBeat = false;
	public static bool isOnEigthBeat = false;

	// beat counts
	public static float timeBetweenBeats = 0.0f;
	private float currentTimeBetweenBeats = 0.0f;
	private float nextTimeBetweenBeats = 0.0f;
	private float elapsedTime = 0.0f;
	private int beatCounter = 0;

	private int previousBeatCount = 0;
	private int previousHalfBeatCount = 0;
	private int previousQuarterBeatCount = 0;
	private int previousEighthBeatCount = 0;

	private float goodHitSeconds = 0.15f; // seconds before and after a beat that a hit is still good, set in initialize

	// Fade Speeds (Serialized)
	[Header("Fade Speed")]
	[SerializeField]
	private float fadeSpeed = 5.0f;

	// Bools
	public static bool isPaused = false;
	public static bool isInitialized = false;
	public static bool hasStarted = false; // for elapsed time counter

	[Header("Test")]
	[SerializeField]
	private Text hitText;

	//Get audio start delay from manager, needed? just call startaudio from manager when the game is ready?

	// Yep
	public IEnumerator Start()
	{
		yield return this.StartCoroutine(this.InitializeAudioManager());
		isInitialized = true;
		this.StartCoroutine (this.StartAudio(1.0f));
	}

	// Initialize Everyting
	public  IEnumerator InitializeAudioManager()
	{
		timeBetweenBeats = 60.0f/levelAudioBpm[selectedSongIndex];
		this.goodHitSeconds = timeBetweenBeats * (GameManager.goodPercentage / 100.0f);

		Debug.Log ("Time Between Beats: " + timeBetweenBeats.ToString());

		yield return null;
	}

	// Call to start new audio
	public  IEnumerator StartAudio(float startDelay)
	{
		yield return new WaitUntil (() => isInitialized == true);

		this.beatCounter = 0;

		this.mainSource.PlayScheduled (AudioSettings.dspTime + startDelay);

		InvokeRepeating("BeatCounter", startDelay, timeBetweenBeats);

		yield return new WaitForSeconds (startDelay - timeBetweenBeats);

		hasStarted = true;

		yield return null;
	}

	// Play/Pause the given audio source
	public void PlayPauseAudio(AudioSource audioSource)
	{
		if (isPaused) 
		{
			audioSource.UnPause ();
			isPaused = false;
		} 
		else 
		{
			audioSource.Pause ();
			isPaused = true;
		}
	}

	// stop the audio source from playing
	public  IEnumerator StopAudio(AudioSource audioSource)
	{
		audioSource.Stop();	
		CancelInvoke("BeatCounter");

		yield return null;
	}
		
	// Count dem beatz
	private void Update()
	{
		// Input Test
		if(Input.GetKeyDown(KeyCode.Space))
		{
			float temp = GetPointValue();
		}

		if(!isPaused && hasStarted)
			this.elapsedTime += Time.deltaTime;

		// Debug.Log ("Elapsed Time: " + this.elapsedTime.ToString());

		isOnBeat = this.GetBeat ();
		isOnHalfBeat = this.GetHalfBeat ();
		isOnQuarterBeat = this.GetQuarterBeat ();
		isOnEigthBeat = this.GetEighthBeat ();
	}

	private void BeatCounter()
	{
		if (!isPaused)
		{
			this.beatCounter += 1;

			this.currentTimeBetweenBeats += timeBetweenBeats;
			this.nextTimeBetweenBeats = this.currentTimeBetweenBeats + timeBetweenBeats;

			// Debug.Log ("current total time between beats: " + this.currentTimeBetweenBeats.ToString());
			// Debug.Log ("next total time between beats: " + this.nextTimeBetweenBeats.ToString());
		}
	}

	private bool GetBeat()
	{
		if(this.beatCounter > previousBeatCount)
		{
			previousBeatCount = beatCounter;
			return true;
		}
		return false;
	}

	private bool GetHalfBeat()
	{
		if(this.beatCounter > previousHalfBeatCount)
		{
			previousHalfBeatCount = beatCounter + 1;
			return true;
		}
		return false;
	}

	private bool GetQuarterBeat()
	{
		if(this.beatCounter > previousQuarterBeatCount)
		{
			previousQuarterBeatCount = beatCounter + 3;
			return true;
		}
		return false;
	}

	private bool GetEighthBeat()
	{
		if(this.beatCounter > previousEighthBeatCount)
		{
			previousEighthBeatCount = beatCounter + 7;
			return true;
		}
		return false;
	}

	public float GetCurrentTimeBetweenBeats()
	{
		return this.currentTimeBetweenBeats;
	}

	public float GetNextTimeBetweenBeats()
	{
		return this.nextTimeBetweenBeats;
	}

	public int GetPointValue()
	{
		if (this.elapsedTime <= this.currentTimeBetweenBeats + goodHitSeconds || this.elapsedTime >= this.nextTimeBetweenBeats - goodHitSeconds)
		{
			Debug.Log("Good Hit");
			//hitText.text = "Good";
			Debug.Log("Elapsed Time: " + this.elapsedTime.ToString());
			return GameManager.goodPoints;
		}
		else
		{
			Debug.Log("Bad Hit");
			//hitText.text = "Bad";
			Debug.Log("Elapsed Time: " + this.elapsedTime.ToString());
			return GameManager.badPoints;
		}
	}

	// Fade the given audio source's audio clip in/out
	public IEnumerator FadeAudio(AudioSource audioSource, bool isFadingIn)
	{
		if(isFadingIn)
		{
			//fade dat music in for the given audioSource
		}
		else
		{
			//fade dat music out for the given audioSource
		}

		yield return null;
	}
}
