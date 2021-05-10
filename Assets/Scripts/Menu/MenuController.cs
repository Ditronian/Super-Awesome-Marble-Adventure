using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject skinsMenu;
	public int activeSkin = 0;
	public PlayerIO playerIO;
	public Button continueButton;

	// Use this for initialization
	void Start () {
		//check for a different active skin
		this.playerIO = FileIO.loadPlayer();

		//Check if new player
		if (playerIO.CurrentLevel == 0) continueButton.interactable = false;
		else
		{
			continueButton.interactable = true;
			Color color;
			ColorUtility.TryParseHtmlString("#dadf38", out color);
			continueButton.GetComponentInChildren<Text>().color = color;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void newGame()
	{
		SceneManager.LoadScene("level1");
	}

	public void continueGame()
	{
		if(playerIO.CurrentLevel == 1) SceneManager.LoadScene("level1");
		else if (playerIO.CurrentLevel == 2) SceneManager.LoadScene("level2");
	}

	public void skinSelect()
	{
		mainMenu.SetActive(false);
		skinsMenu.SetActive(true);
	}

	public void menuSelect()
	{
		mainMenu.SetActive(true);
		skinsMenu.SetActive(false);
	}

	public void quitGame()
	{
		Application.Quit();
	}

	public void skinOneSelect()
	{
		playerIO.SkinChoice = 1;
		FileIO.savePlayer(playerIO);
	}

	public void skinTwoSelect()
	{
		playerIO.SkinChoice = 2;
		FileIO.savePlayer(playerIO);
	}

	public void skinThreeSelect()
	{
		playerIO.SkinChoice = 3;
		FileIO.savePlayer(playerIO);
	}

	public void skinFourelect()
	{
		playerIO.SkinChoice = 4;
		FileIO.savePlayer(playerIO);
	}

	public void skinFiveSelect()
	{
		playerIO.SkinChoice = 5;
		FileIO.savePlayer(playerIO);
	}
}
