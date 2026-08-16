using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Linq;

public class FileFishingManager : MonoBehaviour
{
  public CatController cat;
  public BasketManager basket;
  public SystemManager systemManager;


  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    StartCoroutine(FishingLoop());
  }




  IEnumerator FishingLoop()
  {
    while (true)
    {
      //wait period
      Debug.Log("fishing...");
      yield return new WaitForSeconds(10f); //make this random in future

      FileItem caughtFile = systemManager.RetrieveRandomFile();
      if (caughtFile != null && basket.Files.Count < 20)
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


  // Update is called once per frame
  void Update()
  {

  }
}
