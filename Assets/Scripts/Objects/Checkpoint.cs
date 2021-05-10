using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {

	private GameController gameController;
	public AudioClip levelComplete;
	private AudioSource soundEffectsPlayer;
	private AudioController audioController;
	private AudioSource musicPlayer;
	private int nextGameLevel;

	// Use this for initialization
	void Awake () 
	{
		gameController = FindObjectOfType<ReferenceManager>().gameController;
		soundEffectsPlayer = FindObjectOfType<ReferenceManager>().soundEffectPlayer;
		audioController = soundEffectsPlayer.GetComponent<AudioController>();
		musicPlayer = FindObjectOfType<ReferenceManager>().musicPlayer;

		if (SceneManager.GetActiveScene().name.Equals("Level1")) nextGameLevel = 2;
		else if (SceneManager.GetActiveScene().name.Equals("Level2")) nextGameLevel = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "Player")
		{
			target.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			target.GetComponent<Player>().isMovementLocked = true;
			StartCoroutine(nextLevel(target.gameObject));
		}
	}

	private IEnumerator nextLevel(GameObject target)
	{
		Player player = target.GetComponent<Player>();
		player.playerIO.CurrentLevel = nextGameLevel;
		FileIO.savePlayer(player.playerIO);

		musicPlayer.Stop();
		audioController.playOnce(levelComplete);
		yield return new WaitForSeconds(soundEffectsPlayer.clip.length);
		if (player.playerIO.CurrentLevel == 2) SceneManager.LoadScene("level2");
		else if (player.playerIO.CurrentLevel == 0) SceneManager.LoadScene("Menu");
	}
}
