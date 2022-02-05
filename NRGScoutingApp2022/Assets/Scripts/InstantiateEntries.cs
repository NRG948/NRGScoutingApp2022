using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEditor;

[ExecuteInEditMode]
public class InstantiateEntries : MonoBehaviour
{
    [Serializable]
    public struct Entry{
        public string teamName;
        public int matchNumber;
        public TeamIdentifier teamIdentifier;
    }

    public enum TeamIdentifier{
        Red_1,
        Red_2,
        Red_3,
        Blue_1,
        Blue_2,
        Blue_3
    }

    public List<Entry> data;
    public GameObject entryObject;
    public Transform entryParent;
    List<GameObject> entries;

    [HideInInspector] public Color defaultColor;
    [HideInInspector] public Color selectedColor;
    [HideInInspector] public int defaultColorIndex;
    [HideInInspector] public int selectedColorIndex;

    // Start is called before the first frame update
    void Start()
    {       
        entries = new List<GameObject>();
        data.ForEach(x => InstantiateEntry(EntryToString(x)));
    }

    void InstantiateEntry(string text){
        GameObject entry = Instantiate(entryObject, entryParent);
        entry.GetComponentInChildren<TextMeshProUGUI>().text = text;
        entries.Add(entry);
    }

    string EntryToString(Entry entry){
        return "Match " + entry.matchNumber + "\n" + entry.teamName + " - " + entry.teamIdentifier.ToString().Replace("_", " ");
    }

    public void Search(string phrase){
        entries.ForEach(x => Destroy(x));
        entries = new List<GameObject>();
        foreach(Entry entry in data){
            string stringEntry = EntryToString(entry);
            if(stringEntry.ToLower().Contains(phrase.ToLower())){
                InstantiateEntry(stringEntry);
            }
        }
    }

    public void SelectEntry(int entry){
        for(int i = 0; i < transform.childCount; i++){
            ColorComponent colorComponent = transform.GetChild(i).GetComponent<ColorComponent>();
            if(i == entry){
                colorComponent.color = selectedColor;
                colorComponent.UpdateColor();
            }
            else{
                colorComponent.color = defaultColor;
                colorComponent.UpdateColor();
            }
        }
    }
}

[CustomEditor(typeof(InstantiateEntries))]
public class EntryEditor : Editor{

    AppManager appManager;

    private void OnEnable() {
        appManager = FindObjectOfType<AppManager>();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        InstantiateEntries instantiateEntries = (InstantiateEntries)target;

        int dafaultPreviousIndex = instantiateEntries.defaultColorIndex;
        instantiateEntries.defaultColorIndex = 
            EditorGUILayout.Popup("Color", instantiateEntries.defaultColorIndex, appManager.graphics.colorData.GetColorNames(
            appManager.graphics.colorData.GetTemplate(appManager.graphics.selectedTemplate)));

        instantiateEntries.defaultColor = 
            appManager.graphics.colorData.GetTemplate
            (appManager.graphics.selectedTemplate)[instantiateEntries.defaultColorIndex].color;

        int selectedPreviousIndex = instantiateEntries.selectedColorIndex;
        instantiateEntries.selectedColorIndex = 
            EditorGUILayout.Popup("Color", instantiateEntries.selectedColorIndex, appManager.graphics.colorData.GetColorNames(
            appManager.graphics.colorData.GetTemplate(appManager.graphics.selectedTemplate)));

        instantiateEntries.selectedColor = 
            appManager.graphics.colorData.GetTemplate
            (appManager.graphics.selectedTemplate)[instantiateEntries.selectedColorIndex].color;
    }
}
