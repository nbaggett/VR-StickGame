using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    enum state {moveA, moveB};
    public Transform positionA;
    public Transform positionB;

    public Vector3 newPosition;

    state currentState;
    public float smooth;

    public float resetTime;

    public Vector3 currentVelocity = Vector3.zero;
    private Vector3 lastFramePosition;

    private void Awake()
    {
        lastFramePosition = transform.position;
        currentState = state.moveA;
        ChangeTarget();
    }

    void FixedUpdate()
    {
        currentVelocity.x = (transform.position.x - lastFramePosition.x) / Time.deltaTime;
        currentVelocity.y = (transform.position.y - lastFramePosition.y) / Time.deltaTime;
        currentVelocity.z = (transform.position.z - lastFramePosition.z) / Time.deltaTime;
        lastFramePosition = transform.position;

        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, smooth * Time.deltaTime);
    }

    private void ChangeTarget()
    {
        if (currentState == state.moveA)
        {
            currentState = state.moveB;
            newPosition = positionB.position;
        }

        else
        {
            currentState = state.moveA;
            newPosition = positionA.position;
        }

        Invoke("ChangeTarget", resetTime);
    }
}
