using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDestroyerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name.Contains("fireball"))
		{
			Destroy(collision.gameObject);
			Debug.Log("Fireball miss!");
		}
	}
}
