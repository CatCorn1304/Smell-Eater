using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody rb => GetComponent<Rigidbody>();
    public Vector3 input;
    [Header("Movement")]
    public float speed = 5;
    public float sniffSpeedMod = 0.5f;
    public float turnSpeed = 360;
    public float pushForce = 10;
    public float stunForce = 5;
    public float maxTimeStunned = 1;
    float gravity;
    float stunTimer;
    bool stunned;
    GameObject stunner;
    [Header("Mechanics")]
    public UnityEvent OnSniff;
    public UnityEvent OnBark;
    public SniffController sniffScript;
    [Header("Utility")]
    public LayerMask groundLayer;
    public string groundType;
    public bool grounded;

    Animator _animator;

    private void Update()
    {
        _animator = GetComponent<Animator>();

        GatherInput();
        Look();
        Move();
        Timers();
        GroundCheck();
    }

    private void FixedUpdate()
    {
    }
    void GroundCheck()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 2, groundLayer);
        grounded = hit.collider;
        if (grounded)
        {
            groundType = LayerMask.LayerToName(hit.collider.gameObject.layer);
        }
    }
    void Timers()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
        else
        {
            stunned = false;
        }
    }
    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (Input.GetButtonDown("Bark"))
        {
            OnBark.Invoke();
        }
        if (Input.GetButtonDown("Sniff")) 
        {
            OnSniff.Invoke(); 
        }
    }

    private void Look()
    {
        if (input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        if (!stunned)
        {
            _animator.SetBool("Run", input.magnitude > 0);
            float moveSpeed = speed * (sniffScript.sniffing ? sniffSpeedMod : 1);
            Vector3 force = (transform.forward * input.normalized.magnitude * moveSpeed);
            force.y = rb.velocity.y;
            rb.velocity = force;
        }
        else
        {
            _animator.SetBool("Run", false);
        }

       
    }

    public void Stun(GameObject barker)
    {
        stunned = true;
        stunTimer = maxTimeStunned;
        Vector3 dir = transform.position - barker.transform.position;
        dir.y = 0;
        rb.AddForce(dir.normalized * stunForce);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Rigidbody rb = collision.collider.attachedRigidbody;
        //Vector3 dir = collision.collider.transform.position - transform.position;
        //dir.y = 0;
        //if (rb != null)
        //{
        //    rb.AddForce(dir * pushForce);
        //}
    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
