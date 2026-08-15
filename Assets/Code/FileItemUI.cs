using UnityEngine;
using TMPro;

public class FileItemUI : MonoBehaviour
{

    public TextMeshProUGUI FileNameText;
    public TextMeshProUGUI FileSizeText;
    public TextMeshProUGUI ExtensionText;
    private FileItem CurrentFile;
    private BasketManager Basket;
    private SystemManager FileSystemManager;

    private string FormatFileSize(long bytes)
    {
        if (bytes < 1024)
            return bytes + " B";

        if (bytes < 1024 * 1024)
            return (bytes / 1024f).ToString("0.0") + " KB";

        if (bytes < 1024L * 1024 * 1024)
            return (bytes / (1024f * 1024f)).ToString("0.0") + " MB";

        return (bytes / (1024f * 1024f * 1024f)).ToString("0.0") + " GB";
    }

    public void SetBasket(BasketManager basket)
    {
        Basket = basket;
    }

    public void SetSystemManager(SystemManager fileSystemManager)
    {
        FileSystemManager = fileSystemManager;
    }

    public void AddFileItem(FileItem file)
    {
        CurrentFile = file;
        string name = file.Name;
        string ext = file.Extension;
        FileNameText.text = $"{name}{ext}";
        FileSizeText.text = FormatFileSize(file.SizeBytes);
        ExtensionText.text = ext;
    }

    public void OpenFileItem()
    {
        Application.OpenURL("file://" + CurrentFile.FilePath);
    }

    public void RemoveFileItem()
    {
        Basket.RemFile(CurrentFile);
        Destroy(gameObject);
    }

    public void TrashFileItem()
    {
        if (FileSystemManager.TrashFile(CurrentFile.FilePath))
        {
            RemoveFileItem();
        }
    }
}
