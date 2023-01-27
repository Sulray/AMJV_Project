using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnightAttack : Attack
{

    public int attack1Damage = 5;
    public int attack2Damage = 2;
    public int attack3Damage = 1;
    public float attack2Range = 1.5f;
    public float attack3Range = 1.5f;
    public override void First() //le player assène un coup d'épée devant lui qui repousse les ennemis dans la trajectoire
    {
        Debug.Log("Knight First Attack");
        RaycastHit[] hitArray = Physics.SphereCastAll(transform.position, 1.5f, transform.forward, 1.5f);
        foreach (RaycastHit hit in hitArray)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance);
                Debug.Log("Did Hit");
                Debug.Log(hit.collider);
                hit.collider.GetComponent<Enemy>().GetKnockback(transform.position); //on lance dans le script ennemy la fonction qui va repousser la cible
                hit.collider.gameObject.GetComponent<Health>().OnTakeDamage(attack1Damage);
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
        if (this.gameObject.GetComponent<HeroController>().grounded) //cette attaque n'est possible que lorsque le joueur n'est pas au dessus d'une fosse
        {
            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, 3, rb.velocity.z);
            StartCoroutine(WaitTilOnGround());
            if (this.gameObject.GetComponent<HeroController>().grounded)
            {
                RaycastHit[] hitArray = Physics.SphereCastAll(transform.position, attack2Range, Vector3.zero, 1.5f);
                foreach (RaycastHit hit in hitArray)
                {
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.zero) * hit.distance);
                        Debug.Log("Did Hit");
                        Debug.Log(hit.collider);
                        hit.collider.GetComponent<Enemy>().GetKnockback(transform.position);
                        hit.collider.gameObject.GetComponent<Health>().OnTakeDamage(attack2Damage);
                    }
                }
            }
        }

    }
    public override void Third() //le player tournoie pendant 3 secondes sur lui même
    {
        Debug.Log("Knight Third Attack");
        StartCoroutine(Tournoiement());
        RaycastHit[] hitArray = Physics.SphereCastAll(transform.position, attack3Range, Vector3.zero, 1.5f);
        foreach (RaycastHit hit in hitArray)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.zero) * hit.distance);
                Debug.Log("Did Hit");
                Debug.Log(hit.collider);
                for (int i=0; i < 6; i++)
                {
                    StartCoroutine(DamageTournoiment()); //le player inflige 1 dégats toutes les 0.5 secondes
                    hit.collider.gameObject.GetComponent<Health>().OnTakeDamage(attack3Damage);
                }
            }
        }

    }

    private IEnumerator DamageTournoiment()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator Tournoiement()
    {
        this.gameObject.GetComponent<HeroController>().canMove = false;
        yield return new WaitForSeconds(3);
        this.gameObject.GetComponent<HeroController>().canMove = true;
    }

    private IEnumerator WaitTilOnGround()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
