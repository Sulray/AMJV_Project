using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAttack : Attack
{

    ProjectileManager HunterArrowManager;
    HeroController controller;
    Transform transformProjectileSource;
    [SerializeField]
    GameObject Trap;

    public void Start()
    {
        HunterArrowManager = GameObject.Find("HunterArrowManager").GetComponent<ProjectileManager>();
        controller = this.gameObject.GetComponent<HeroController>();
        transformProjectileSource = GameObject.FindGameObjectWithTag("Projectile_Source").transform;
    }

    public override void First()
    {
        Debug.Log("Hunter First Attack");
        RaycastHit hit;
        if (Physics.Raycast(controller.rayMouse, out hit))
        {
            HunterArrowManager.SendMessage("OnArrowProjectile", new Vector3[] { transformProjectileSource.position, new Vector3(hit.point.x, transformProjectileSource.position.y, hit.point.z) });
            Debug.Log("shot");
        }
    }

    public override void Second()
    {
        Instantiate(Trap, this.transform.position, Quaternion.identity);
    }
    public override void Third()
    {

    }
}
