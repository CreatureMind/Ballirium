using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WoodMaterial : MonoBehaviour
{
    
    // privates
    private bool _isGrounded;
    // publics
    public float force = 1.5f;
    public float jumpForce = 3.5f;
    public Rigidbody _rigidbody;
    public float _maxVelocity = 4f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
