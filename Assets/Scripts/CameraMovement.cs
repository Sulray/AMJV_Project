﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] float cameraHeight;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x,cameraHeight, player.transform.position.z);
    }
}