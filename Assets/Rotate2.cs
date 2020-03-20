using UnityEngine;

public class Rotate2 : MonoBehaviour
{
    Vector2 startDistance;
    float minAngle = 20;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(1);
            if (touch.phase == TouchPhase.Began)
            {
                startDistance = Input.GetTouch(1).position - Input.GetTouch(0).position;
            }
            else
            {
                Vector2 currentDistance = Input.GetTouch(1).position - Input.GetTouch(0).position;
                float rotationAngle = Vector2.Angle( startDistance, currentDistance);
                Vector3 angle = Vector3.Cross( startDistance, currentDistance);
                //check to which side to turn
                if (rotationAngle > minAngle)
                {
                    if (angle.z > 0)
                    {
                        gameObject.transform.eulerAngles = new Vector3(0, -rotationAngle, 0);
                    }
                    else if (angle.z < 0)
                    {
                        gameObject.transform.eulerAngles = new Vector3(0, rotationAngle, 0);
                    }
                }
            }
        }
    }
}
