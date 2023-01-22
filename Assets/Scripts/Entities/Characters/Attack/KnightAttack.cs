using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : Attack
{

    public override void First()
    {
        Debug.Log("Knight First Attack");
        RaycastHit[] hitArray = Physics.SphereCastAll(transform.position, 4, transform.forward, 4);
        foreach (RaycastHit hit in hitArray)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance);
                Debug.Log("Did Hit");
                Debug.Log(hit.collider);
                hit.collider.GetComponent<Enemy>().GetKnockback(transform.position);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000);
                Debug.Log("Did not Hit");
            }
        }
    }

    public override void Second()
    {
        Debug.Log("Knight Second Attack");

    }
    public override void Third()
    {
        Debug.Log("Knight Third Attack");

    }
}
