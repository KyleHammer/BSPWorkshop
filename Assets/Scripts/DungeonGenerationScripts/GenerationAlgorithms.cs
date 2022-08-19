using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PROCEDURAL GENERATION ALGORITHMS SCRIPT
/// USED FOR THE RANDOM WALK VALUE GENERATION
/// CAN LATER BE USED FOR  BSP IF FOLLOWED FURTHER
/// </summary>

// Change class to static for accessibility
public static class GenerationAlgorithms
{
    // The main implementation of the algorithm, what we call
    // Returns a single path (which will eventually correspond to floor tile co-ordinates)
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength)
    {
        // Create a variable to store the 'path', the co-ordinates where floor tiles will spawn
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        // Add the start position
        path.Add(startPos);
        
        Vector2Int previousPos = startPos;

        // For the length of the walk...
        for (int i = 0; i < walkLength; i++)
        {
            // Walk one step in a random direction
            Vector2Int newPos = previousPos + Direction2D.GetRandomCardinalDirection();
            
            // Add that step to the path
            path.Add(newPos);
            
            // Set the previous pos to the new pos for the next step in the loop
            previousPos = newPos;
        }

        return path;
    }

    // We want to use a list instead of a hashset
    // This is because lists can keep track of the last position of the corridor
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength)
    {
        // List for the corridor tile co-ordinates
        List<Vector2Int> corridor = new List<Vector2Int>();
        
        // Pick a random direction for the corridor to take
        Vector2Int direction = Direction2D.GetRandomCardinalDirection();
        
        Vector2Int currentPos = startPos;
        
        corridor.Add(currentPos);

        // For the length of the corridor...
        for (int i = 0; i < corridorLength; i++)
        {
            // Walk one step in that direction
            currentPos += direction;
            
            // Add that co-ordinate to the corridor
            corridor.Add(currentPos);
        }

        return corridor;
    }
}
