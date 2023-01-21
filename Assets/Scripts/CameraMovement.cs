using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
<<<<<<< HEAD
    private UnityEngine.GameObject player;
=======
    private GameObject player;
    [SerializeField]
    private Vector3 offset;
>>>>>>> 82870c0cf7168199ad85a287387778cc55e7224a
    // Start is called before the first frame update
    void Start()
    {
        player = UnityEngine.GameObject.FindGameObjectsWithTag("Player")[0];

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
    }
}
