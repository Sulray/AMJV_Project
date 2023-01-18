using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Warrior : HeroController
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        SwordHit();
    }

    public void SwordHit()
    {
        if (inputAction1)
        {
            Debug.Log("Hit");
        }
    }

}
