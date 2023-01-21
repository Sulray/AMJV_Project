using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField]
    private int amount;
    [SerializeField]
    private float lifetime;

    private LevelSystem levelSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            levelSystem = other.GetComponent<LevelSystem>();
            levelSystem.AddExeprience(amount);
            Destroy(this.gameObject);
        }
    }
}
