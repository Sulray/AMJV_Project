using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Warrior : HeroController
{
    private float cooldownGlobal; //ce cooldown empêche le joueur de faire plusieurs actions en même temps
    private bool isCooldown1Over; //il y a un booléen cooldown pour chaque action en plus du cooldown global
    public float knockbackForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        isCooldown1Over = true;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        SwordHit();
    }

    public void SwordHit()
    {
        if (inputAction1 &&isCooldown1Over)
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 5, transform.forward, out hit, 4))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance);
                Debug.Log("Did Hit");
                hit.collider.GetComponent<Damage>().TakeDamage(5);
                Vector3 knockDirection = (hit.collider.transform.position - transform.position).normalized;
                Vector3 knockback = knockDirection * knockbackForce;
                Rigidbody rbTarget = hit.collider.GetComponent<Rigidbody>();
                rbTarget.AddForce(knockback);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000);
                Debug.Log("Did not Hit");
            }
            StartCoroutine(Action1Cooldown(1));
        }
    }


    public IEnumerator Action1Cooldown(float cooldown) //la coroutine prend en arguments le temps de cooldown pour une action donnée
    {
        isCooldown1Over = false;
        yield return new WaitForSeconds(cooldown);
        isCooldown1Over = true;
    }

}
