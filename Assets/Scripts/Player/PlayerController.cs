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
    
    public PlayerMaterialScriptableObject[] materials = null;

    //privates
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private bool _isGrounded;
    private float _maxVelocity;
    private int firstMaterial = 0;
    private bool _canJump;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        InitializePlayerMaterial(materials, firstMaterial);
    }

    public void InitializePlayerMaterial(PlayerMaterialScriptableObject[] currentMaterial, int index)
    {
        name = currentMaterial[index].name;
        moveForce = currentMaterial[index].moveForce;
        jumpForce = currentMaterial[index].jumpForce;
        _maxVelocity = currentMaterial[index].maxVelocity;
        _rigidbody.mass = currentMaterial[index].mass;
        _rigidbody.drag = currentMaterial[index].drag;
        _rigidbody.angularDrag = currentMaterial[index].angularDrag;
        _renderer.material = currentMaterial[index].material;
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

        if (_rigidbody.velocity.magnitude > _maxVelocity)
        {
            _rigidbody.AddForce(-_rigidbody.velocity * _rigidbody.velocity.magnitude / _maxVelocity, ForceMode.Force);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _canJump)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            InitializePlayerMaterial(materials, firstMaterial == 0 ? firstMaterial = 1 : firstMaterial = 0);
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
