using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStrategy : Strategy
{
    
    public override Vector3 Move()
    {
        return Target.position;
    }

    public override bool Attack()
    {
        Debug.Log("archer attack");
        return true;
    }
}
