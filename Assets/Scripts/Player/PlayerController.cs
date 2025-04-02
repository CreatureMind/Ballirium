using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Public Variables
    public float moveForce;
    public float jumpForce;
    public float knockbackForce;
    public float flySpeed = 10f; // Speed for flying mode
    public int RestartPoint;
    
    public GameObject spawnPoint;
    public GameObject lastCheckPoint;

    public TMP_Text MaterialName;
    public Vector3 MaterialNameOffset = new Vector3(0, 0, 0);
    public GameObject StarPanel;

    public PlayerMaterialScriptableObject[] materials = null;

    // Private Variables
    private UnityEvent starPickUpEvent;
    private PickupStars _pickupStars;
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private bool _isGrounded;
    private float _maxVelocity;
    private int _firstMaterial = 0;
    private bool _canJump;
    private bool _isFlying = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();

        if (spawnPoint != null)
            _rigidbody.MovePosition(spawnPoint.transform.position);
        else
            Debug.LogError("Spawn point not set");

        InitializePlayerMaterial(materials, _firstMaterial);
        _pickupStars = StarPanel.GetComponent<PickupStars>();

        if (starPickUpEvent == null)
        {
            starPickUpEvent = new UnityEvent();
        }
        starPickUpEvent.AddListener(_pickupStars.StarControllerEvent);
    }

    public void InitializePlayerMaterial(PlayerMaterialScriptableObject[] currentMaterial, int index)
    {
        name = currentMaterial[index].name;
        MaterialName.gameObject.SetActive(true);
        MaterialName.text = currentMaterial[index].name;
        MaterialName.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position) + MaterialNameOffset;
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

    void FixedUpdate()
    {
        if (!_isFlying)
        {
            if (Input.GetKey(KeyCode.W)) _rigidbody.AddForce(Camera.main.transform.forward * moveForce, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.S)) _rigidbody.AddForce(-Camera.main.transform.forward * moveForce, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.D)) _rigidbody.AddForce(Camera.main.transform.right * moveForce, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.A)) _rigidbody.AddForce(-Camera.main.transform.right * moveForce, ForceMode.Acceleration);

            if (_rigidbody.velocity.magnitude > _maxVelocity)
            {
                _rigidbody.AddForce(-_rigidbody.velocity * _rigidbody.velocity.magnitude / _maxVelocity, ForceMode.Force);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
        {
            ToggleFlyMode();
        }

        if (_isFlying)
        {
            Vector3 moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) moveDirection += Camera.main.transform.forward;
            if (Input.GetKey(KeyCode.S)) moveDirection -= Camera.main.transform.forward;
            if (Input.GetKey(KeyCode.D)) moveDirection += Camera.main.transform.right;
            if (Input.GetKey(KeyCode.A)) moveDirection -= Camera.main.transform.right;
            if (Input.GetKey(KeyCode.Space)) moveDirection += Vector3.up;
            if (Input.GetKey(KeyCode.E)) moveDirection -= Vector3.up;

            transform.position += moveDirection * flySpeed * Time.deltaTime;
        }
        else
        {
            if (transform.position.y < RestartPoint)
            {
                Respawn();
            }
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _canJump)
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                InitializePlayerMaterial(materials, _firstMaterial = (_firstMaterial + 1) % 3);
            }
        }
    }

    private void ToggleFlyMode()
    {
        _isFlying = !_isFlying;
        _rigidbody.useGravity = !_isFlying;
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("StoneBall"))
        {
            _rigidbody.AddForce((transform.position - collision.transform.position).normalized * knockbackForce, ForceMode.Impulse);
        }
        _isGrounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn")) Respawn();
        if (other.CompareTag("CheckPoint"))
        {
            Transform childTransform = other.transform.GetChild(0);
            lastCheckPoint = childTransform.gameObject;
            Destroy(other.GetComponent<SphereCollider>());
        }
        if (other.CompareTag("Star"))
        {
            starPickUpEvent.Invoke();
            Destroy(other.gameObject);
        }
    }

    void Respawn()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.MovePosition(lastCheckPoint != null ? lastCheckPoint.transform.position : spawnPoint.transform.position);
    }
}