using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudPopulate : MonoBehaviour {

    public GameObject cloud;
    public int num = 20;
    public float zOff = 3.0f;
    public float xSpan = 1.0f;
    public float ySpan = 1.0f;
    public float zSpan = 1.0f;

    List<Domain> domainList = new List<Domain>();

    // Use this for initialization
    void Start () {

        domainList.Add(new Domain(-xSpan, xSpan));
        domainList.Add(new Domain(-ySpan, ySpan));
        domainList.Add(new Domain(-zSpan, zSpan));

        
        if(cloud != null) {

            for (int i = 0; i < num; ++i) {
                Vector3 v = new Vector3(GetRamdom(domainList[0]), GetRamdom(domainList[1]) + zOff, GetRamdom(domainList[2]));
                //Debug.Log(v);
                GameObject g = Instantiate(cloud,  v, Quaternion.identity) as GameObject;
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
	


	}

    float GetRamdom(Domain domain) {
        return (float)Random.Range(domain.min, domain.max);
    }

    public class Domain{
    public float min = 0.0f;
    public float max = 0.0f;
    
        public Domain(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

    }

}
