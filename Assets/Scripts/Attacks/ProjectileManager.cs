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

        }
    }
    public void OnFireProjectile(Vector3[] entities)
    {
        Projectile obj;
        if (pool.Count == 0)
        {
            obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
        }
        else
        {
            obj = pool.Pop();
        }
        obj.transform.position = entities[0];
        obj.transform.LookAt(entities[1]);
        obj.gameObject.GetComponent<Rigidbody>().velocity = (entities[1] - entities[0]).normalized * 3;
        obj.gameObject.SetActive(true);

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
