using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField]
    private int amount;
    [SerializeField]
    private float lifetime;
    [SerializeField]
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
            Destroy(this.gameObject);
        }
    }

    private IEnumerator AutoDestroy(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }
}
