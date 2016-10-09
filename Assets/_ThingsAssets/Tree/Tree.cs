using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tree : MonoBehaviour
{
    public bool keepTreeUpright = true;
    static public List<GameObject> treeList = new List<GameObject>();
    static public void GetTree(string treeType) {   
    }

    Animation ani;
    Rigidbody rig;
    Collider coll;
    Renderer render;

    void Awake()
    {
        ani = this.GetComponent<Animation>();
        rig = this.GetComponent<Rigidbody>();
        coll = this.GetComponent<Collider>();
        render = this.GetComponentInChildren<Renderer>();
  
    }
    void Start()
    {

    }


    bool updateBool = true;

    void Update()
    {
        if (updateBool)
        {
            if (!ani.IsPlaying("start"))
            {
                ani.Play("loop_01");
                updateBool = false;
            }
        }

        if(keepTreeUpright) {
            
            //transform.rotation = Quaternion.LookRotation(new Vector3(1,0,0), new Vector3(0,1,0));
            transform.rotation = Quaternion.FromToRotation(transform.up, new Vector3(0,1,0)) * transform.rotation;

        }   


    }

 
}
