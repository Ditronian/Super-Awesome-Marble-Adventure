using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	public int maxTime = 120;
	public int remainingTime;
	public Text timerText;
	public GameController gameController;
	public Player player;
	private AudioController audioController;
	public AudioSource soundEffectPlayer;
	public AudioClip lowTime;
	private bool warningSiren = false;

	// Use this for initialization
	void Start () 
	{
		audioController = soundEffectPlayer.GetComponent<AudioController>();
		remainingTime = maxTime;
		updateText();
		InvokeRepeating("decrementTimer", 1f, 1f);  //1s delay, repeat every 1s
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//Counts down the timer
	private void decrementTimer()
	{
		remainingTime -= 1;
		if (remainingTime > 10 && warningSiren)
		{
			//If lowTime was playing and has been stopped, remove flag.  Else wait until it has started playing again and remove it then.
			if (audioController.stopClip(lowTime)) warningSiren = false;
			if (timerText.color == Color.red) timerText.color = Color.black;
		}
		else if (remainingTime == 10)
		{
			timerText.color = Color.red;
			audioController.playRecurring(lowTime);
			warningSiren = true;
			updateText();
		}
		else if (remainingTime < 10 && remainingTime > 0)
		{
			if (remainingTime % 2 == 1) timerText.color = Color.red;
			else timerText.color = Color.black;
			updateText();
		}
		else if (remainingTime < 0) player.isMovementLocked = true;
		else updateText();
	}

	//Only exists so it can be called externally as needed.
	public void updateText()
	{
		timerText.text = "Remaining Time: " + remainingTime.ToString() + "s";
	}
}
