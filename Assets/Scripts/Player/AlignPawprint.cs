using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignPawprint : MonoBehaviour
{
    public PlayerController pc;
    public ParticleSystem ps => GetComponent<ParticleSystem>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ps.enableEmission = pc.rb.velocity.magnitude > 0;
        float rads = Mathf.Atan2(pc.gameObject.transform.forward.z, pc.gameObject.transform.forward.x);
        float degs = rads * Mathf.Rad2Deg;
        ps.startRotation3D = Vector3.up * (-degs - 45);
    }
}
