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
    private List<Vector2> section2RoomTilePositions;
    private List<Vector2> bossRoomTilePositions;

    private void Start()
    {
        entranceRoomTilePositions = roomManager.entranceRoomTilePositions;
        section1RoomTilePositions = roomManager.section1RoomTilePositions;
        section2RoomTilePositions = roomManager.section2RoomTilePositions;
        bossRoomTilePositions = roomManager.bossRoomTilePositions;

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

            if(enemy.type == ENEMY_TYPE.BOSS)
            {
                ExcludeTilesForBoss(tilePositions);
            }

            if(tilePositions.Count > 0)
            {
                for (int j = 0; j < enemy.count; j++)
                {
                    Vector2 tilePosition = tilePositions[Random.Range(0, tilePositions.Count)];
                    Enemy enemyClone = Instantiate(enemy, new Vector3(tilePosition.x, tilePosition.y, 0), Quaternion.identity);
                    enemyClone.gameObject.transform.parent = transform;
                }
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
                tilePositions = section2RoomTilePositions;
                break;
            case ENEMY_TYPE.BOSS:
                tilePositions = bossRoomTilePositions;
                break;
            default:
                break;
        }

        return tilePositions;
    }

    private void ExcludeTilesForBoss(List<Vector2> tilePositions)
    {
        for (int i = 0; i < tilePositions.Count; i++)
        {
            if (tilePositions[i].y > -12)
            {
                tilePositions.RemoveAt(i);
            }
        }
    }
}
