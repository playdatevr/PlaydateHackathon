using UnityEngine;
using System.Collections;

public class SunMovement : MonoBehaviour {

    public Transform light;
    float t = 0.0f;
    public float scale = 1.0f;
    void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 v = light.transform.position;

        this.transform.position = new Vector3(v.x ,v.y + (Mathf.Cos(t) * scale), v.z );
        t += 0.01f;
	}
}
