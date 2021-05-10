using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	public AudioSource soundEffectsPlayer;

	// Use this for initialization
	void Start () {
		soundEffectsPlayer = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Plays the sound effect once before returning to previous settings
	public void playRecurring(AudioClip newClip)
	{
		//Play new clip
		soundEffectsPlayer.clip = newClip;
		soundEffectsPlayer.loop = true;
		soundEffectsPlayer.Play();
	}

	//Plays the sound effect once before returning to previous settings
	public void playOnce(AudioClip newClip)
	{
		//Get current state of sound effects player and save for recall later
		AudioClip oldClip = null;
		bool isLooping = false;
		if (soundEffectsPlayer.isPlaying) oldClip = soundEffectsPlayer.clip;
		if (soundEffectsPlayer.loop) isLooping = true;

		//Play new clip
		soundEffectsPlayer.clip = newClip;
		soundEffectsPlayer.loop = false;
		soundEffectsPlayer.Play();

		//Reset things as needed
		if (oldClip != null && isLooping) StartCoroutine(ResetSound(oldClip, isLooping, soundEffectsPlayer.clip.length));
	}

	//Stops the Sound Effects if the provided effect is the one playing.
	public bool stopClip(AudioClip stoppingClip)
	{
		if (soundEffectsPlayer.clip == stoppingClip)
		{
			soundEffectsPlayer.Stop();
			return true;
		}
		else return false;
		
	}

	//Resets the Player to the old clip provided.
	IEnumerator ResetSound(AudioClip oldClip, bool isLooping, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		soundEffectsPlayer.loop = isLooping;
		soundEffectsPlayer.clip = oldClip;
		soundEffectsPlayer.Play();
	}
}
