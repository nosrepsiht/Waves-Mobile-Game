using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInOut : MonoBehaviour
{
    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier, posY;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    [SerializeField]
    float zoomModifierSpeed = 0.1f;

    void Start()
    {
        posY = 10f;
    }

    void Update()
    {
        if (Input.touchCount == 2 && SceneObjects.Singleton.joystickMovement.InputDirection.magnitude == 0 && SceneObjects.Singleton.joystickRotation.InputDirection.magnitude == 0)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
                posY += zoomModifier;
            if (touchesPrevPosDifference < touchesCurPosDifference)
                posY -= zoomModifier;

        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(posY, 3f, 90f), transform.position.z);
    }
}
