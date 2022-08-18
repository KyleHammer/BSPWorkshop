using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UTILITY CLASS SCRIPT TO SUPPORT GETTING THE DIRECTION OF TILES
/// USEFUL FOR THE WALK AND WALL PLACEMENT
/// TOO COMPLEX TO COVER FOR THE TUTORIAL
/// </summary>

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP

    };
    
    public static Vector2Int GetRandomCardinalDirection()
    {
        // Can alter the probability of the walk direction with the case statements
        // E.g. Case int n when (n => 0 && n < 4)
        switch (Random.Range(0, 4))
        {
            case 0:
                return Vector2Int.down;
            case 1:
                return Vector2Int.left;
            case 2:
                return Vector2Int.right;
            case 3:
                return Vector2Int.up;
            default: // Theoretically shouldn't be possible
                return Vector2Int.down;
        }
    }}
