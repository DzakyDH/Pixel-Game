using UnityEngine;
using UnityEngine.UI;

public class HealtBarUI : MonoBehaviour
{
    public Image fillImage;

    public void SetHealth(float current, float max)
    {
        float fill = current / max;
        fillImage.fillAmount = fill;
    }    
}
