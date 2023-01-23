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

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Wall")
        {
            ProjectileManager.OnDestroyProjectile(this);
        }
        if (tag == "Player")
        {
            //collision.gameObject.GetComponent<
            //
            //>().OnTakeDamage(damage);
            ProjectileManager.OnDestroyProjectile(this);
        }
    }
}
