using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //publics
    public float force;
    public float jumpForce;

    //privates
    private Rigidbody _rigidbody;
    private bool _isGrounded;
    private float _maxVelocity = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(Vector3.forward * force, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(Vector3.back * force, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddForce(Vector3.right * force, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddForce(Vector3.left * force, ForceMode.Acceleration);
        }

        Debug.Log(_rigidbody.velocity.magnitude);

        if (_rigidbody.velocity.magnitude > _maxVelocity)
        {
            _rigidbody.AddForce(-_rigidbody.velocity * _rigidbody.velocity.magnitude / _maxVelocity, ForceMode.Force);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }

        _isGrounded = true;
    }
}
