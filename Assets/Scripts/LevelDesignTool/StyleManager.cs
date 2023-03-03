using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleManager : MonoBehaviour
{
    public ButtonStyle [] buttonStyles;
}
[System.Serializable]
public struct ButtonStyle
{
    public Texture2D icon;
    public string buttonText;
    [HideInInspector] public GUIStyle nodeStyle;
    public GameObject prefab;
}
