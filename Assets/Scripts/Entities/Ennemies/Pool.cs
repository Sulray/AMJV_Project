using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    //duplicata de code car WaveManager utilise explicitement des Pools<Enemy> pour pouvoir leur set certains paramètres, alors que pour les autres on veut utiliser     
    //Pools<GameObject> et qu'on ne peut pas transtiper un gameobject en Enemy car Enemy est un script

    private Stack<Enemy> pool;
    public Enemy Prefab { get; set; }
    public int Size { get; set; }
    

    /*public Pool(int size, Enemy prefab)
    {
        Debug.Log("constructor");
        pool = new Stack<Enemy>(size);
        this.prefab = prefab;
        this.size = size;
    }*/

    private void Awake()
    {
        pool = new Stack<Enemy>(Size);
    }
    public Enemy GetObject()
    {
        //Debug.Log("get");
        if (pool.Count == 0)
        {
            return Instantiate(Prefab);
            //GameObject obj = Instantiate(prefab);   
            //return obj;
        }
        else
        {
            Enemy obj = pool.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    public void ReturnObject(Enemy obj)
    {
        if(pool.Count >= Size)
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
