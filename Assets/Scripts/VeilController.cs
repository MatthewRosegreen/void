using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeilController : MonoBehaviour {
	private float fadeSpeed;
	private Rigidbody2D rb2d;
	private bool isMoving;
	private GameObject veilPrefab;
	public int cloneNumber;

	// Use this for initialization
	void Start () {
		fadeSpeed = 0.1f;
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.MovePosition(new Vector2(18.25f, 0));
		isMoving = false;
		veilPrefab = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving)
			return;

		var pos = rb2d.position;
		var newPos = new Vector2(pos.x - fadeSpeed, pos.y);

		rb2d.MovePosition(newPos);
		if (newPos.x <= 0)
		{
			rb2d.MovePosition(new Vector2(0, pos.y));
			isMoving = true;

			Debug.Log("Veil " + cloneNumber + " created a clone");
			var clone = Instantiate(veilPrefab);

			clone.GetComponent<VeilController>()
				.IncreaseCloneNumber(cloneNumber + 1);
		}
	}

	public void IncreaseCloneNumber(int newCloneNumber)
	{
		var maxClones = 15;

		cloneNumber = newCloneNumber;
		if (cloneNumber > maxClones)
			Destroy(gameObject);
		Debug.Log("Veil created: " + cloneNumber);
	}
}
