using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private Stack<Projectile> pool;
    [SerializeField] private Projectile prefab;
    [SerializeField] private int size;
    [SerializeField] private int projSpeed = 3;
    

    private void Awake()
    {
        pool = new Stack<Projectile>(size);
        
    }

    private void Update()
    {

    }
    public void OnFireProjectile(int damage, Vector3 shooter, Vector3 target)
    {
        Projectile obj;
        if (pool.Count == 0)
        {
            obj = Instantiate(prefab);
            obj.ProjectileManager = this;
            obj.gameObject.SetActive(false);
        }
        else
        {
            obj = pool.Pop();
        }
        
        obj.damage = damage;
        obj.transform.position = shooter;
        obj.transform.right = -(target - shooter).normalized;
        obj.gameObject.GetComponent<Rigidbody>().velocity = (target - shooter).normalized * projSpeed;
        obj.gameObject.SetActive(true);

    }

    public void OnDestroyProjectile(Projectile obj)
    {
        if (pool.Count >= size)
        {
            Destroy(obj.gameObject);
        }
        else
        {
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }
}
