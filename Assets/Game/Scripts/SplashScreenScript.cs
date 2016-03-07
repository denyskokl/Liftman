using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour {


	private int startGame = 3;
	// Use this for initialization
	void Start () {
		Debug.Log ("```");
	}
	
	// Update is called once per frame
	void Update () {
		startGame--;
		if (startGame <= 0) 	
		{
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
