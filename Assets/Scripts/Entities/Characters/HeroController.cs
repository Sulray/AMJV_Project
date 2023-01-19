using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    [SerializeField] float directionOffset;
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] GameObject playerModel;
    public float speed;

    public bool inputAction1;
    public bool inputAction2;
    public bool inputAction3;
    public bool inputJump;
    float xMove;
    float zMove;

    public Rigidbody rb;
    public float buttonTimeJump = 0.3f;
    public float jumpAmount = 20;
    float jumpTime;
    bool jumping;
    bool Grounded;
    bool canMove;
    [SerializeField] float gravityOnFall;


    float momentSpeed;
    bool moving;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = playerModel.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        Action();
        inAir();
        Grounded = IsGrounded();

        momentSpeed = agent.velocity.magnitude / agent.speed;
        //Debug.Log(momentSpeed);
        moving = (momentSpeed != 0);
        Debug.Log("Grounded : " + Grounded);
        if (Grounded) 
        {
            agent.destination = transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * directionOffset;
        }
        animator.SetFloat("floatVelocity",momentSpeed);
        //animator.SetBool("Moving", moving);
        //rb.velocity = new Vector3(xMove,0, zMove).normalized * speed + new Vector3(0,rb.velocity.y,0); // Creates velocity in direction of value equal to keypress (WASD). rb.velocity.y deals with falling + jumping by setting velocity to y. 
        //Debug.Log(rb.velocity+" at speed "+speed);
    }


    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up,0.1f);
    }

    public void Inputs()
    {
        xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1
        inputAction1 = Input.GetKeyDown(KeyCode.Mouse0);
        inputAction2 = Input.GetKeyDown(KeyCode.Mouse1);
        inputAction3 = Input.GetKeyDown(KeyCode.Space);

    }

    public void Action()
    {
        if (inputAction1)
        {
            StartCoroutine(TimeAttack(1));

        }
        /*
        if (inputJump && isGrounded == true)
        {
            animator.SetFloat("Jumping", 1);
            animator.SetFloat("Trigger Number", 1);
            jumping = true;
            jumpTime = 0;

        }
       
        if (jumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpAmount, rb.velocity.z);
            jumping = false;
            
            //    jumpTime += Time.deltaTime;
        }
        //if (Input.GetKeyUp(KeyCode.Space) | jumpTime > buttonTimeJump)
        //{
        //    jumping = false;
        //}
        */
    }

    public void inAir()
    {
        Debug.Log("velocity on y : " + rb.velocity.y);
        if (rb.velocity.y < 0) 
        {
            //rb.AddForce(new Vector3(0, gravityOnFall, 0), ForceMode.Acceleration);
            animator.SetInteger("intAir", 2);
        }
        if (Grounded)
        {
            animator.SetInteger("intAir", 0);
            agent.enabled = true;

        }
        else
        {
            agent.enabled = false;
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        /*
        if (hit.gameObject.CompareTag("Ground"))
        {
            //iGrounded = true;
            animator.SetInteger("intAir", 0);
            rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
        }
        */

    }

    void OnCollisionExit(Collision hit)
    {
        if (hit.gameObject.CompareTag("Ground"))
        {
            //isGrounded = false;
        }
    }


    IEnumerator TimeAttack(int intAttack)
    {
        animator.SetInteger("intAttack", intAttack);
        yield return new WaitForSeconds(0.1f);
        animator.SetInteger("intAttack", 0);

    }
}
