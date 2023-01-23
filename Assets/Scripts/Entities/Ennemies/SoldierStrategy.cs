using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStrategy : Strategy
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

    public override Vector3 Move()
    {
        return Target.transform.position;
    }

    public override bool Attack()
    {
        RaycastHit[] hitArray = Physics.SphereCastAll(transform.position, range, transform.forward, range);
        foreach (RaycastHit hit in hitArray)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
              Debug.Log("Hit");
              return true;
            }
        }
        return false;
    }
}
