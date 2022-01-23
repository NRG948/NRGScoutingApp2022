using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "Data/Color", order = 1)]
public class ColorData : ScriptableObject
{
    [System.Serializable]
    public struct ColorName{
        public string name;
        public Color color;
    }

    [System.Serializable]
    public struct ColorTemplate{
        public string name;
        public ColorName[] colors;
    }

    public ColorTemplate[] colorTemplates;

    public ColorName[] GetTemplate(string name){
        return Array.Find(colorTemplates, template => template.name == name).colors;
    }

    public string[] GetColorNames(ColorName[] colors){
        string[] names = new string[colors.Length];
        for(int i = 0; i < names.Length; i++){
            names[i] = colors[i].name;
        }
        return names;
    }
}
