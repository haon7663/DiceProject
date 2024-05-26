using System;
using PlasticGui.Configuration.CloudEdition;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardData))]
public class SkillDataInspector : Editor
{
    private CardData _cardData;

    private void OnEnable()
    {
        _cardData = serializedObject.targetObject as CardData;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.Space();
        _cardData.sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", _cardData.sprite, typeof(Sprite), true);
        EditorGUILayout.Space();
        
        base.OnInspectorGUI();
        EditorGUILayout.EndVertical();
    }
}
