using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PROCEDURAL GENERATION ALGORITHMS SCRIPT
/// USED FOR THE BSP AND RANDOM WALK VALUE GENERATION
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

    // We want to use a list instead of a hashset
    // This is because lists can keep track of the last position of the corridor
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        Vector2Int direction = Direction2D.GetRandomCardinalDirection();
        Vector2Int currentPos = startPos;
        
        // Need to add the startPos to the corridor
        // As the loop would not add it
        corridor.Add(currentPos);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPos += direction;
            corridor.Add(currentPos);
        }

        return corridor;
    }
}
