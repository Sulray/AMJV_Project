﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] 
    private int damage;
    [SerializeField]
    private bool onPlayerSide;
    public ProjectileManager ProjectileManager { get; set; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            ProjectileManager.BroadcastMessage("OnDestroyProjectile", this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Wall")
        {
            ProjectileManager.BroadcastMessage("OnDestroyProjectile", this);
        }
        if (tag == "Player")
        {
            collision.gameObject.BroadcastMessage("Damage", damage);
        }
    }
}