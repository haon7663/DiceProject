#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.XR;

public static class EditorHierarchyIconHelper
{
    [UnityEditor.InitializeOnLoadMethod]
    private static void ApplyHierarchyIcon()
    {
        if (iconData == null || iconData.Length == 0)
            InitIconData();

        UnityEditor.EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon;
    }

    private static (Type type, Texture icon, Color color)[] iconData;

    private static void InitIconData()
    {
        iconData = new (Type, Texture, Color)[]
        {
            (typeof(GameManager), EditorGUIUtility.FindTexture("GameManager Icon"), Color.white),
            (typeof(TurnManager), EditorGUIUtility.FindTexture("GameManager Icon"), Color.white),
            (typeof(DiceManager), EditorGUIUtility.FindTexture("GameManager Icon"), Color.white),
            (typeof(CardManager), EditorGUIUtility.FindTexture("GameManager Icon"), Color.white),
            (typeof(Creature),   EditorGUIUtility.FindTexture("d_Favorite On Icon"), Color.yellow),
            (typeof(Camera), EditorGUIUtility.FindTexture("Camera Gizmo"),       Color.cyan),
        };
    }

    private static void DrawHierarchyIcon(int instanceID, Rect selectionRect)
    {
        Rect iconRect = new Rect(selectionRect);
        iconRect.x = 32f; // ���̶�Ű ���� ��
        iconRect.width = 16f;

        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null)
            return;

        Color c = GUI.color;

        for (int i = 0; i < iconData.Length; i++)
        {
            ref var current = ref iconData[i];
            if (current.icon != null && go.GetComponent(current.type) != null)
            {
                GUI.color = go.activeInHierarchy ? current.color : Color.white * 0.5f;
                GUI.DrawTexture(iconRect, current.icon);
                break;
            }
        }

        GUI.color = c;
    }
}

#endif