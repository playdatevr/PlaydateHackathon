using UnityEngine;
using System.Collections;

public class ObjectFloat : MonoBehaviour {

	//Bouncing While Inactive
	public float amplitude = 0.001f;
	public float speed = 3.0f;
	private Vector3 initPos;
	private float initShift;


	//Active or Inactive
	private bool modelSelected = false;


	// Use this for initialization
	void Awake () {
	

		initShift = Random.Range (-2 * Mathf.PI, 2 * Mathf.PI);
	}

	// Update is called once per frame

	void Update () {

		//Object bobs up and down when it is inactive
		if (modelSelected == false) {
			Vector3 currentPos = transform.position;
			transform.position = currentPos + new Vector3 (0f, (amplitude * Mathf.Cos (speed * Time.time + initShift)), 0f);

		}


	}
}
