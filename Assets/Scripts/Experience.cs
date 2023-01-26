﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField]
    private float lifetime;
    public ExperiencePool Manager { get; set; }

    private void Awake()
    {
        StartCoroutine(AutoDestroy(lifetime));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SendMessage("OnFireProjectile", (new Vector3[] { Vector3.zero, Vector3.one }));//event
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.CompareTag("Player"))
        {
            Debug.Log("auto destroy");
            Destroy(this.gameObject);
        }
    }

    private IEnumerator AutoDestroy(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Debug.Log("time auto destroy");
        Destroy(this.gameObject);
    }
}
