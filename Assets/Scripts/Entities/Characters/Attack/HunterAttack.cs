﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAttack : Attack
{

    ProjectileManager HunterArrowManager;
    HeroController controller;
    Transform transformProjectileSource;
    public GameObject Trap;

    public int attack1Damage = 10;

    public void Start()
    {
        HunterArrowManager = GameObject.Find("HunterArrowManager").GetComponent<ProjectileManager>();
        controller = this.gameObject.GetComponent<HeroController>();
        transformProjectileSource = GameObject.FindGameObjectWithTag("Projectile_Source").transform;
    }

    public override void First()
    {
        Debug.Log("Hunter First Attack");
        //RaycastHit hit;
        //if (Physics.Raycast(controller.rayMouse, out hit))
        //{
         //   HunterArrowManager.SendMessage("OnArrowProjectile", new Vector3[] { transformProjectileSource.position, new Vector3(hit.point.x, transformProjectileSource.position.y, hit.point.z) });
          //  hit.collider.gameObject.GetComponent<Health>().OnTakeDamage(attack1Damage);
           // Debug.Log("shot");
        //}
    }

    public override void Second()
    {
        Debug.Log("Hunter Second Attack");
        Instantiate(Trap, this.transform.position, Quaternion.identity);
    }
    public override void Third()
    {
        Debug.Log("Hunter Third Attack");
        StartCoroutine(EffectsThirdAttack());
    }

    private IEnumerator EffectsThirdAttack()
    {
        float saveSpeed = this.gameObject.GetComponent<HeroController>().speed;
        float saveCd1 = this.gameObject.GetComponent<HeroController>().cooldown1;
        this.gameObject.GetComponent<HeroController>().speed *= 2;
        this.gameObject.GetComponent<HeroController>().cooldown1 /= 2;
        yield return new WaitForSeconds(3);
        this.gameObject.GetComponent<HeroController>().speed = saveSpeed;
        this.gameObject.GetComponent<HeroController>().cooldown1 = saveCd1;
    }
}
