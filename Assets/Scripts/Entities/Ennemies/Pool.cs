using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private Stack<GameObject> pool;
    public GameObject prefab;
    private int size;
    

    /*public Pool(int size, Enemy prefab)
    {
        Debug.Log("constructor");
        pool = new Stack<Enemy>(size);
        this.prefab = prefab;
        this.size = size;
    }*/

    private void Awake()
    {
        pool = new Stack<GameObject>(size);
    }

    public void SetPrefab(GameObject prefab)
    {
        this.prefab = prefab;
    }
    public GameObject GetObject()
    {
        //Debug.Log("get");
        if (pool.Count == 0)
        {
            return Instantiate(prefab);
            //GameObject obj = Instantiate(prefab);   
            //return obj;
        }
        else
        {
            GameObject obj = pool.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        if(pool.Count >= size)
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
