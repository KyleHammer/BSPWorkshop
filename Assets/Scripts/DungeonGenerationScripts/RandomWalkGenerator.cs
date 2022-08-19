using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// MAIN DUNGEON GENERATOR
/// OUR MAIN GENERATOR
/// INHERITS FROM ABSTRACTDUNGEONGENERATOR, IS INHERITED BY CORRIDORFIRSTGENERATOR
/// </summary>


public class RandomWalkGenerator : AbstractDungeonGenerator
{
    // Protected as it is used by a later child class, CorridorFirstGenerator
    [SerializeField] protected RandomWalkSO randomWalkParameters;

    protected override void RunGenerator()
    {
        // Have a list to store the positions of the multiple random walks
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPos);
        
        // Clear the previous tiles and paint them based on the floorPositions
        // Functions are accessible because it is public and a part of the inherited class
        tileSpawner.Clear();
        tileSpawner.PaintFloorTiles(floorPositions);
        // Function is accessible because it is public AND static
        WallGenerator.CreateWalls(floorPositions, tileSpawner);
    }

    // Will return a series of positions where floor tiles are to spawn
    protected HashSet<Vector2Int> RunRandomWalk(RandomWalkSO parameters, Vector2Int pos)
    {
        Vector2Int currentPos = pos;
        // Have a list to store the positions of the multiple random walks
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        
        // Loop over every iteration. Multiple iterations will result in less spaghetti-like generation
        // since the multiple paths will overlap with each other.
        for (int i = 0; i < parameters.iterations; i++)
        {
            // Run one simple walk
            HashSet<Vector2Int> path = GenerationAlgorithms.SimpleRandomWalk(currentPos, parameters.walkLength);
            
            // Adds to the list but does not worry about duplicates
            // The beauty of Dictionaries/Hashmaps
            floorPositions.UnionWith(path);
            
            // We can start the walk from a random existing position in floorPositions if this is true
            if (parameters.startRandomlyEachIteration)
                currentPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count)); // Linq required for method
        }

        return floorPositions;
    }
}
