using Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManager : MonoBehaviour
{
    public RoomManager roomManager;
    public Enemy[] enemies;

    private List<Vector2> entranceRoomTilePositions;
    private List<Vector2> section1RoomTilePositions;

    private void Start()
    {
        entranceRoomTilePositions = roomManager.entranceRoomTilePositions;
        section1RoomTilePositions = roomManager.section1RoomTilePositions;

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

            List<Vector2> tilePositions = FillTilePositionsByEnemyType(enemy.type);

            for (int j = 0; j < enemy.count; j++)
            {
                Vector2 tilePosition = tilePositions[Random.Range(0, tilePositions.Count)];
                Instantiate(enemy, new Vector3(tilePosition.x, tilePosition.y, 0), Quaternion.identity);
            }
        }
    }

    private List<Vector2> FillTilePositionsByEnemyType(ENEMY_TYPE enemyType)
    {
        List<Vector2> tilePositions = new List<Vector2>();

        switch (enemyType)
        {
            case ENEMY_TYPE.ORC:
                tilePositions = entranceRoomTilePositions;
                break;
            case ENEMY_TYPE.SOLDIER:
                tilePositions = section1RoomTilePositions;
                break;
            case ENEMY_TYPE.NINJA:
                break;
            default:
                break;
        }

        return tilePositions;
    }
}
