using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeScrool : MonoBehaviour
{
    public float swipeSpeed = 0.5f;
    public float minX = -10f;
    public float maxX = 10f;

    private Vector2 previousPosition;
    private float targetX;
    private bool isDragging = false;


    void Start()
    {
        targetX = transform.position.x;
    }
    void Update()
    {
        Vector2 currentPosition = Vector2.zero;
        bool inputActive = false;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            currentPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            inputActive = true;
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            currentPosition = Mouse.current.position.ReadValue();
            inputActive = true;
        }
        if (inputActive)
        {
            if (!isDragging)
            {
                previousPosition = currentPosition;
                isDragging = true;
            }

            else
            {
                float delta = currentPosition.x - previousPosition.x;
                targetX = Mathf.Clamp(targetX - delta * swipeSpeed, minX, maxX);
                previousPosition = currentPosition;
            }
        }
        else
        {
            isDragging = false;
        }
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, -2f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
    }
}




    