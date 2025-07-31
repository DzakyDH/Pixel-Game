using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CastleClose : MonoBehaviour
{
    public GameObject targetPanel;
    public RectTransform imageContent;

    private void Update()
    {
      if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 MousePos = Mouse.current.position.ReadValue();
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, MousePos, null, out localPoint);
            if (!RectTransformUtility.RectangleContainsScreenPoint(imageContent, MousePos))
            {
                targetPanel.SetActive(false);
            }
        }
    }
}
