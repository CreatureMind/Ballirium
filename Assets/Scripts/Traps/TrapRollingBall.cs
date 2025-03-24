using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRollingBall : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject ball;
    public float intervals;
    public float moveForce;
    public int direction;

    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (intervals > 0)
        {
            intervals -= Time.deltaTime;
        }
        else
        {
            intervals = 10;
            var newBall = Instantiate(ball, spawnPoint.transform.position, Quaternion.identity, this.transform);
            _rigidbody = newBall.GetComponent<Rigidbody>();
            _rigidbody.AddForce(Camera.main.transform.forward * moveForce, ForceMode.Impulse);
        }
    }
}
