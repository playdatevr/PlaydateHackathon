using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class earthFlat : MonoBehaviour
{
    public bool keepEarthOnGround = true;

    void Awake()
    {

    }
    void Start()
    {

	}
    void Update()
    {


        if(keepEarthOnGround == true) {
            
            //transform.rotation = Quaternion.LookRotation(new Vector3(1,0,0), new Vector3(0,1,0));
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);


        }   


    }

 
}
