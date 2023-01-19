using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ArcherAttack : Attack
{
    public override void First()
    {
        Debug.Log("first");
    }

    public override void Second()
    {
        Debug.Log("second");
    }

    public override void Third()
    {
        Debug.Log("third");
    }
}
