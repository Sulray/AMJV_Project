using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStrategy : Strategy
{
    private Transform target;
    public override Vector3 Move(GameObject target)
    {
        return target.transform.position;
    }

    public override void Attack()
    {

    }
}
