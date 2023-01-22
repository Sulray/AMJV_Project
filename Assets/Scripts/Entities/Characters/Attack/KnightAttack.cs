using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : Attack
{

    public override void First() //le player assène un coup d'épée devant lui qui repousse les ennemis dans la trajectoire
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
                hit.collider.GetComponent<Enemy>().GetKnockback(transform.position); //on lance dans le script ennemy la fonction qui va repousser la cible
                hit.collider.SendMessage("OnTakeDamage", 5);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000);
                Debug.Log("Did not Hit");
            }
        }
    }

    public override void Second() //le player fait un bond et affecte et repousse tous les ennemis autour de lui quand il retombe 
    {
        Debug.Log("Knight Second Attack");
        if (GetComponent<HeroController>().grounded)
        {
            Debug.Log("jumping");
        }

    }
    public override void Third() //le player tournoie pendant 3 secondes sur lui même
    {
        Debug.Log("Knight Third Attack");
        RaycastHit[] hitArray = Physics.SphereCastAll(transform.position, 5, Vector3.zero, 5);
        foreach (RaycastHit hit in hitArray)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.zero) * hit.distance);
                Debug.Log("Did Hit");
                Debug.Log(hit.collider);
                for (int i=0; i < 6; i++)
                {
                    StartCoroutine(DamageTournoiment());
                    hit.collider.SendMessage("OnTakeDamage", 1);
                }
            }
        }

    }

    public IEnumerator DamageTournoiment()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
