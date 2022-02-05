using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ListEntry : MonoBehaviour
{

    [HideInInspector] public Color defaultColor;
    [HideInInspector] public Color selectedColor;
    [HideInInspector] public int defaultColorIndex;
    [HideInInspector] public int selectedColorIndex;

    public void Press(){
        foreach(ListEntry listEntry in FindObjectsOfType<ListEntry>()){
            if(listEntry == this){
                listEntry.GetComponent<ColorComponent>().color = selectedColor;
            }
            else{
                listEntry.GetComponent<ColorComponent>().color = defaultColor;
            }
        }
    }
}

[CustomEditor(typeof(ListEntry))]
public class EntryEditor : Editor{

    AppManager appManager;

    private void OnEnable() {
        appManager = FindObjectOfType<AppManager>();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ListEntry listEntry = (ListEntry)target;
        Undo.RecordObject(listEntry, "List Entries");

        int dafaultPreviousIndex = listEntry.defaultColorIndex;
        listEntry.defaultColorIndex = 
            EditorGUILayout.Popup("Default Color", listEntry.defaultColorIndex, appManager.graphics.colorData.GetColorNames(
            appManager.graphics.colorData.GetTemplate(appManager.graphics.selectedTemplate)));

        listEntry.defaultColor = 
            appManager.graphics.colorData.GetTemplate
            (appManager.graphics.selectedTemplate)[listEntry.defaultColorIndex].color;

        int selectedPreviousIndex = listEntry.selectedColorIndex;
        listEntry.selectedColorIndex = 
            EditorGUILayout.Popup("Selected Color", listEntry.selectedColorIndex, appManager.graphics.colorData.GetColorNames(
            appManager.graphics.colorData.GetTemplate(appManager.graphics.selectedTemplate)));

        listEntry.selectedColor = 
            appManager.graphics.colorData.GetTemplate
            (appManager.graphics.selectedTemplate)[listEntry.selectedColorIndex].color;
    }
}

