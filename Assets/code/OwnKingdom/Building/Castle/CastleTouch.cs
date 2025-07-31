using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class CastleTouch : MonoBehaviour
{
    public GameObject panelCastle;
    private Camera mainCamera;

    private void Update()
    {
       if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            
            RaycastHit2D hit = Physics2D.Raycast(worldPoint,Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Castle"))
            {
                panelCastle.SetActive(true);
            }
        }
    }
}
