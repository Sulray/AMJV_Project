using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePool : MonoBehaviour
{

    private Stack<Experience> pool;
    [SerializeField] private Experience prefab;
    [SerializeField] private int size;

    private void Awake()
    {
        pool = new Stack<Experience>(size);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SendMessage("OnFireProjectile", (new Vector3[] { Vector3.zero, Vector3.one }));//event
        }
    }
    public void OnSpawnExp(Vector3 source)
    {
        Experience obj;
        if (pool.Count == 0)
        {
            obj = Instantiate(prefab);
            obj.Manager = this;
            obj.gameObject.SetActive(false);
        }
        else
        {
            obj = pool.Pop();
        }
        obj.transform.position = source;
        obj.gameObject.SetActive(true);

    }

    public void OnDestroyExp(Experience obj)
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
