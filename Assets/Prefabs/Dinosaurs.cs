using UnityEngine;
using System.Collections;

public class Dinosaurs : MonoBehaviour {

    Animation ani;
    Rigidbody rig;
    Collider coll;
    Renderer render;

    int num = 10;
    int preNum = 14;
    void Awake()
    {


        ani = this.GetComponent<Animation>();
        rig = this.GetComponent<Rigidbody>();
        coll = this.GetComponent<Collider>();
        render = this.GetComponentInChildren<Renderer>();

    }
    public void OnTriggerEnter(Collider other)
    {

    }
    private Color startcolor;
    void OnMouseEnter()
    {
        startcolor = render.material.color;
        render.material.color = Color.white;
    }
    void OnMouseExit()
    {
        render.material.color = startcolor;
    }
    private Vector3 screenPoint;
    private Vector3 offset;
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }
    void OnDestroy()
    {

    }
}
