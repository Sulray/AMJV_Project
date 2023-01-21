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
    bool grounded;
    [SerializeField] float groundDetection;
    bool canMove;
    [SerializeField] float gravityOnFall;


    float momentSpeed;
    bool moving;
    private Vector3 positionRay;

    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        animator = playerModel.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        Action();
        inAir();
        LookAt();
        grounded = IsGrounded();

        //momentSpeed = agent.velocity.magnitude / agent.speed;
        momentSpeed = rb.velocity.magnitude / speed;

        //Debug.Log(momentSpeed);
        moving = (momentSpeed != 0);
        Debug.Log("Grounded : " + grounded);
        if (grounded) 
        {
            rb.velocity = new Vector3(xMove, 0, zMove).normalized * speed + new Vector3(0, rb.velocity.y, 0); // Creates velocity in direction of value equal to keypress (WASD). rb.velocity.y deals with falling + jumping by setting velocity to y. 
            //agent.destination = transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //* directionOffset  
        }
        animator.SetFloat("floatVelocity",momentSpeed);
        //animator.SetBool("Moving", moving);
        //Debug.Log(rb.velocity+" at speed "+speed);
    }


    bool IsGrounded()
    {
        Debug.DrawRay(transform.position-Vector3.up*0.1f, -Vector3.up * (0.2f + groundDetection), Color.red);
        return Physics.Raycast(transform.position+Vector3.up*0.2f, -Vector3.up,0.3f+groundDetection);

    }

    void LookAt()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            positionRay = hit.point;
        }
        Quaternion Rotation = Quaternion.LookRotation(new Vector3(positionRay.x, transform.position.y, positionRay.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, 0.3f);
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
        if (inputAction2)
        {
            StartCoroutine(TimeAttack(2));

        }
        if (inputAction3)
        {
            StartCoroutine(TimeAttack(3));

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
        if ((rb.velocity.y < -0.1f) && (!grounded))
        {
            //rb.AddForce(new Vector3(0, gravityOnFall, 0), ForceMode.Acceleration);
            animator.SetInteger("intAir", 2);
        }
        else if (grounded)
        {
            animator.SetInteger("intAir", 0);
        }
        /*
        if (grounded)
        {
            animator.SetInteger("intAir", 0);
            //agent.enabled = true;

        }
        else
        {
            //agent.enabled = false;
        }
        */
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
