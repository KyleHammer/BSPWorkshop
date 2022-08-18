using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// MAIN DUNGEON GENERATOR
/// HEART OF THE GENERATOR
/// </summary>

public class DungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] private int iterations = 10;
    [SerializeField] public int walkLength = 10;
    [SerializeField] public bool startRandomlyEachIteration = true;

    protected override void RunGenerator()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        
        // Functions are accessible because it is public
        tileSpawner.Clear();
        tileSpawner.PaintFloorTiles(floorPositions);
    }

    // Will return a series of positions where floor tiles are to spawn
    protected HashSet<Vector2Int> RunRandomWalk()
    {
        Vector2Int currentPos = startPos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        
        // Loop over every iteration. Multiple iterations will result in less spaghetti-like generation
        // since the multiple paths will overlap with each other.
        // A board demonstration might be useful for this
        for (int i = 0; i < iterations; i++)
        {
            HashSet<Vector2Int> path = RandomWalkAlgorithm.SimpleRandomWalk(currentPos, walkLength);
            
            // Adds to the list but does not worry about duplicates
            // The beauty of Dictionaries/Hashmaps
            floorPos.UnionWith(path);
            
            if (startRandomlyEachIteration)
                currentPos = floorPos.ElementAt(Random.Range(0, floorPos.Count)); // Linq required for method
        }

        return floorPos;
    }
}
