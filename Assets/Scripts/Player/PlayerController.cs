using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    //publics
    public float moveForce;
    public float jumpForce;
    
    public PlayerMaterialScriptableObject[] materials;

    //privates
    private Rigidbody _rigidbody;
    private bool _isGrounded;
    private const float MaxVelocity = 3f;
    [SerializeField] private int firstMaterial = 0;
    [SerializeField] private bool _canJump;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        InitializeMaterial(materials, firstMaterial);
    }

    public void InitializeMaterial(PlayerMaterialScriptableObject[] currentMaterial, int index)
    {
        name = currentMaterial[index].name;
        moveForce = currentMaterial[index].moveForce;
        jumpForce = currentMaterial[index].jumpForce;
        _rigidbody.mass = currentMaterial[index].mass;
        _rigidbody.drag = currentMaterial[index].drag;
        _rigidbody.angularDrag = currentMaterial[index].angularDrag;
        _rigidbody.isKinematic = currentMaterial[index].isKinematic;
        _rigidbody.useGravity = currentMaterial[index].useGravity;
        _canJump = currentMaterial[index].canJump;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(Vector3.forward * moveForce, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(Vector3.back * moveForce, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddForce(Vector3.right * moveForce, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddForce(Vector3.left * moveForce, ForceMode.Acceleration);
        }

        Debug.Log(_rigidbody.velocity.magnitude);

        if (_rigidbody.velocity.magnitude > MaxVelocity)
        {
            _rigidbody.AddForce(-_rigidbody.velocity * _rigidbody.velocity.magnitude / MaxVelocity, ForceMode.Force);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _canJump)
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
