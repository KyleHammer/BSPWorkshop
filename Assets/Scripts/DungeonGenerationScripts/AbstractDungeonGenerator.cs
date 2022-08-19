using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PARENT OF THE RANDOM WALK GENERATOR
/// INHERITS ATTRIBUTES FROM HERE
/// CUSTOM CLASS, PROVIDED FOR YOU
/// </summary>

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TileSpawner tileSpawner = null;
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tileSpawner.Clear();
        RunGenerator();
    }

    protected abstract void RunGenerator();
}
