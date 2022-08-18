using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// EDITOR ALTERING SCRIPT
/// GIVES EDITOR ATTRIBUTES TO ABSTRACT-DUNGEON-GENERATOR
/// </summary>

[CustomEditor((typeof(AbstractDungeonGenerator)), true)]
public class DungeonGeneratorEditor : Editor
{
    private AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button(("Create Dungeon")))
        {
            generator.GenerateDungeon();
        }
    }
}
