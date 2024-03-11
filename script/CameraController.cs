using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private Camera theCam;

    private Transform target;

    public BoxCollider2D aeraBox;

    private float halfwidth,halfheight;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        theCam = GetComponent<Camera>();
        target = PlayerController.instance.transform;

        halfheight = theCam.orthographicSize;
        halfwidth = theCam.orthographicSize*theCam.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        if (aeraBox != null)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, aeraBox.bounds.min.x + halfwidth, aeraBox.bounds.max.x - halfwidth), Mathf.Clamp(transform.position.y, aeraBox.bounds.min.y + halfheight, aeraBox.bounds.max.y - halfheight), transform.position.z); ;

        }
    }
}
