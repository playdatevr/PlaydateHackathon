using UnityEngine;
using System.Collections;

public class bubbleInteraction : MonoBehaviour {

	float rotSpeed = 1.0f;
    float bRadius = 1.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		int layerMask = ~(1 << 8);
		if (Physics.Raycast (ray, out hit, 100,layerMask)) {
			//transform.position = hit.point;
		}

		float d = Input.GetAxis("Mouse ScrollWheel");
//		Debug.Log (d);
		if (d < 0f)
		{
			// scroll up
			transform.localScale += new Vector3(0.2F, 0.2F, 0.2F);
		}
		else if (d > 0f)
		{
			// scroll down
			transform.localScale -= new Vector3(0.2F, 0.2F, 0.2F);
		}


        transform.localScale = new Vector3(bRadius, bRadius, bRadius);


		//rotate like a globe
//		transform.Rotate(new Vector3(-Time.deltaTime*20,-Time.deltaTime*20,0));

        SelectRadius(transform.position, bRadius);

			
	}

    void SelectRadius(Vector3 center, float radius) {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach(Collider c in hitColliders) {
            print(c.name);
        }
    }

	void OnCollisionEnter ()
	{
		
	}
}

