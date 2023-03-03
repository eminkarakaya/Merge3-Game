using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNode : MonoBehaviour
{
    Rect rect;
    public GUIStyle style;
    public ItemNode(Vector2 pos,float width,float height,GUIStyle defaultStyle)
    {
        rect = new Rect(pos.x,pos.y,width,height);
        style = defaultStyle;
    }
    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }
    public void Draw()
    {
        GUI.Box(rect,"",style);
    }
    public void SetStyle(GUIStyle style)
    {
        this.style = style;
    }
}
