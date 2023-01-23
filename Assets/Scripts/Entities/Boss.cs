using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;


public class Boss : MonoBehaviour
{

    BossStates currentState;
    
    Health health;
    NavMeshAgent agent;
    public Animator animator;
    Rigidbody rb;

    bool isCooldownOver;
    bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case BossStates.FirstPhase:
                Debug.Log("Phase 1");
                break;
            case BossStates.SecondPhase:
                Debug.Log("Phase 2");
                break;
            case BossStates.ThirdPhase:
                Debug.Log("Phase 3");
                break;
            default:
                Debug.Log("State not in the list");
                break;
        }
    }


    void UpdatePhase1()
    {

    }

    void UpdatePhase2()
    {

    }

    void UpdatePhase3()
    {

    }

    void SwitchToState(BossStates newState)
    {
        currentState = newState;
    }

    void TransitionToPhase1()
    {

    }

    void TransitionToPhase2()
    {

    }

    void TransitionToPhase3()
    {

    }



    public IEnumerator Action2Cooldown(float cooldown) //la coroutine prend en arguments le temps de cooldown pour une action donnée
                                                       //et le booléen correspondant à l'action.
    {
        isCooldownOver = false;
        yield return new WaitForSeconds(cooldown);
        isCooldownOver = true;
    }


    IEnumerator TimeAttack(int intAttack, float wait)
    {
        animator.SetInteger("intAttack", intAttack);
        canMove = false;
      
        yield return new WaitForSeconds(wait);
        canMove = true;
        animator.SetInteger("intAttack", 0);

    }
}
