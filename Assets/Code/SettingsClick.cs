using UnityEngine;

public class SettingsClick : MonoBehaviour
{
    public SettingsManager setting;

    void OnMouseDown()
    {
        setting.OpenSettings();
    }
}