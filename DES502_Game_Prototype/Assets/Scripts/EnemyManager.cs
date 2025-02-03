using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManager : MonoBehaviour
{
    public RoomManager roomManager;
    public Enemy[] enemies;

    private List<Vector2> entranceRoomTilePositions;


    private void Start()
    {
        entranceRoomTilePositions = roomManager.entranceRoomTilePositions;

        CreateEnemies();
    }

    private void CreateEnemies()
    {
        if(enemies.Length == 0)
        {
            return;
        }

        for(int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];

            if (!enemy)
            {
                continue;
            }

            for (int j = 0; j < enemy.count; j++)
            {
                Vector2 tilePosition = entranceRoomTilePositions[Random.Range(0, entranceRoomTilePositions.Count)];
                Instantiate(enemy, new Vector3(tilePosition.x, tilePosition.y, 0), Quaternion.identity);
            }
        }
    }
}
