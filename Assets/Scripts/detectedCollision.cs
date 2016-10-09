using UnityEngine;
using System.Collections;

public class detectedCollision : MonoBehaviour {
	public bool isSelected = false;
	GameObject parent;

	public GameObject text;

	void OnGUI()
	{
//		text.GetComponent<MeshRenderer> ().enabled = false;
	}

	void Start() {
		parent = this.transform.root.gameObject;
	}

	void Update() {
		if (isSelected && Input.GetMouseButtonDown (0)) {
			print (parent.name);
		}

		//display text when building is selected and disappear when building is not selected
		if (isSelected) {
		
//			text.GetComponent<MeshRenderer> ().enabled = true;
		} else {
//			text.GetComponent<MeshRenderer> ().enabled = false;
		}
	}

	void OnCollisionEnter (Collision collision)
	{
        print("#####");
		print(collision.gameObject.name);
		isSelected = true;
		parent.tag = "isSelected";
			
	}

	void OnCollisionExit (Collision collision)
	{
		isSelected = false;
		parent.tag = "Untagged";

	}

    void SphereCollide() {
        print("I am" + this.gameObject.name);
    }

}
