using UnityEngine;
using System.Collections;

public class CloudMovement : MonoBehaviour {

    float t = 0.0f;
    public float scale = 0.01f;
    
    void Awake()
    {
        t = Random.Range(-1.0f, 1.0f);
        this.transform.localScale += new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f));

        transform.Rotate(Vector3.up * Random.Range(-180.1f, 180.1f), Space.World);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 v = this.transform.position;

        this.transform.position = new Vector3(v.x, v.y + (Mathf.Cos(t) * scale), v.z);
        
        t += 0.01f;
    }
}
