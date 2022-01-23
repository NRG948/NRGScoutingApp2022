using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    [System.Serializable]
    public struct Graphics{
        public string selectedTemplate;
        public ColorData colorData;
    }

    public Graphics graphics;
}
