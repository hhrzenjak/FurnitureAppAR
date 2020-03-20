using UnityEngine;

public class Scale2 : MonoBehaviour
{
    float startVector;
    float speed = 2.0f;

    public float initialFingersDistance;
    public Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 maxScale = new Vector3(4f, 4f, 4f);


    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(1);

            if (touch.phase == TouchPhase.Began)
            {
                initialFingersDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            else
            {
                float currentFingersDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

                float scaleFactor = currentFingersDistance / initialFingersDistance;
                Vector3 newScale = gameObject.transform.localScale * scaleFactor;

                if (newScale.x < 0.1)
                {
                    newScale = minScale;
                }
                else if (newScale.x > 4)
                {
                    newScale = maxScale;
                }

                gameObject.transform.localScale = Vector3.Lerp(transform.localScale, newScale, speed * Time.deltaTime);
            }
        }
    }
    
}