using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    public Tilemap entranceRoom;
    public Tilemap section1Room;
    public Tilemap section2Room;

    public List<Vector2> entranceRoomTilePositions;
    public List<Vector2> section1RoomTilePositions;
    public List<Vector2> section2RoomTilePositions;

    private void Start()
    {
        entranceRoomTilePositions = new List<Vector2>();
        section1RoomTilePositions = new List<Vector2>();
        section2RoomTilePositions = new List<Vector2>();

        SetTileMapPositions(entranceRoom, entranceRoomTilePositions);
        SetTileMapPositions(section1Room, section1RoomTilePositions);
        SetTileMapPositions(section2Room, section2RoomTilePositions);
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
