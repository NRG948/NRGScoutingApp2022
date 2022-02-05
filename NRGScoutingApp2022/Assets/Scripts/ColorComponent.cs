using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

[ExecuteInEditMode]
public class ColorComponent : MonoBehaviour
{

    public enum Type{
        Image,
        Text
    }
    public Type type = Type.Image;

    Image image;
    TextMeshProUGUI text;
    public int index;
    public Color color;

    private void OnEnable() {
        switch(type){
            case Type.Image:
                image = GetComponent<Image>();
                break;
            case Type.Text:
                text = GetComponent<TextMeshProUGUI>();
                break;
        }
    }

    private void OnDrawGizmos() {
        UpdateType();
        switch(type){
            case Type.Image:
                image.color = color;
                break;
            case Type.Text:
                text.color = color;
                break;
        }
    }

    public void UpdateColor(){
        switch(type){
            case Type.Image:
                image.color = color;
                break;
            case Type.Text:
                text.color = color;
                break;
        }
    }

    public void UpdateType(){
        switch(type){
            case Type.Image:
                image = GetComponent<Image>();
                break;
            case Type.Text:
                text = GetComponent<TextMeshProUGUI>();
                break;
        }
    }
}

[CustomEditor(typeof(ColorComponent))]
[CanEditMultipleObjects]
public class ColorEditor : Editor
{
    AppManager appManager;

    private void OnEnable() {
        appManager = FindObjectOfType<AppManager>();
    }

    public override void OnInspectorGUI()
    {
        ColorComponent colorComponent = (ColorComponent)target;
        Undo.RecordObject(colorComponent, "Color Component");

        int previousIndex = colorComponent.index;
        colorComponent.index = EditorGUILayout.Popup("Color", colorComponent.index, appManager.graphics.colorData.GetColorNames(
            appManager.graphics.colorData.GetTemplate(appManager.graphics.selectedTemplate)));

        colorComponent.color = 
            appManager.graphics.colorData.GetTemplate(appManager.graphics.selectedTemplate)[colorComponent.index].color;


        colorComponent.type = (ColorComponent.Type)EditorGUILayout.EnumPopup("Type", colorComponent.type);

        if(previousIndex != colorComponent.index) {
            colorComponent.UpdateType();
            colorComponent.UpdateColor();
            Undo.PerformUndo();
            Undo.PerformRedo();
        }
    }
}
