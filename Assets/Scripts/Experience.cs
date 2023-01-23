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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestroy(lifetime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelSystem = other.GetComponent<LevelSystem>();
            levelSystem.AddExeprience(amount);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator AutoDestroy(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }
}
