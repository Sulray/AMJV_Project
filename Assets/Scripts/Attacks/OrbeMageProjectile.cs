using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbeMageProjectile : MonoBehaviour
{
    [SerializeField] int damage=2;
    [SerializeField] int enemyCount=0;
    [SerializeField] int enemyCountMax=5;
    [SerializeField] int timeMax=5;
    [SerializeField] float speedRotation= 20f; 
    [SerializeField] float distance = 2f;


    Transform target;
    Transform parent;


    // Start is called before the first frame update
    void Start()
    {
        
        enemyCount = 0;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        parent = this.transform.parent;
        
        parent.position = target.position + new Vector3(0, 1, 0);
        this.transform.localPosition = new Vector3(distance, 0, 0);

        StartCoroutine(DestructionTime());

    }


    private void OnEnable()
    {
        StartCoroutine(DestructionTime());

    }


    // Update is called once per frame
    void Update()
    {
        parent.position = target.position + new Vector3(0, 1, 0);
        parent.Rotate(0, speedRotation * Time.deltaTime, 0);
    }


    private void OnTriggerEnter(Collider collider)
    {
        string tag = collider.gameObject.tag;
        
        if (tag == "Enemy")
        {
            collider.gameObject.BroadcastMessage("Damage", damage);
            enemyCount += 1;
            if (enemyCount == enemyCountMax) 
            {
                parent.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DestructionTime()
    {
        yield return new WaitForSeconds(timeMax);
        parent.gameObject.SetActive(false);

    }
}
