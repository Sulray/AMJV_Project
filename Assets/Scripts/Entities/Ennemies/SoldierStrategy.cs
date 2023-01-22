using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStrategy : MonoBehaviour
{
    bool isCdEnemyOver;
    int range = 2;

    // Start is called before the first frame update
    void Start()
    {
        isCdEnemyOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwordAttack()
    {
        RaycastHit[] hitArray = Physics.SphereCastAll(transform.position, range, transform.forward, range);
        foreach (RaycastHit hit in hitArray)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                if (isCdEnemyOver)
                {
                    Debug.Log("Hit");
                    hit.collider.SendMessage("OnTakeDamage", 5);
                    StartCoroutine(CdAttack());
                }
            }
        }
    }

    private IEnumerator CdAttack()
    {
        isCdEnemyOver = false;
        yield return new WaitForSeconds(1);
        isCdEnemyOver = true;
    }
}
