using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Strategy : MonoBehaviour
{
    public Transform Target { get; set; }
    public Camera Camera { get; set; }

    //set la transform "target" de l'ennemi, appellé lors de l'instanciation du prefab;
    /*public void SetTarget(Transform target)
    {
        this.target = target;
    }*/

    //renvoie la destination de l'ennemi, prend le joueur en argument mais n'est pas obligé de l'utiliser
    public abstract Vector3 Move();

    //gère l'execution de l'attaque, renvoie true si l'attaque est lancée, false sinon
    public abstract bool Attack();

}
