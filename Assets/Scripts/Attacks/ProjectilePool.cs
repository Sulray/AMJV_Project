using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    private Stack<GameObject> pool;
    [SerializeField] public GameObject prefab;
    [SerializeField] private int size;

    private void Awake()
    {
        pool = new Stack<GameObject>(size);
    }
    public GameObject OnFireProjectile()
    {
        if (pool.Count == 0)
        {
            return Instantiate(prefab);
        }
        else
        {
            GameObject obj = pool.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    public void OnDestroyProjectile(GameObject obj)
    {
        if (pool.Count >= size)
        {
            Destroy(obj);
        }
        else
        {
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }
}
