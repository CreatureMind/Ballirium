using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoosballPole : MonoBehaviour
{
    public Transform pivot; // The pivot for kicking movement
    public float minMoveSpeed = 2f;
    public float maxMoveSpeed = 5f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;

    private float moveSpeed;
    private int moveDirection = 1; // 1 = right, -1 = left
    private bool canMove = true;

    private void Start()
    {
        StartCoroutine(ChangeMovement());
        StartCoroutine(KickBall());
    }

    private void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector3.forward * moveDirection * moveSpeed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall")) // Make sure to tag your walls as "Wall"
        {
            moveDirection *= -1; // Change direction when hitting a wall
        }
    }
    
    private IEnumerator ChangeMovement()
    {
        while (true)
        {
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            canMove = true;
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            canMove = false;
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

    private IEnumerator KickBall()
    {
        while (true)
        {
            // Choose a kick angle (-90, 0, or 90)
            float randomRotation = Random.Range(0, 3) * 90 - 90;
            float duration = 0.05f; // Smooth transition
            float elapsedTime = 0f;
            float startRotation = pivot.localRotation.eulerAngles.z;
            float endRotation = randomRotation;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float zRotation = Mathf.LerpAngle(startRotation, endRotation, elapsedTime / duration);
                pivot.localRotation = Quaternion.Euler(0, 0, zRotation);
                yield return null;
            }

            // Hold the kick position briefly
            //yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));

            // Return to neutral (0 degrees)
            float resetDuration = 0.2f;
            float resetElapsedTime = 0f;
            float resetStartRotation = pivot.localRotation.eulerAngles.z;
            float resetEndRotation = 0;

            while (resetElapsedTime < resetDuration)
            {
                resetElapsedTime += Time.deltaTime;
                float zRotation = Mathf.LerpAngle(resetStartRotation, resetEndRotation, resetElapsedTime / resetDuration);
                pivot.localRotation = Quaternion.Euler(0, 0, zRotation);
                yield return null;
            }

            // Wait before the next kick
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

}