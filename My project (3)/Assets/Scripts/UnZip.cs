using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Compression;

public class UnZip : MonoBehaviour
{
    public UnZip()
    {
        string startPath = @"C:\Users\fired\OneDrive\Desktop\TestObj";
        string zipPath = @"C: \Users\fired\OneDrive\Desktop\TestObj\TEST 8.zip";
        string extractPath = @"C:\Users\fired\OneDrive\Desktop\TestObj\FakeAssetFolder";

        ZipFile.CreateFromDirectory(startPath, zipPath);

        ZipFile.ExtractToDirectory(zipPath, extractPath);
    }
}


