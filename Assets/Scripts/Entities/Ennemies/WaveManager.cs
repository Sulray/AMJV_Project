using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //On utilise une Pool d'objets pour optimiser la mémoire
    [SerializeField] private Enemy[] enemies;
    private Pool[] pools = new Pool[5];

    private int wave;
    private int maxEnemies = 500;
    private float spawnSpeed = 2;
    private float waveDelay = 5;
    private float timer;

    //entre 4 et 8 spawnpoints
    [SerializeField] private GameObject[] spawnpoints;
    
    private void Awake()
    {
        int i = 0;
        foreach(Enemy enemy in enemies)
        {
            pools[i] = new Pool(50, enemies[i]);
            i += 1;
        }
    }

    private void Start()
    {
        timer = 0;
        StartCoroutine("Life");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Life()
    {
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= waveDelay)
            {
                timer = 0;
                yield return startWave(wave);
                wave += 1;
            }
        }
    }
    private IEnumerator startWave(int wave)
    {
        yield return null;
    }
    
    //On préfère spawn à partir de l'id de l'ennemi pour accéder directement à la pool correspondante
    private void Spawn(int enemy, int number, Vector3 spawn)
    {
        for (int i=0; i<= number; i++)
        {
            Enemy guy = pools[enemy].GetEnemy();
            guy.transform.position = spawn;
        }
    }
}
