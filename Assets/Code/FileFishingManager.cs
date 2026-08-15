using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Linq;

public class FileFishingManager : MonoBehaviour
{
  public CatController cat;
  public BasketManager basket;
  private string testPath;
  private string[] testPaths;
  // private string[] folderPaths;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

    //sum path shit
    string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    //for final
    // string downloadsPath = Path.Combine(userPath, "Downloads");
    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
    //folderPaths = new[] { downloadsPath, desktopPath };

    testPath = Path.Combine(desktopPath, "TestFolder");
    Debug.Log("hi! file path is " + testPath);
    testPaths = new[] { testPath }; ;

    //start fishing mofo
    StartCoroutine(FishingLoop());
  }



  IEnumerator FishingLoop()
  {
    while (true)
    {
      //wait period
      Debug.Log("fishing...");
      yield return new WaitForSeconds(10f); //make this random in future

      FileItem caughtFile = RetrieveRandomFile();
      if (caughtFile != null && (basket.Files).Count < 20)
      {
        cat.StartFishing();
        Debug.Log("found file, starting fish");
        int sizeCat = caughtFile.SizeCategory;
        if (sizeCat == 1)
        {
          Debug.Log("tiny file");
          yield return new WaitForSeconds(1f);
        }
        else if (sizeCat == 2)
        {
          Debug.Log("small file");
          yield return new WaitForSeconds(3f);
        }
        else if (sizeCat == 3)
        {
          Debug.Log("medium file");
          yield return new WaitForSeconds(7f);
        }
        else if (sizeCat == 4)
        {
          Debug.Log("big file");
          yield return new WaitForSeconds(11f);
        }
        else
        {
          Debug.Log("huge file");
          yield return new WaitForSeconds(15f);

        }
        //future basket update for anim
        Debug.Log("caught file");
        cat.CatchFish();

        Debug.Log("adding file to basket");
        basket.AddFile(caughtFile);
      }
    }
  }


  FileItem RetrieveRandomFile()
  {
    int randFold = UnityEngine.Random.Range(0, testPaths.Length); //final: int randFold = UnityEngine.Random.Range(0, folderPaths.Length);
    var allFiles = Directory.EnumerateFiles(testPaths[randFold]); //final: var allFiles = Directory.EnumerateFiles(folderPaths[randFold]);

    string[] allowedExtensions =
    {
      ".png", ".jpg", ".jpeg", ".pdf", ".html",
      ".txt", ".mov", ".mp4", ".mp3", ".m4a",
      ".webp", ".gif", ".dmg", ".epub", ".zip",
      ".ai", ".psd", ".tar", ".tex", ".csv",
      ".ics", ".procreate", ".pkf", ".pkg",
      ".tif", ".svg", ".wav", ".docx", ".blend",
    };
    string[] caughtFiles = allFiles.Where(file => allowedExtensions.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase)).ToArray();
    int numFiles = caughtFiles.Length;

    if (numFiles == 0)
    {
      Debug.Log("No catchable files found.");
      return null;
    }
    int randIdx = UnityEngine.Random.Range(0, numFiles);
    string caught = caughtFiles[randIdx];
    Debug.Log("caught file " + caught);

    return new FileItem(caught);

  }

  // Update is called once per frame
  void Update()
  {

  }
}
