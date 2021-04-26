using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private float speed;
	private int lives;
	private AudioSource _audioSource;
	private AudioClip _hitResource;
	private AudioClip _explosionResource;
	private AudioSource _thrustersAudioSource;
	public GameObject gameOverPrefab;


	// Use this for initialization
	void Start () {
		speed = 0.1f;
		lives = 3;
		
		rb2d = GetComponent<Rigidbody2D>();
		_audioSource = GetComponent<AudioSource>();
		_hitResource = Resources.Load<AudioClip>("Audio/hit");
		_explosionResource = Resources.Load<AudioClip>("Audio/explosion");
		_thrustersAudioSource = transform.parent
			.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {		
		MovePosition();
	}

	private IEnumerator WaitUntilFrameLoad()
    {
        yield return new WaitForEndOfFrame();
    }

	private void MovePosition()
	{
		var pos = rb2d.position;
		var isUpArrowDown = Input.GetKey(KeyCode.UpArrow);
		var isDownArrowDown = Input.GetKey(KeyCode.DownArrow);

		if (isUpArrowDown && isDownArrowDown){
			return;
		}

		if (isUpArrowDown){
			rb2d.MovePosition(new Vector2(pos.x, pos.y + speed));
		}
		else if (isDownArrowDown){
			rb2d.MovePosition(new Vector2(pos.x, pos.y - speed));
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name.Contains("fireball"))
		{
			Destroy(collision.gameObject);
			Debug.Log("Fireball hit!");

			lives--;
			var livesText = string.Empty;
			for (int i = 0; i < lives; i++)
			{
				livesText += "_ ";
			}

			transform.parent
				.Find("HUBCanvas")
				.Find("LivesText")
				.GetComponent<Text>()
				.text = livesText;

			if (lives <= 0)
			{
				Debug.Log("Stopping " + _thrustersAudioSource.clip.name);
				_thrustersAudioSource.Stop();
				_thrustersAudioSource.PlayOneShot(_explosionResource);
				
				transform.parent
					.GetComponent<GameController>()
					.isGameOver = true;
				Instantiate(gameOverPrefab);
				Destroy(gameObject);
				

				Debug.Log("Game over!");
			}
			else
			{
				PlaySoundEffect(_hitResource);
			}
		}
	}

	private void PlaySoundEffect(AudioClip sound)
	{
		_audioSource.clip = sound;
		Debug.Log("Playing " + _audioSource.clip.name);
		_audioSource.PlayOneShot(sound, 0.7f);
		//yield return new WaitForSeconds(_audioSource.clip.length);
		//PlayThrusters();
	}
}
