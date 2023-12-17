using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RampDog : MonoBehaviour
{
    Rigidbody rb => GetComponent<Rigidbody>();
    public float speed;
    public float minBarkingDist, minPlayDist, timeBetweenBarks;
    public float barkTimer;
    public Transform player, ball;
    public UnityEvent Bark, OnPlay;
    enum DogState {Guarding, Playing}
    DogState dogState;
    // Start is called before the first frame update

    public Animator animator;
    void Start()
    {
        dogState = DogState.Guarding;
        print(animator);
        
    }

   

    // Update is called once per frame
    void Update()
    {
        animator = GetComponentInChildren<Animator>();

        Look();
        if (dogState == DogState.Guarding)
        {
            Guard();
            //if (animator)
           
        }
        else if (dogState == DogState.Playing)
        {
            Play();
           // if (animator)
           
        }
    }
    void Look()
    {
        Transform target = null;
        if (dogState == DogState.Guarding)
        {
            target = player;
        }
        else if (dogState == DogState.Playing)
        {
            target = ball;

            { animator.SetBool("player", false); }
        }
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

       
    }
    void Guard()
    {
        if (Vector3.Distance(transform.position,player.transform.position) < minBarkingDist && barkTimer <= 0)
        {
            Bark.Invoke();
            barkTimer = timeBetweenBarks;

            { animator.SetBool("player", true); }
        }
        if (barkTimer > 0)
        {
            barkTimer -= Time.deltaTime;
        }
        if (Vector3.Distance(transform.position,ball.transform.position) < minPlayDist)
        {
            OnPlay.Invoke();
            dogState = DogState.Playing;

        }

        
    }

    void Play()
    {
        rb.AddForce((ball.transform.position - transform.position)* speed);
    }
}
