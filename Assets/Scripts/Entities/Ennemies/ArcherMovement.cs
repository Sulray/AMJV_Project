using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : Movement
{
    private Transform target;
    public override Vector3 Move(GameObject target)
    {
        return target.transform.position;
    }
}
