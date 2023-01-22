using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MageAttack : Attack
{
    Object rotatingFireball;
    Object baseFireball;



    private void Start()
    {
        baseFireball = LoadPrefab("Assets/FireIceProjectileExplosion/Prefabs/FX_BulletTrail_Blue");
        rotatingFireball = LoadPrefab("Assets/FireIceProjectileExplosion/Prefabs/Rotating_FX_BulletTrail_Blue");
    }

    private Object LoadPrefab(string filename)
    {
        var fireball = Resources.Load(filename);
        if (fireball == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        return fireball;
    }

    public override void First()
    {
        Debug.Log("Mage First Attack");
        
    }

    public override void Second()
    {
        Debug.Log("Mage Second Attack");

    }
    public override void Third()
    {
        Debug.Log("Mage Third Attack");

    }
}
