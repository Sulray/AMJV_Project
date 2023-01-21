using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    Transform cam;

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward); // make the UI object stay in front of the camera
    }
}
