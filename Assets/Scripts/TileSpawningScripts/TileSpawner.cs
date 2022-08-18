using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;

    [SerializeField] private List<TileBase> floorTiles;

    // Think of IEnumerable is the var of lists
    // You can replace with HashSet<> if you wish
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos)
    {
        PaintTiles(floorPos, floorTilemap, GetRandomFloorTile());
    }

    // Keeping these painting functions separated (i.e. PaintTiles, PaintSingleTile)
    // So they can be reused by other methods
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var pos in positions)
        {
            PaintSingleTile(tilemap, tile, pos);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int pos)
    {
        // WorldToCell function needs a Vector3Int parameter.
        // Luckily we can cast a Vector2Int into this like so
        var tilePos = tilemap.WorldToCell((Vector3Int) pos);
        
        tilemap.SetTile(tilePos, tile);
    }

    // Tile base is the term used for an individual tile object
    private TileBase GetRandomFloorTile()
    {
        // Explain Random.Range and it's interesting quirk with ints
        int i = Random.Range(0, floorTiles.Count);
        return floorTiles[i];
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
    }
}
