using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstGenerator : RandomWalkGenerator
{
    [SerializeField] private int corridorLength = 15;
    [SerializeField] private int corridorCount = 5;

    // Parameters used for generating rooms (not corridors)
    [SerializeField] [Range(0.1f, 1)] private float roomPercent = 0.8f;

    protected override void RunGenerator()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPos);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPos);
        
        // SAVE FOR DEAD ENDS PART
        // Locate all dead corridor ends
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        // Then create rooms at those dead ends
        CreateRoomsAtDeadEnds(deadEnds, roomPositions);
        
        // Add the corridors with the rooms
        floorPositions.UnionWith(roomPositions);
        
        tileSpawner.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileSpawner);
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

    // SAVE FOR DEAD ENDS PART
    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var pos in floorPositions)
        {
            // To find dead ends, we will use a neighbour count
            // If we only have a neighbour in one direct, then we know we found a dead end
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

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPos)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        
        // Need to convert and round a float into an int
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent);

        // Next line is a doozy
        // Requires Linq Library
        // This is known as a lambda expression, don't worry about it too much
        // Guid = Global Unique Identifier (a unique number each time, with a random order)
        // Take = Take a specific number of values. In this case we are taking roomToCreateCount rooms
        // .ToList() to cast it as a list
        List<Vector2Int> roomToCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

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

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPos)
    {
        Vector2Int currentPos = startPos;

        // Adding currentPos first as it is skipped in the for loop
        potentialRoomPos.Add(currentPos);

        for (int i = 0; i < corridorCount; i++)
        {
            List<Vector2Int> corridor = GenerationAlgorithms.RandomWalkCorridor(currentPos, corridorLength);
            
            // Set next current position to the last current position on the corridor
            // This will ensure that the corridors are connected
            // Have to -1 from the count since [] starts at 0, Count is outside the array
            currentPos = corridor[corridor.Count - 1];

            // Each end of a corridor needs to be added to the potential room position
            potentialRoomPos.Add(currentPos);
            
            // Add the corridors to the floor positions HashSet<>
            floorPositions.UnionWith(corridor);
        }
    }
}
