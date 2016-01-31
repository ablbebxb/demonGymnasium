using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour {
    public const int MOVING = 0;
    public const int SHOOT = 1;
    public const int EXPAND = 2;

    int currentActionSelected;
    Entity currentEntity;
    PlayerSelectManager playerSelectManager;
    
    void Start()
    {
        currentActionSelected = -1;
        playerSelectManager = GetComponent<PlayerSelectManager>();
    }


    public void sestActionType(int actionType)
    {
        this.currentActionSelected = actionType;
    }

    public void handleTileSelected()
    {

    }

    public void selectMovement(Entity entity)
    {
        currentEntity = entity;
        currentActionSelected = MOVING;
    }

    public void setAttack(Entity entity)
    {
        currentEntity = entity;
        currentActionSelected = SHOOT;
    }

    public void selectExpand(Entity entity)
    {
        currentEntity = entity;
        currentActionSelected = EXPAND;

        handleExpandLogic();
    }

    public void handleMovementLogic()
    {
        
    }

    public void handleAttackLogic(Entity entity)
    {
        
    }

    public void handleExpandLogic()
    {
        Tile[,] mapTiles = MapGenerator.mapTiles;
        int x = currentEntity.getCurrentTile().getX();
        int y = currentEntity.getCurrentTile().getY();
        mapTiles[x, y].setTileType(currentEntity.entityType);
        if (x - 1 >= 0)
        {
            mapTiles[x - 1, y].setTileType(currentEntity.entityType);
        }
        if (y - 1 >= 0)
        {
            mapTiles[x, y - 1].setTileType(currentEntity.entityType);
        }
        if (x + 1 < mapTiles.GetLength(0))
        {
            mapTiles[x + 1, y].setTileType(currentEntity.entityType);
        }
        if (y + 1 < mapTiles.GetLength(1))
        {
            mapTiles[x, y + 1].setTileType(currentEntity.entityType);
        }


    }

    void checkKillEnemy(Tile tile)
    {
        Entity entity = tile.getCurrentEntity();
         if (entity == null)
        {
            return;
        }
        
    } 

}
