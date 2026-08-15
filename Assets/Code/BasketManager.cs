using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BasketManager : MonoBehaviour
{
    public SpriteRenderer BasketSprite;
    public Sprite EmptyBasketSprite;
    public Sprite FullBasketSprite;


    public List<FileItem> Files = new List<FileItem>();
    public GameObject FileItemPrefab;
    public Transform Content;

    public SystemManager FileSystemManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void AddFile(FileItem file)
    {
        string path = file.FilePath;
        if (!Files.Any(f => f.FilePath == path))
        {
            Files.Add(file);

            GameObject newFile = Instantiate(FileItemPrefab, Content);
            FileItemUI newFileUI = newFile.GetComponent<FileItemUI>();
            newFileUI.SetBasket(this);
            newFileUI.SetSystemManager(FileSystemManager);
            newFileUI.AddFileItem(file);

            UpdateBasketSprite();
        }

    }

    public void RemFile(FileItem file)
    {
        bool removed = Files.Remove(file);
        if (!removed)
        {
            //error handling
        }
        UpdateBasketSprite();
    }

    private void UpdateBasketSprite()
    {
        if (Files.Count == 0)
        {
            BasketSprite.sprite = EmptyBasketSprite;
        }
        else
        {
            BasketSprite.sprite = FullBasketSprite;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
