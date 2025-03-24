using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRollingBall : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject ballPrefab;
    public float interval;
    public float moveSpeed;
    public float selfDestructTime;
    public Vector3 direction;
    public float StartDelay;

    private float _currentInterval;

    private void Start()
    {
        _currentInterval = StartDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentInterval > 0)
        {
            _currentInterval -= Time.deltaTime;
        }
        else
        {
            SpawnBall();
            _currentInterval = interval;
        }
    }

    void SpawnBall()
    {
        GameObject newBall = Instantiate(ballPrefab, spawnPoint.transform.position, Quaternion.identity);
        
        RollingBall newBallScript = newBall.GetComponent<RollingBall>();

        if(newBallScript != null)
        {
            newBallScript.moveDirection = direction;
            newBallScript.speed = moveSpeed;
        }
    }
}
