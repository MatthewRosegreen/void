using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private float speed;

	// Use this for initialization
	void Start () {
		speed = 0.1f;

		rb2d = GetComponent<Rigidbody2D>();
		var possibleRows = new int[] { -4, -2, 0, 2, 4 };
		var yCoordinate = possibleRows[Random.Range(0, possibleRows.Length - 1)];
		Debug.Log("Fireball spawned at " + yCoordinate);
		rb2d.MovePosition(new Vector2(10, yCoordinate));
	}
	
	// Update is called once per frame
	void Update () {
		MovePosition();
	}

	private void MovePosition()
	{
		var pos = rb2d.position;
		rb2d.MovePosition(new Vector2(pos.x - speed, pos.y));
	}
}
