﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private Stack<Enemy> pool;
    public Enemy prefab;
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
        pool = new Stack<Enemy>(size);
    }

    public void SetEnemy(Enemy prefab)
    {
        this.prefab = prefab;
    }
    public Enemy GetEnemy()
    {
        //Debug.Log("get");
        if (pool.Count == 0)
        {
            //Debug.Log("empty");
            Enemy enemy = Instantiate(prefab);
            //enemy.gameObject.SetActive(true);
            //pool.Push(enemy);
            return enemy;
        }
        else
        {
            //Debug.Log("Pop");
            Enemy enemy = pool.Pop();
            enemy.gameObject.SetActive(true);
            return enemy;
        }
    }

    public void ReturnEnemy(Enemy enemy)
    {
        if(pool.Count >= size)
        {
            Destroy(enemy);
        }
        else
        {
            enemy.gameObject.SetActive(false);
            pool.Push(enemy);
        }
    }
}
