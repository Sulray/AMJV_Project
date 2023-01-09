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
            pools[i] = new Pool();
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
        
    }
}
