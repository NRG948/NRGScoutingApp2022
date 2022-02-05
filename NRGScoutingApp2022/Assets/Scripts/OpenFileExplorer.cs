using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OpenFileExplorer : MonoBehaviour
{
    string filePath;

    
    public void openFileExplorer()
    {
        filePath = EditorUtility.OpenFilePanel("Open CSV (.csv)", "", "csv");
    }
}
