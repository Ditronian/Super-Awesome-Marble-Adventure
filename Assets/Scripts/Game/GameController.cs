using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public AudioClip deathClip;
	public AudioSource musicPlayer;
	public bool isDead;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void executeDeath()
	{
		if(!isDead)
		{
			isDead = true;
			musicPlayer.clip = deathClip;
			musicPlayer.Play();
			StartCoroutine(deathTimer());
		}
	}

	private IEnumerator deathTimer()
	{
		yield return new WaitForSeconds(musicPlayer.clip.length);
		Scene thisScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(thisScene.name);
	}


}
