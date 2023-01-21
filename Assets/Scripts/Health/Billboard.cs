using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    Transform cam;

<<<<<<< HEAD
    // Start is called before the first frame update
    void Start()
    {
        cam = UnityEngine.GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
=======
>>>>>>> 82870c0cf7168199ad85a287387778cc55e7224a
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward); // make the UI object stay in front of the camera
    }
}
