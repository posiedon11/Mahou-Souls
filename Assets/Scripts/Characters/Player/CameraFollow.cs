using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CPSC386
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        float z = -10;//Used to maintain the camera's z level, sometimes important for rendering
        public float x_offset = 0, y_offset = 0;

        void Start()
        {
            if (!target)
            {
                enabled = false;
                Debug.Log("No target");
            }
        }

        void Update()
        {
            z = transform.position.z;
            Vector3 v = target.position;
            v.z = z;
            v.x += x_offset;
            v.y += y_offset;
            transform.position = v;
        }
    }
}