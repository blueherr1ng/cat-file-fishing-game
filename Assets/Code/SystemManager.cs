using UnityEngine;
using System.Runtime.InteropServices;

public class SystemManager : MonoBehaviour
{

    //TrashFile returns true:  if a file was successfully 
    //                         moved to trash
    //                  false: otherwise
    [DllImport("__Internal")]
    private static extern bool MacTrashFilePath(string filePath);

    public bool TrashFile(string filePath)
    {
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            return MacTrashFilePath(filePath);
        }

        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            // Windows implementation
        }

        Debug.Log("fell through");
        return false;

    }


}
