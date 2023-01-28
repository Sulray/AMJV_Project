using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAttack : Attack
{

    ProjectileManager HunterArrowManager;
    HeroController controller;
    Transform transformProjectileSource;
    [SerializeField] private TrapBehavior Trap;

    public int attack1Damage = 10;
    public float attack3Speed = 2;

    public void Start()
    {
        HunterArrowManager = GetComponent<ProjectileManager>();
        controller = this.gameObject.GetComponent<HeroController>();
        //transformProjectileSource = GameObject.FindGameObjectWithTag("Projectile_Source").transform;
        Debug.Log("trap " + Trap);
    }

    public override void First()
    {
        Debug.Log("Hunter First Attack");
        RaycastHit hit;
        if (Physics.Raycast(controller.rayMouse, out hit))
        {
           HunterArrowManager.OnFireProjectile(attack1Damage, transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z));  //SendMessage("OnArrowProjectile", new Vector3[] { transformProjectileSource.position, new Vector3(hit.point.x, transformProjectileSource.position.y, hit.point.z) });
           //hit.collider.gameObject.GetComponent<Health>().OnTakeDamage(attack1Damage);
           Debug.Log("shot");
        }
    }

    public override void Second()
    {
        Debug.Log("Hunter Second Attack");
        Debug.Log(Trap);
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
        this.gameObject.GetComponent<HeroController>().speed *= attack3Speed;
        this.gameObject.GetComponent<HeroController>().cooldown1 /= 2;
        yield return new WaitForSeconds(3);
        this.gameObject.GetComponent<HeroController>().speed = saveSpeed;
        this.gameObject.GetComponent<HeroController>().cooldown1 = saveCd1;
    }
}
