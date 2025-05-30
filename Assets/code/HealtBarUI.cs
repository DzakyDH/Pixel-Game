using UnityEngine;
using UnityEngine.UI;

public class HealtBarUI : MonoBehaviour
{
    public Image fillImage;
    public RectTransform rectTransform;

    public void SetHealth(float current, float max)
    {
        float fill = current / max;
        fillImage.fillAmount = fill;
    }
    public void UpdatePosition(Vector3 worldPosition)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        rectTransform.position = screenPos;
    }

}
