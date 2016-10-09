using UnityEngine;
using System.Collections;

public class RandomScale : MonoBehaviour {

	// Use this for initialization
	void Start () {

		float randX = Random.Range (.05f, .5f);
		float randY = Random.Range (.05f, .5f);
		float randZ = Random.Range (.05f, .5f);

		transform.localScale = new Vector3 (randX, randY, randZ);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
