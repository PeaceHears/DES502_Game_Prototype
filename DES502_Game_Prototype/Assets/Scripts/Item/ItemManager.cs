using Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public RoomManager roomManager;
    public Item[] items;

    private List<Vector2> section1RoomTilePositions;

    private void Start()
    {
        section1RoomTilePositions = roomManager.section1RoomTilePositions;

        CreateItems();
    }

    private void CreateItems()
    {
        if (items.Length == 0)
        {
            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i];

            if (!item)
            {
                continue;
            }

            List<Vector2> tilePositions = FillTilePositionsByEnemyType(item.type);

            if(tilePositions.Count > 0)
            {
                for (int j = 0; j < item.count; j++)
                {
                    Vector2 tilePosition = tilePositions[Random.Range(0, tilePositions.Count)];
                    Item itemClone = Instantiate(item, new Vector3(tilePosition.x, tilePosition.y, 0), Quaternion.identity);
                    itemClone.gameObject.transform.parent = transform;
                }
            }
        }
    }

    private List<Vector2> FillTilePositionsByEnemyType(ITEM_TYPE itemType)
    {
        List<Vector2> tilePositions = new List<Vector2>();

        switch (itemType)
        {
            case ITEM_TYPE.CHEST:
                tilePositions = section1RoomTilePositions;
                break;
            case ITEM_TYPE.COIN:
                tilePositions = section1RoomTilePositions;
                break;
            default:
                break;
        }

        return tilePositions;
    }
}
