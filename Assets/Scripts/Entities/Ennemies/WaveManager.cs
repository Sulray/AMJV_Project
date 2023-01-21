using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    //Objects qui seront récupérés dans awake et passés à chaque entité spawn
    private GameObject player;
    private Camera camera;
    [SerializeField] private Image healthBar; //voir avec louise pour setup ça correctement avec les différents éléments à initialiser
    //On utilise une Pool d'objets pour optimiser la mémoire
    [SerializeField] private Enemy[] enemies;
    private Pool[] pools;
    [SerializeField] private int poolSize;

    private int wave;
    private int currentFib = 0;
    private int lastFib = 1;
    [SerializeField] private int totalEnemies;
    private int maxEnemies = 500;
    private float spawnSpeed = 2;
    private float waveDelay = 2;
    private float timer;


    //entre 4 et 8 spawnpoints
    [SerializeField] private GameObject[] spawnpoints;
    
    private void Awake()
    {
        pools = new Pool[enemies.Length];
        player = GameObject.FindWithTag("Player");
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        int i = 0;
        foreach(Enemy enemy in enemies)
        {
            pools[i] = gameObject.AddComponent<Pool>();
            pools[i].SetPrefab(enemy);
            pools[i].Size = poolSize;
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
        if (Input.GetKey(KeyCode.C))
        {
            totalEnemies -= 1;
        }
        /*timer += Time.deltaTime;
        if (timer >= waveDelay)
        {
            timer = 0;
            IEnumerator newWave = startWave(wave);
            StartCoroutine("newWave");
            wave += 1;

        }*/
    }

    private IEnumerator Life()
    {
        int tmpFib;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= waveDelay)
            {
                timer = 0;
                //update the fibonacci sequence
                tmpFib = currentFib;
                currentFib += lastFib;
                lastFib = tmpFib;
                wave += 1;
                yield return startWave(currentFib);
                
            }
            else
            {
                yield return null; //utile ou ça se fait tout seul de sortir de la coroutine?
            }
        }
    }

    //spawns random ennemies at random spawn anchors if there's room available, until all are spawned
    private IEnumerator startWave(int toSpawn)
    {
        int spawnedEnemies = 0;
        while (spawnedEnemies < toSpawn)
        {
            //polish : optimiser le choix des spawns, attendre qu'il soit vide, etc...
            if (totalEnemies < maxEnemies)
            {   
                foreach (GameObject spawnpoint in spawnpoints)
                {
                    if (!CheckSpawn(spawnpoint.transform.position))
                    {
                        Spawn(pickEnemy(), spawnpoint.transform.position);
                        totalEnemies += 1;
                        spawnedEnemies += 1;
                        yield return new WaitForSeconds(1f / spawnSpeed);
                        break;
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    /*private Vector3 pickSpawn()
    {
        foreach (GameObject spawn in spawnpoints)
        {
            if (!CheckSpawn(spawn.transform.position))
            {
                return spawn.transform.position;
            }
        }
    }*/

    private int pickEnemy()
    {
        return Random.Range(0, pools.Length);
    }

    private bool CheckSpawn(Vector3 spawn)
    {
        return Physics.SphereCast(new Ray(spawn, Vector3.up), 0.8f, 2f);
    }
    
    //spawns an enemy from pool "enemy" at "spawn" anchor
    //On préfère spawn à partir de l'id de l'ennemi pour accéder directement à la pool correspondante
    private void Spawn(int enemy, Vector3 spawn)
    {
        //Debug.Log(enemy);
        Enemy guy = pools[enemy].GetObject();
        guy.transform.position = spawn;
        guy.Player = player;
        guy.Camera = camera;
        guy.gameObject.SetActive(true);
    }
}
