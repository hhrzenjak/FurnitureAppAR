using UnityEngine;
using UnityEngine.EventSystems;

public class Translate : MonoBehaviour {

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {

                // Screen position of the transform
                var screenPoint = Camera.main.WorldToScreenPoint(transform.position);

                // Add the deltaPosition
                screenPoint += (Vector3)touch.deltaPosition;

                // Convert back to world space
                transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
                
                
            }

        }
    }
}

