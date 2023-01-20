using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Strategy : MonoBehaviour
{
    public abstract Vector3 Move(GameObject target);

    public abstract void Attack();

}
