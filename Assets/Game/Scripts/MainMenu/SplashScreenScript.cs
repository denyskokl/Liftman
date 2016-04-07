using Soomla.Profile;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour {


	private int startGame = 0;
	// Use this for initialization
	void Start () {
		startGame = 10;
	}

    void Awake()
    {
        SoomlaProfile.Initialize();
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
