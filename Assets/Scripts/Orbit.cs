using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;
    enum MoveType { Orbit, Wiggle }
    [SerializeField] MoveType type;

    void FixedUpdate()
    {
        switch (type)
        {
            case MoveType.Orbit:
                transform.Rotate(Vector3.forward, speed);
                break;
            case MoveType.Wiggle:
                transform.Rotate(Vector3.forward, 0.1f * Mathf.Sin(10 * speed * Time.time));
                break;
        }
    }
}
