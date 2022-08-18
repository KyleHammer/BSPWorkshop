using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RANDOM WALK TUTORIAL
/// </summary>

// Change class to static for accessibility
public static class RandomWalkAlgorithm
{
    // The main implementation of the algorithm, what we call
    // Returns a single path (which will eventually correspond to floor tile co-ordinates)
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPos);
        Vector2Int previousPos = startPos;

        for (int i = 0; i < walkLength; i++)
        {
            Vector2Int newPos = previousPos + Direction2D.GetRandomCardinalDirection();
            path.Add(newPos);
            previousPos = newPos;
        }

        return path;
    }

    
}
