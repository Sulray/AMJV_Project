using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Vector3 camDirection = new Vector3(0, -13, 8); // opposé de l"offset de la caméra

    void LateUpdate()
    {
        transform.LookAt(transform.position + camDirection);
    }
}