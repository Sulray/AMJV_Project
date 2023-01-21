using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private UnityEngine.GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = UnityEngine.GameObject.FindGameObjectsWithTag("Player")[0];

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x,player.transform.position.y + 5, player.transform.position.z - 6);
        transform.LookAt(player.transform.position);
    }
}
