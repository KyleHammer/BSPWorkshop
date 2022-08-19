using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// CORRIDOR GENERATOR
/// RESPONSIBLE FOR GENERATING THE CORRIDORS AND THEN THE ROOMS AFTERWARDS
/// USES RANDOMWALKGENERATOR WHICH INHERITS FROM ABSTRACTDUNGEONGENERATOR
/// </summary>

public class CorridorFirstGenerator : RandomWalkGenerator
{
    [SerializeField] private int corridorLength = 15;
    [SerializeField] private int corridorCount = 5;

    // Parameters used for generating rooms (not corridors)
    // We don't always want a room at the end of a corridor
    // Sometimes we want 2 corridors to connect
    [SerializeField] [Range(0.1f, 1)] private float roomPercent = 0.8f;

    // Override the standard RunGenerator() function
    protected override void RunGenerator()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        // All the floor tile positions
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        // This is where rooms have the potential to spawn (end of each corridor)
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        
        // Generate the corridor positions first
        CreateCorridors(floorPositions, potentialRoomPositions);

        // This is where rooms are to DEFINITELY spawn
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
        
        // Locate all dead corridor ends
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        // Then create rooms at those dead ends
        CreateRoomsAtDeadEnds(deadEnds, roomPositions);
        
        // Add the corridors floor positions with the rooms
        floorPositions.UnionWith(roomPositions);
        
        // Spawn the floor and wall tiles based on floorPositions
        tileSpawner.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileSpawner);
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        
        // Need to convert and round a float into an int
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        // Next line is a doozy
        // Requires Linq Library
        // This is known as a lambda expression, don't worry about it too much
        // Guid = Global Unique Identifier (a unique number each time, we use this to get a random order)
        // Take = Take a specific number of values. In this case we are taking roomToCreateCount rooms
        // .ToList() to cast it as a list
        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        // For each random room position we have...
        foreach (Vector2Int roomPos in roomToCreate)
        {
            // Generate a room
            HashSet<Vector2Int> roomFloor = RunRandomWalk(randomWalkParameters, roomPos);
            // Add the new room to the room positions
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        Vector2Int currentPos = startPos;

        // Adding currentPos to the positions
        potentialRoomPositions.Add(currentPos);

        // For the number of corridors...
        for (int i = 0; i < corridorCount; i++)
        {
            // Store the corridor tiles in a list
            List<Vector2Int> corridor = GenerationAlgorithms.RandomWalkCorridor(currentPos, corridorLength);
            
            // Set next current position to the last current position on the corridor
            // This will ensure that the corridors are connected
            // Have to -1 from the count since [] starts at 0, Count is outside the array
            // Or you can use .Last()
            currentPos = corridor[corridor.Count - 1];

            // Each end of a corridor needs to be added to the potential room position
            potentialRoomPositions.Add(currentPos);
            
            // Add the corridors to the floor positions
            floorPositions.UnionWith(corridor);
        }
    }
    
    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        // Loop over each dead end
        foreach (var pos in deadEnds)
        {
            // IF a dead end is not part of a room then
            if (roomFloors.Contains(pos) == false)
            {
                // Run the random walk generator
                HashSet<Vector2Int> room = RunRandomWalk(randomWalkParameters, pos);
                // Add it to the room floors
                roomFloors.UnionWith(room);
            }
        }
    }
    
    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var pos in floorPositions)
        {
            // To find dead ends, we will use a neighbour count
            // If we only have a neighbour in one direction, then we know we found a dead end
            // If we have more than one, we know it's a corridor intersection or corridor corner
            int neighboursCount = 0;
            
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(pos + direction))
                    neighboursCount++;
            }
            
            // If there is only 1 neighbour
            if (neighboursCount == 1)
                deadEnds.Add(pos);
        }
        
        return deadEnds;
    }
}
