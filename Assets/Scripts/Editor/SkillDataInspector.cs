using System;
using PlasticGui.Configuration.CloudEdition;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardSO))]
public class SkillDataInspector : Editor
{
    private CardSO _cardData;

    private void OnEnable()
    {
        _cardData = serializedObject.targetObject as CardSO;
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
