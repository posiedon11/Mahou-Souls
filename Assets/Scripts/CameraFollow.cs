using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    [SerializeField]
    float z = -10;

    public float x_offset =0, y_offset =2;
    // Start is called before the first frame update
    void Start()
    {
        if (!target)
        {
            enabled = false;
            Debug.Log("No target");
        }
    }

    // Update is called once per frame
    void Update()
    {
        z = transform.position.z;
        Vector3 v = target.position;
        v.z = z;
        v.y += y_offset;
        v.x += x_offset;
        transform.position = v;
    }

    public void SetTarget(Transform t)
    {
        target = t;
        enabled = true;
    }
}
