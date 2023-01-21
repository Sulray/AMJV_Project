using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Warrior : HeroController
{
    private float cooldownGlobal; //ce cooldown empêche le joueur de faire plusieurs actions en même temps
    private bool isCooldown1Over; //il y a un booléen cooldown pour chaque action en plus du cooldown global

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
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000);
                Debug.Log("Did not Hit");
            }
            StartCoroutine(ActionsCooldown(1, isCooldown1Over));
        }
    }


    private IEnumerator ActionsCooldown(float cooldown, bool isCooldownOver) //la coroutine prend en arguments le temps de cooldown pour une action donnée
                                                                             //et le booléen correspondant à l'action.
    {
        isCooldownOver = false;
        yield return new WaitForSeconds(cooldown);
        isCooldownOver = true;
    }

}
