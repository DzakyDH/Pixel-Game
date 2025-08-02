using UnityEngine;
using UnityEngine.InputSystem;
public class PanelBuidingTouch : MonoBehaviour
{
    public GameObject panelToOpen;
    private static GameObject currentLyOpenpanel = null;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(false);
        }
    }
    

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                   if (currentLyOpenpanel != null && currentLyOpenpanel != panelToOpen)
                   {
                   currentLyOpenpanel.SetActive(false);
                   }

                   panelToOpen.SetActive(true);
                   currentLyOpenpanel = panelToOpen;
                   return;
            }
            if (panelToOpen.activeSelf && !IsPointerOverUIObject(mousePos))
            {
                    panelToOpen.SetActive(false);
                    currentLyOpenpanel = null;
            }
        }
    }
    private bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        RectTransform rect = panelToOpen.GetComponent<RectTransform>();
        Canvas canvas = panelToOpen.GetComponentInParent<Canvas>();
        if (rect == null || canvas == null) return false;


        Vector2 localPoint;
        Camera uiCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        return RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPosition, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera, out localPoint)
        && rect.rect.Contains(localPoint);

    }
}
