using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private Stack<Enemy> pool;
    private Enemy prefab;
    private int size;

    public void createPool(int size, Enemy prefab)
    {
        pool = new Stack<Enemy>(size);
        this.prefab = prefab;
        this.size = size;
    }

    public Enemy GetEnemy()
    {
        if (pool.Count == 0)
        {
            Enemy enemy = Instantiate(prefab);
            pool.Push(enemy);
            return enemy;
        }
        else
        {
            Enemy enemy = pool.Pop();
            enemy.enabled = true;
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
            enemy.enabled = false;
            pool.Push(enemy);
        }
    }
}
