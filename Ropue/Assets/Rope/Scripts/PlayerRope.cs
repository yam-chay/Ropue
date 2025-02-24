using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRope : MonoBehaviour
{
    [SerializeField] float pushForce;
    [SerializeField] private bool attached = false;
    [SerializeField] private Transform attachedTo;
    [SerializeField] float moveSpeed;
    [SerializeField] float detachForce;
    [SerializeField] GameObject pulleySelected = null;
    [SerializeField] float jumpForce;
    [SerializeField] bool isGround = false;
    [SerializeField] private GameObject startPlatform;
    private GameObject disregard;
    private Rigidbody2D rb;
    private HingeJoint2D hj;
    private bool isStartAttach = false;
    private int coins = 10;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
    }
    // Start is called before the first frame update
    public bool IsStopMoving()
    {
        if ((rb.velocity == Vector2.zero && isGround == false && attached == false))
        {
            return true;
        }
        return false;
    }
    void CheckKeyboardInputs()
    {
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        if (attached)
        {
            Destroy(startPlatform);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (attached)
            {
                rb.AddRelativeForce(new Vector3(-1, 0, 0) * pushForce);
            }
            else
            {
                if (!isStartAttach)
                {
                    transform.Translate(horizontal, 0, 0);
                }
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (attached)
            {
                rb.AddRelativeForce(new Vector3(1, 0, 0) * pushForce);
               
            }
            else if (!isStartAttach)
            {
                if (!isStartAttach)
                {
                    transform.Translate(horizontal, 0, 0);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && attached)
        {
           
            if (attached)
            {
                Slide(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && attached)
        {
            if (attached)
            {
                Slide(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && attached == true)
        {
            Detach();
        }
        if(Input.GetKeyDown(KeyCode.Space) &&  isGround == true)
        {
            Jump();
        }
    }
    public void Attach(Rigidbody2D ropeBone)
    {
        if(isStartAttach == true)
        { 
        rb.AddRelativeForce(new Vector2(rb.velocity.x * 100, rb.velocity.y));
        }
        isStartAttach = true;
        ropeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        hj.connectedBody = ropeBone;
        hj.enabled = true;
        attached = true;
        attachedTo = ropeBone.gameObject.transform.parent;
    }
    private void Detach()
    {
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        attached = false;
        hj.enabled = false;
        hj.connectedBody = null;
        StartCoroutine(AttachedNull());
        rb.AddRelativeForce(new Vector2(rb.velocity.x * detachForce, rb.velocity.y * (detachForce + 30)));
    }
    IEnumerator AttachedNull()
    {

        yield return new WaitForSeconds(0.5f);
        attachedTo = null;

    }


    public void Slide(int direction)
    {
        RopeSegment myConnection = hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;
        if(direction > 0)
            {
            if(myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
            {
                newSeg = myConnection.connectedAbove;
            }
        }
        else
        {
            if(myConnection.connectedBelow != null)
            {
                newSeg = myConnection.connectedBelow;
            }
        }
        if (newSeg != null)
        {
            transform.position = newSeg.transform.position;
            myConnection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            hj.connectedBody = newSeg.GetComponent<Rigidbody2D>();
        }
    }
    private void Jump()
    {
        rb.velocity *= rb.velocity.normalized * jumpForce;
        isGround = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attached)
        {
            if (collision.CompareTag("Rope"))
            {
                if(attachedTo != collision.gameObject.transform.parent)
                {
                    if(disregard == null 
                        || collision.gameObject.transform.parent.gameObject != disregard.gameObject)
                    {
                        Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckKeyboardInputs();
    }
}
