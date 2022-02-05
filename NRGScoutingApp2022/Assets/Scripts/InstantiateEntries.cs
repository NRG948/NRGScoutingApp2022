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

}