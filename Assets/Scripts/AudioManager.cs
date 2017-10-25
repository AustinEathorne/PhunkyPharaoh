using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private float timeBetweenBeats = 0.0f;
	private int beatCounter = 0;

	private int previousBeatCount = 0;
	private int previousHalfBeatCount = 0;
	private int previousQuarterBeatCount = 0;
	private int previousEigthBeatCount = 0;

	// Fade Speeds (Serialized)
	[Header("Fade Speed")]
	[SerializeField]
	private float fadeSpeed = 5.0f;

	// Bools
	private bool isPaused = false;
	private bool isInitialized = false;

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

		//yield return null;
	}

	// Call to start new audio
	public  IEnumerator StartAudio(float startDelay)
	{
		yield return new WaitUntil (() => this.isInitialized == true);

		this.beatCounter = 0;

		mainSource.PlayScheduled (AudioSettings.dspTime + startDelay);

		InvokeRepeating("BeatCounter", startDelay, timeBetweenBeats);

		yield return null;
	}

	// Play/Pause the given audio source
	public void PlayPauseAudio(AudioSource audioSource)
	{
		if (this.isPaused) 
		{
			audioSource.UnPause ();
			this.isPaused = false;
		} 
		else 
		{
			audioSource.Pause ();
			this.isPaused = true;
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
		isOnBeat = this.GetBeat ();
		isOnHalfBeat = this.GetHalfBeat ();
		isOnQuarterBeat = this.GetQuarterBeat ();
		isOnEigthBeat = this.GetEigthBeat ();
	}

	private void BeatCounter()
	{
		if(!this.isPaused)
			beatCounter += 1;
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

	private bool GetEigthBeat()
	{
		if(this.beatCounter > previousEigthBeatCount)
		{
			previousEigthBeatCount = beatCounter + 7;
			return true;
		}
		return false;
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
