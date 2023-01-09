using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    private Pool[] pools = new Pool[5];
    //On utilise une Pool d'objets pour optimiser la mémoire
    private void Awake()
    {
        int i = 0;
        foreach(Enemy enemy in enemies)
        {
            pools[i] = new Pool(50, enemies[i]);
            i += 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn(0, 1, Vector3.zero);
        }
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
