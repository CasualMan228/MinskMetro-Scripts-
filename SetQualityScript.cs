using UnityEngine;

public class SetQualityScript : MonoBehaviour
{
    private void Awake()
    {
        int quality_level = PlayerPrefs.GetInt("Quality", 0);
        if (quality_level == 1) //low
        {
            QualitySettings.SetQualityLevel(0);
        }
        else if (quality_level == 2) //medium balanced
        {
            QualitySettings.SetQualityLevel(1);
        }
        else if (quality_level == 3) //high extreme
        {
            QualitySettings.SetQualityLevel(2);
        }
        else
        {
            Debug.Log("Неверное значение уровня графики! Ставлю low");
            QualitySettings.SetQualityLevel(0);
        }
    }
}
