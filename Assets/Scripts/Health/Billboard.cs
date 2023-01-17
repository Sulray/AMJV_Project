﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward); // make the UI object stay in front of the camera
    }
}