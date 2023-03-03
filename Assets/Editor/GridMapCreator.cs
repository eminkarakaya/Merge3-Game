using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridMapCreator : EditorWindow
{
    Vector2 offset ;
    Vector2 drag;
    List<List<ItemNode>> nodes;
    List<List<PartScript>> parts;
    GUIStyle empty;
    Vector2 nodePos;
    bool isErasing;
    Rect menuBar;
    GUIStyle currentStyle;
    GameObject map;
    StyleManager styleManager;
    [MenuItem("Window/Grid Map Creator")]


    public static void OpenWindow()
    {
        GridMapCreator window = GetWindow<GridMapCreator>();
        window.titleContent = new GUIContent("Grid Map Creator");
    }    
    void OnEnable()
    {
        SetupStyles();
        SetUpItemNodesAndParts();
        SetUpMap();
    }
    void OnGUI()
    {
        DrawGrid();
        DrawNodes();
        ProcessNodes(Event.current);
        ProcessGrid(Event.current);
        DrawMenuBar();
        if(GUI.changed)
            Repaint();
    }
    void SetUpMap()
    {
        try
        {
            map = GameObject.FindGameObjectWithTag("Map");
        }
        catch (System.Exception)
        {
            
            throw;
        }
        if(map == null)
        {
            map = new GameObject("Map");
            map.tag = "Map";
        }
    }
    void SetupStyles()
    {
        try
        {
            styleManager = GameObject.FindObjectOfType<StyleManager>();
            for (int i = 0; i < styleManager.buttonStyles.Length; i++)
            {
                styleManager.buttonStyles[i].nodeStyle = new GUIStyle();
                styleManager.buttonStyles[i].nodeStyle.normal.background = styleManager.buttonStyles[i].icon;
            }
        }
        catch (System.Exception e)
        {}
        empty = styleManager.buttonStyles[0].nodeStyle;;
        currentStyle = styleManager.buttonStyles[1].nodeStyle;
    } 
    void DrawMenuBar()
    {
        menuBar = new Rect(0,0,position.width,20);
        GUILayout.BeginArea(menuBar,EditorStyles.toolbar);
        GUILayout.BeginHorizontal();
        for (int i = 0; i < styleManager.buttonStyles.Length; i++)
        {
            if(GUILayout.Toggle((currentStyle == styleManager.buttonStyles[i].nodeStyle) ,styleManager.buttonStyles[i].buttonText,EditorStyles.toolbarButton,GUILayout.Width(80)))
            {
                currentStyle = styleManager.buttonStyles[i].nodeStyle;
            }
            
        }
        // if(GUILayout.Toggle((currentStyle == styleManager.buttonStyles[2].nodeStyle),new GUIContent("Club"),EditorStyles.toolbarButton,GUILayout.Width(80)))
        // {
        //     currentStyle = styleManager.buttonStyles[2].nodeStyle;
        // }
        // if(GUILayout.Toggle((currentStyle == styleManager.buttonStyles[3].nodeStyle),new GUIContent("Hearth"),EditorStyles.toolbarButton,GUILayout.Width(80)))
        // {
        //     currentStyle = styleManager.buttonStyles[3].nodeStyle;
        // }
        // if(GUILayout.Toggle((currentStyle == styleManager.buttonStyles[4].nodeStyle),new GUIContent("Spade"),EditorStyles.toolbarButton,GUILayout.Width(80)))
        // {
        //     currentStyle = styleManager.buttonStyles[4].nodeStyle;
        // }
        
        
        
        
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
    void ProcessNodes(Event e)
    {
        int row = (int)((e.mousePosition.x - offset.x)/30);
        int col = (int)((e.mousePosition.y - offset.y)/30);
        if((e.mousePosition.x - offset.x )<0 || (e.mousePosition.x - offset.x)>600||
            (e.mousePosition.y - offset.y) < 0 || (e.mousePosition.y - offset.y) > 300)
        {

        }
        else
        {
            if(e.type == EventType.MouseDown)
            {
                if(nodes[row][col].style.normal.background.name == "9Sliced")
                {
                    isErasing = false;
                    // nodes[row][col].SetStyle(styleManager.buttonStyles[1].nodeStyle);
                    // GUI.changed = true;
                }
                else
                    isErasing = true;
                            
                PaintNodes(row,col);
            }
            if(e.type == EventType.MouseDrag)
            {
                PaintNodes(row,col);
                e.Use();
            }
        }
    }

    void PaintNodes(int row,int col)
    {
        if(isErasing)
        {
            nodes[row][col].SetStyle(empty);
            GUI.changed = true;
        }
        else
        {
            if(parts[row][col] == null)
            {
                nodes[row][col].SetStyle(currentStyle);
                Debug.Log(currentStyle.normal.background.name);
                GameObject g = Instantiate(Resources.Load("Item/"+currentStyle.normal.background.name)) as GameObject;
                g.name = currentStyle.normal.background.name;
                g.transform.position = new Vector3(col*10,0,row * 10);
                g.transform.parent = map.transform;
                parts[row][col] = g.GetComponent<PartScript>();
                parts[row][col].part = g;
                parts[row][col].name = g.name;
                parts[row][col].row = row;
                parts[row][col].col = col;
                parts[row][col].style = currentStyle;
                GUI.changed = true;
            }
        }
    }
    void SetUpItemNodesAndParts()
    {
        nodes = new List<List<ItemNode>>();
        parts = new List<List<PartScript>>();
        for (int i = 0; i < 20; i++)
        {
            nodes.Add(new List<ItemNode>());
            parts.Add(new List<PartScript>());
            for (int j = 0; j < 10; j++)
            {
                nodePos.Set(i*30,j*30);
                nodes[i].Add(new ItemNode(nodePos,30,30,empty));
                parts[i].Add(null);
            }
        }
    }
    void DrawNodes()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                nodes[i][j].Draw();
            }
        }
    }
    void ProcessGrid(Event e)
    {
        drag = Vector2.zero;
        switch (e.type)
        {
            case EventType.MouseDrag:
                if(e.button == 0)
                {
                    OnMouseDrag(e.delta);
                }
                break;
        }
    }
    void OnMouseDrag(Vector2 delta)
    {
        drag = delta;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                nodes[i][j].Drag(delta);
            }
        }
        GUI.changed = true;
    }
    void DrawGrid()
    {
        int widthDivider = Mathf.CeilToInt(position.width/20);
        int heightDivider = Mathf.CeilToInt(position.height/20);
        Handles.BeginGUI();
        offset += drag;
        Handles.color = new Color(.5f,.5f,.5f,.2f);
        Vector3 newOffset = new Vector3(offset.x%20,offset.y%20,0);
        for (int i = 0; i < widthDivider; i++)
        {
            Handles.DrawLine(new Vector3(20*i ,0,0) + newOffset,new Vector3(20*i,position.height,0) + newOffset);
        }
        for (int i = 0; i < heightDivider; i++)
        {
            Handles.DrawLine(new Vector3(0 ,20*i ,0) + newOffset,new Vector3(position.width,20*i,0) + newOffset);
        }
        Handles.color = Color.white;
        Handles.EndGUI();
    }
}
