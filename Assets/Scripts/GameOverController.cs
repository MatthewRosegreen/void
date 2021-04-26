using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {
	private Text _highscoreText;


	// Use this for initialization
	void Start () {
		_highscoreText = transform.Find("HighScoreText").GetComponent<Text>();
		var score = PlayerPrefs.GetInt("score", 0);
		var highscore = PlayerPrefs.GetInt("highscore", 0);
		
		
		if ((highscore < score) || (highscore == 0))
			_highscoreText.text = "High Score!";
		else
			_highscoreText.text = "Score to beat: "
				+ highscore
				+ " miles";
	}
	
	// Update is called once per frame
	void Update () {

	}

	private IEnumerator WaitUntilFrameLoad()
    {
        yield return new WaitForEndOfFrame();
    }

	public void RetryGame()
	{
		SceneManager.LoadScene("main", LoadSceneMode.Single);
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("menu", LoadSceneMode.Single);
	}
}
