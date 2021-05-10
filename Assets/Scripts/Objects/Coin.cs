using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	private GameController gameController;
	public AudioClip acquiredCoin;
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


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D target)
	{
		if(target.gameObject.tag == "Player")
		{
			audioController.playOnce(acquiredCoin);
			gameTimer.remainingTime += 1;
			gameTimer.updateText();
			Destroy(gameObject);
		}
	}
}
