using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    public Tilemap entranceRoom;

    public List<Vector2> entranceRoomTilePositions;

    private void Start()
    {
        entranceRoomTilePositions = new List<Vector2>();

        SetTileMapPositions(entranceRoom, entranceRoomTilePositions);
    }

    private void SetTileMapPositions(Tilemap tilemap, List<Vector2> tilePositions)
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(position))
            {
                continue;
            }

            tilePositions.Add(new Vector2(position.x + 0.5f, position.y + 0.5f));
        }
    }
}
