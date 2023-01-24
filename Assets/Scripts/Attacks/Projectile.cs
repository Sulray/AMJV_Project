using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] 
    private int damage;
    [SerializeField]
    private bool onPlayerSide;
    [SerializeField]
    private Rigidbody rb;
    public ProjectileManager ProjectileManager { get; set; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            ProjectileManager.SendMessage("OnDestroyProjectile", this);//utiliser la méthode
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Wall")
        {
            ProjectileManager.OnDestroyProjectile(this);
        }
        else if (tag == "Player" && !onPlayerSide)
        {
            other.gameObject.GetComponent<Health>().OnTakeDamage(damage);
            ProjectileManager.OnDestroyProjectile(this);
        }
        else if (tag == "Enemy" && onPlayerSide)
        {
            other.gameObject.GetComponent<Health>().OnTakeDamage(damage);
            ProjectileManager.OnDestroyProjectile(this);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }
}
