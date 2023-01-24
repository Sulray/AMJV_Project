using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class WaveManager : MonoBehaviour
{
    //Objects qui seront récupérés dans awake et passés à chaque entité spawn
    private GameObject player;
    private Camera camera;

    [SerializeField] private ExperiencePool xpPool;
    [SerializeField] private ProjectileManager projectileManager;
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
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text text;

    //entre 4 et 8 spawnpoints
    [SerializeField] private GameObject[] spawnpoints;

    //To handle death
    public UnityAction<Enemy> despawn;
    
    private void Awake()
    {
        pools = new Pool[enemies.Length];
        player = GameObject.FindWithTag("Player");
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        int i = 0;
        foreach(Enemy enemy in enemies)
        {
            pools[i] = gameObject.AddComponent<Pool>();
            pools[i].Prefab = enemy;
            pools[i].Size = poolSize;
            i += 1;
        }
    }

    private void Start()
    {
        despawn += Despawn;
        timer = 0;
        StartCoroutine("Life");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Spawn(pickEnemy(), Vector3.zero);
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
            slider.value = (timer / waveDelay);
            timer += Time.deltaTime;
            if (timer >= waveDelay)
            {
                timer = 0;
                //update the fibonacci sequence
                tmpFib = currentFib;
                currentFib += lastFib;
                lastFib = tmpFib;
                wave += 1;
                text.text = "Wave " + wave;
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
                    if (!CheckSpawn(spawnpoint.transform.position) && (1 == Random.Range(0,2)))
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
        guy.ProjectileManager = projectileManager;
        guy.Manager = this;
        //guy.Xp = xpPool;
        guy.gameObject.SetActive(true);
    }


    //Returns an enemy to corresponding pool
    public void Despawn(Enemy enemy)
    {
        Debug.Log("despawn");
        //Would've been better to make a WaveManager<T> class and fuse it with pools but it's too late so here's a rusty implementation
        foreach(Enemy prefab in enemies)
        {
            for (int i = 0; i<= enemies.Length; i++) //check for each pool if the enemy to return belongs
            {
                if (prefab.CompareTag(enemy.tag))
                {
                    pools[i].ReturnObject(enemy);
                    break;
                }
            }
            
        }
    }
}
