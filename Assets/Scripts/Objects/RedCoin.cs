using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCoin : MonoBehaviour 
{
	private GameController gameController;
	public AudioClip acquiredRedCoin;
	private AudioSource soundEffectsPlayer;
	private AudioController audioController;
	private GameTimer gameTimer;

	void Awake()
	{
		gameController = FindObjectOfType<ReferenceManager>().gameController;
		gameTimer = FindObjectOfType<ReferenceManager>().gameTimer;
		soundEffectsPlayer = FindObjectOfType<ReferenceManager>().soundEffectPlayer;
		audioController = soundEffectsPlayer.GetComponent<AudioController>();
	}


	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "Player")
		{
			audioController.playOnce(acquiredRedCoin);
			gameTimer.remainingTime += 5;
			gameTimer.updateText();
			Destroy(gameObject);
		}
	}
}
