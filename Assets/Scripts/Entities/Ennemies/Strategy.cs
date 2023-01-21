using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Strategy : MonoBehaviour
{
    public Transform Target { get; set; }
    public Camera Camera { get; set; }

    //renvoie la destination de l'ennemi
    public abstract Vector3 Move();

    //gère l'execution de l'attaque, renvoie true si l'attaque est lancée, false sinon
    public abstract bool Attack();

}
