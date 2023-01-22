﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStrategy : Strategy
{
    private float xDistanceFromPlayer = 0.4f; //0 to 0.5, used in viewport
    private float yDistanceFromPlayer = 0.4f; //0 to 0.5, used in viewport
    private float zCameraToGround;
    
    private void Start()
    {
        zCameraToGround = Camera.transform.position.y;
    }

    public override Vector3 Move()
    {
        //select one of the four areas inside the viewport but away from the player; will go slightly more into the corners but osef
        int area = Random.Range(0, 4);
        switch (area)
        {
            case 0: //bottom
                return Camera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 0.5f - yDistanceFromPlayer), zCameraToGround));
            case 1: //top
                return Camera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f + yDistanceFromPlayer, 1f), zCameraToGround));
            case 2: //right
                return Camera.ViewportToWorldPoint(new Vector3(Random.Range(0.5f + xDistanceFromPlayer, 1f), Random.Range(0f, 1f), zCameraToGround));
            case 3: //left
                return Camera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 0.5f - xDistanceFromPlayer), Random.Range(0f, 1f), zCameraToGround));
            default:
                return gameObject.transform.position;
        }
    }
    public override bool Attack()
    {
        StartCoroutine(Fire(Random.Range(1,6)));
        return true;
    }

    private IEnumerator Fire(int ammo)
    {
        //idée de Polish : donner aux flèches une direction random dans un cone tourné vers l'ennemi
        for(int i = 0; i< ammo; i++)
        {
            Vector3[] tempStore = new Vector3[2];
            tempStore[0] = this.transform.position;
            tempStore[1] = Target.transform.position;
            ArrowManager.SendMessage("OnFireProjectile", tempStore);
            yield return new WaitForSeconds(0.5f);
            
        }
    }
}
