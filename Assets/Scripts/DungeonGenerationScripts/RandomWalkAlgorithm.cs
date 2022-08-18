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
            Vector2Int newPos = previousPos + GetRandomCardinalDirection();
            path.Add(newPos);
            previousPos = newPos;
        }

        return path;
    }

    private static Vector2Int GetRandomCardinalDirection()
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
    }
}
