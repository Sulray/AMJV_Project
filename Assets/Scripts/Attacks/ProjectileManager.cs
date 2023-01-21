using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private Stack<Projectile> pool;
    [SerializeField] private Projectile prefab;
    [SerializeField] private int size;

    private void Awake()
    {
        pool = new Stack<Projectile>(size);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            this.BroadcastMessage("OnFireProjectile");
        }
    }
    public Projectile OnFireProjectile()
    {
        Projectile obj;
        if (pool.Count == 0)
        {
            obj = Instantiate(prefab);
            obj.ProjectileManager = this;
            return obj;
        }
        else
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    public void OnDestroyProjectile(Projectile obj)
    {
        Debug.Log("size " + size);
        Debug.Log("count " + pool.Count);
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
