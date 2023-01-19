using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : Movement
{
    [SerializeField] GameObject target;
    public override Vector3 Move()
    {
        return this.transform.position;
    }
}
