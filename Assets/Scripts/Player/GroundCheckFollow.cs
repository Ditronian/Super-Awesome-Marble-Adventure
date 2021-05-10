using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 playerPos = GameObject.Find("marble").transform.position;
		playerPos.y -= 0.358f;
		transform.position = playerPos;
	}
}
