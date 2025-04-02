using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
    public float lifetime;
    public float speed;
    public float rotationSpeed = 200f;
    public Vector3 moveDirection; // Default forward

    private Rigidbody _rigidbody;
    private int _destroyPoint = 5;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
    }

    void Update()
    {
        if (transform.position.y < _destroyPoint)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.velocity = moveDirection.normalized * speed + new Vector3(0, _rigidbody.velocity.y, 0);

        //transform.Rotate(moveDirection.normalized * rotationSpeed * Time.fixedDeltaTime);
    }
}
