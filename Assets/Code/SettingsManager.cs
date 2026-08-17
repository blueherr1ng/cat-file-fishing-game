using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject SettingsUI;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenSettings()
    {
        SettingsUI.SetActive(true);
    }
    public void CloseSettings()
    {
        SettingsUI.SetActive(false);
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
