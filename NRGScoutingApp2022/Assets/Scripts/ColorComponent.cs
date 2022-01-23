using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode]
public class ColorComponent : MonoBehaviour
{
    Image image;
    public int index;
    public Color color;

    private void OnEnable() {
        image = GetComponent<Image>();
    }

    private void OnDrawGizmos() {
        image.color = color;
    }

    public void UpdateColor(){
        image.color = color;
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

        if(previousIndex != colorComponent.index) {
            colorComponent.UpdateColor();
            Undo.PerformUndo();
            Undo.PerformRedo();
        }
    }
}
