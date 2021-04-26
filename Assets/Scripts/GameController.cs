using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private AudioSource _musicAudioSource;
	private AudioSource _effectsAudioSource;
    private AudioClip _musicResource;
	private AudioClip _thrustersResource;
	public GameObject fireballPrefab;
	private float timeSinceLastFireball;
	private float fireballRefreshTime;
	private float _normalVolume;
	public bool isGameOver;
	private bool isScoreSaved;
	public float distanceTravelled;
	private Text distanceText;

	IEnumerator Start () {
		isGameOver = false;
		isScoreSaved = false;
		_musicAudioSource = transform
            .Find("Music")
            .GetComponent<AudioSource>();
		_effectsAudioSource = GetComponent<AudioSource>();
        _musicResource = Resources.Load<AudioClip>("Audio/Panthalassa");
		_thrustersResource = Resources.Load<AudioClip>("Audio/thrusters");
		Instantiate(fireballPrefab);
		timeSinceLastFireball = 0;
		fireballRefreshTime = 1.5f;
		distanceTravelled = 0;
		distanceText = transform.Find("HUBCanvas")
			.Find("DistanceText")
			.GetComponent<Text>();
		Debug.Log("Score to beat: " + PlayerPrefs.GetInt("highscore", 0));
		yield return StartCoroutine(WaitUntilFrameLoad());
        _normalVolume = 0.7f;
        PlayMusic();
		PlayThrusters();
	}
	
	void Update () {
		if (isGameOver)
		{
			GameOver();
			return;
		}

		UpdateTime();
		UpdateDistance();

		if (timeSinceLastFireball > fireballRefreshTime)
		{
			Instantiate(fireballPrefab);
			timeSinceLastFireball = 0;
		}
	}

	private void UpdateTime()
	{
		timeSinceLastFireball += Time.deltaTime;
		distanceTravelled += (Time.deltaTime * 5);
	}

	private void UpdateDistance()
	{
		if (string.IsNullOrEmpty(distanceTravelled.ToString()))
			return;
		distanceText.text = "Distance: " + (int)distanceTravelled + " miles";
		PlayerPrefs.SetInt("score", (int)distanceTravelled);
	}

	private IEnumerator WaitUntilFrameLoad()
    {
        yield return new WaitForEndOfFrame();
    }

	private void PlayMusic()
    {
        _musicAudioSource.clip = _musicResource;
        Debug.Log("Playing " + _musicAudioSource.clip.name);
        _musicAudioSource.Play();
        //_audioSource.PlayOneShot(_musicResource, 0.7f);
    }

	private void PlayThrusters()
    {
        _effectsAudioSource.clip = _thrustersResource;
        Debug.Log("Playing " + _effectsAudioSource.clip.name);
        _effectsAudioSource.Play();
        //_audioSource.PlayOneShot(_musicResource, 0.7f);
    }

	private void GameOver()
	{
		if (isScoreSaved)
			return;
		
		var highscore = PlayerPrefs.GetInt("highscore", 0);
		if ((int)distanceTravelled > highscore)
		{
			PlayerPrefs.SetInt("highscore", (int)distanceTravelled);
		}
		isScoreSaved = true;
	}
}
