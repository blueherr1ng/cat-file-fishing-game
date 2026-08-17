using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SystemManager : MonoBehaviour
{
    private string[] folderPaths;
    private HashSet<string> inaccessibleFolders = new HashSet<string>();

    private string[] allowedExtensions =
        {
         ".png", ".jpg", ".jpeg", ".pdf", ".html",
         ".txt", ".mov", ".mp4", ".mp3", ".m4a",
          ".webp", ".gif", ".dmg", ".epub", ".zip",
         ".ai", ".psd", ".tar", ".tex", ".csv",
          ".ics", ".procreate", ".pkf", ".pkg",
         ".tif", ".svg", ".wav", ".docx", ".blend",
        };


    void Start()
    {
        // string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string desktopPath = Environment.GetFolderPath(
            Environment.SpecialFolder.DesktopDirectory
        );
        string testPath = Path.Combine(desktopPath, "TestFolder");
        Debug.Log("hi! file path is " + testPath);
        folderPaths = new[] { testPath };
    }


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
            // return WindowsTrashFilePath(filePath);
            return false;
        }
        else if (Application.platform == RuntimePlatform.LinuxPlayer)
        {
            //Linux implementation
            return false;
        }

        Debug.Log("fell through");
        return false;

    }

    public FileItem RetrieveRandomFile()
    {
        int folderIdx = UnityEngine.Random.Range(0, folderPaths.Length);
        string caught = SearchFolder(folderPaths[folderIdx]);
        if (caught == null)
        {
            return null;
        }
        return new FileItem(caught);
    }


    private string SearchFolder(string folderPath)
    {
        Debug.Log("searching " + folderPath);

        if (inaccessibleFolders.Contains(folderPath))
        {
            return null;
        }

        string[] folderFiles;
        string[] folderFolders;

        try
        {
            folderFiles =
                Directory.EnumerateFiles(folderPath)
                .Where(file => allowedExtensions.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                .ToArray();
            folderFolders = Directory.EnumerateDirectories(folderPath).ToArray();
        }
        catch (UnauthorizedAccessException)
        {
            inaccessibleFolders.Add(folderPath);
            return null;
        }
        string[] okFolders = folderFolders.Where(folder => !inaccessibleFolders.Contains(folder)).ToArray();


        int numFiles = folderFiles.Length;
        int numFolders = okFolders.Length;


        if (numFiles + numFolders == 0)
        {
            Debug.Log("No catchable files found.");
            return null;
        }

        int randIdx = UnityEngine.Random.Range(0, numFiles + numFolders);
        if (randIdx < numFiles)
        {
            string chosenFile = folderFiles[randIdx];
            Debug.Log("caught file " + chosenFile);
            return chosenFile;
        }
        else //randIdx >= numFiles
        {
            string[] foldersToSearch = okFolders
             .OrderBy(folder => UnityEngine.Random.value)
             .ToArray();

            foreach (string folder in foldersToSearch)
            {
                string search = SearchFolder(folder);

                if (search != null)
                {
                    return search;
                }
            }

            return null;
        }
    }
}
