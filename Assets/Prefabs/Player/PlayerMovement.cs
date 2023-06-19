using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    PlayerReferences p;

    public float movementSpeed;

    void FixedUpdate()
    {
        p.rb.AddForce(SceneObjects.Singleton.joystickMovement.InputDirection.normalized * movementSpeed * 100f, ForceMode.Acceleration);
    }

    void Update()
    {
        p.rotationTransform.LookAt(p.rotationTransform.position + SceneObjects.Singleton.joystickRotation.InputDirection);
    }
}