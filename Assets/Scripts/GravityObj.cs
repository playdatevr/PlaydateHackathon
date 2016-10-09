using UnityEngine;
using System.Collections;

public class GravityObj : MonoBehaviour {

GravityPlanet planet;
   Rigidbody GravityPlanet;

Rigidbody newrigidbody;
   void Awake() {
       planet = GameObject.FindGameObjectWithTag("SMALLplanet").GetComponent<GravityPlanet>();
       newrigidbody = GetComponent<Rigidbody>();

       // Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
       newrigidbody.useGravity = false;
       newrigidbody.constraints = RigidbodyConstraints.FreezeRotation;
   }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	 void FixedUpdate() {
       // Allow this body to be influenced by planet's gravity
       planet.Attract(newrigidbody);
   }
}
