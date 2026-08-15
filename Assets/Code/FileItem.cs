using UnityEngine;
using System.IO;

[System.Serializable]
public class FileItem
{
    public string Name;
    public string Extension;
    public string FilePath;
    //public string ParentFolder; optional, think about this
    public long SizeBytes; //1 kb, 1mb, etc
    public int SizeCategory; //1 = tiny, 2 = small, 3 = med, 4 = big, 5 = hyuge

    //should these be public?
    public FileItem()
    {
        Name = "N/A";
        Extension = "N/A";
        FilePath = "N/A";
        SizeBytes = 0;
        SizeCategory = 0;
    }

    public FileItem(string path)
    {
        string name = Path.GetFileNameWithoutExtension(path);
        string ext = Path.GetExtension(path);
        if (name == null || ext == null)
        {
            Debug.Log("invalid file path!");
            return; // or something idk, but i think i should check this
        }
        Name = name;
        Extension = ext;
        FilePath = path;

        var fi = new FileInfo(path);
        long fi_size = fi.Length;
        SizeBytes = fi_size;
        if (fi_size < 1048576)
        { // 1 mb
            SizeCategory = 1;
        }
        else if (fi_size < 10485760)
        { //10 mb
            SizeCategory = 2;
        }
        else if (fi_size < 104857600)
        { //100 mb
            SizeCategory = 3;
        }
        else if (fi_size < 1073741824)
        {//1gb
            SizeCategory = 4;
        }
        else
        {//above 1gb
            SizeCategory = 5;
        }
    }
}
