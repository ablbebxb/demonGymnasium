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

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentActionSelected != -1)
        {
            playerSelectManager.mouseClicked();
            bool success = false;
            if (currentActionSelected == SHOOT)
            {
                success = handleAttackLogic();
            }

            if (currentActionSelected == MOVING)
            {
                success = handleMovementLogic();
            }

            if (success)
            {
                GameManager.gameManager.performAction();
            }
        }
        
    }

    public void selectMovement(Entity entity)
    {
        currentEntity = entity;
        currentActionSelected = MOVING;
    }

    public void selectAttack(Entity entity)
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

    public bool handleMovementLogic()
    {
        currentActionSelected = -1;
        bool obstructed = false;
        int type = (GameManager.gameManager.getPlayerTurn() ? 0 : 1);
        Tile goalTile = playerSelectManager.currentTileSelected;
        int sourceX = currentEntity.getCurrentTile().getX();
        int sourceY = currentEntity.getCurrentTile().getY();
        int x = goalTile.getX();
        int y = goalTile.getY();
        bool isKing = (currentEntity.GetType() == typeof(King));
        if (sourceX == x && sourceY < y)
        {
            for (int i = 1; i <= y - sourceY; i++)
            {
                Tile tile = MapGenerator.mapTiles[x, sourceY + i];
                if (tile.getIsObstructed() || (!isKing && tile.getCurrentTileType() != type))
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                currentEntity.moveNorth(y - sourceY);
            }
        }
        else if (sourceX == x && sourceY > y)
        {
            for (int i = 0; i < sourceY - y; i++)
            {
                Tile tile = MapGenerator.mapTiles[x, y + i];
                if (tile.getIsObstructed() || (!isKing && tile.getCurrentTileType() != type))
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                currentEntity.moveSouth(sourceY - y);
            }
        }
        else if (sourceY == y && sourceX < x)
        {
            for (int i = 1; i <= x - sourceX; i++)
            {
                Tile tile = MapGenerator.mapTiles[sourceX + i, y];
                if (tile.getIsObstructed() || (!isKing && tile.getCurrentTileType() != type))
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                currentEntity.moveEast(x - sourceX);
            }
        }
        else if (sourceY == y && sourceX > x)
        {
            for (int i = 0; i < sourceX - x; i++)
            {
                Tile tile = MapGenerator.mapTiles[x + i, y];
                if (tile.getIsObstructed() || (!isKing && tile.getCurrentTileType() != type))
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                currentEntity.moveWest(sourceX - x);
            }
        } else if (currentEntity.GetType() == typeof(King) && Mathf.Abs(sourceX - x) == Mathf.Abs(sourceY - y))
        {
            King king = (King)currentEntity;
            if (sourceY > y && sourceX > x)
            {
                for (int i = 0; i < sourceX - x; i++)
                {
                    Tile tile = MapGenerator.mapTiles[x + i, y + i];
                    if (tile.getIsObstructed())
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveSouthWest(sourceX - x);
                }
            }
            else if (sourceY < y && sourceX > x)
            {
                for (int i = 0; i < sourceX - x; i++)
                {
                    Tile tile = MapGenerator.mapTiles[x + i, y - i];
                    if (tile.getIsObstructed())
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveNorthWest(sourceX - x);
                }
            }
            else if (sourceY > y && sourceX < x)
            {
                for (int i = 1; i <= x - sourceX; i++)
                {
                    Tile tile = MapGenerator.mapTiles[x - i, y + i];
                    if (tile.getIsObstructed())
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveSouthEast(x - sourceX);
                }
            }
            else if (sourceY < y && sourceX < x)
            {
                for (int i = 1; i <= x - sourceX; i++)
                {
                    Tile tile = MapGenerator.mapTiles[x - i, y - i];
                    if (tile.getIsObstructed())
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveNorthEast(x - sourceX);
                }
            }
        }
        else
        {
            obstructed = true;
        }

        if (!obstructed)
        {
            currentEntity.getCurrentTile().setEntity(null);
            currentEntity.setCurrentTile(goalTile);
            goalTile.setEntity(currentEntity);
        }

        return !obstructed;
    }
    

    public bool handleAttackLogic()
    {
        currentActionSelected = -1;
        if (playerSelectManager.currentTileSelected == null)
        {
            return false;
        }

        Tile tile = playerSelectManager.currentTileSelected;
        Tile playerTile = currentEntity.getCurrentTile();
        int sourceX = playerTile.getX();
        int sourceY = playerTile.getY();
        int x = tile.getX();
        int y = tile.getY();
        if (checkLineofSight(playerTile.getX(), playerTile.getY(), tile.getX(), tile.getY()))
        {
            if (tile.getCurrentEntity() != null && tile.getCurrentEntity().GetType() != typeof(Obstacle))
            {
                damageIfEnemy(tile.getCurrentEntity());
            }

            int type = (GameManager.gameManager.getPlayerTurn() ? 0 : 1);

            if (sourceX == x && sourceY < y)
            {
                
                for (int i = 1; i <= y - sourceY; i++)
                {
                    MapGenerator.mapTiles[x, sourceY + i].setTileType(type);
                }
                return true;
            }
            else if (sourceX == x && sourceY > y)
            {
                for (int i = 0; i < sourceY - y; i++)
                {
                    MapGenerator.mapTiles[x, y + i].setTileType(type);
                }
                return true;

            }
            else if (sourceY == y && sourceX < x)
            {
                for (int i = 1; i <= x - sourceX; i++)
                {
                    MapGenerator.mapTiles[sourceX + i, y].setTileType(type);
                }
                return true;

            }
            else if (sourceY == y && sourceX > x)
            {
                for (int i = 0; i < sourceX - x; i++)
                {
                    MapGenerator.mapTiles[x + i, y].setTileType(type);
                }
                return true;

            }

        }
        return false;
    }
    private void damageIfEnemy(Entity other)
    {
        if (other != null && other.getIsPlayer() != GameManager.gameManager.getPlayerTurn())
        {
            other.takeDamage();
        }
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
        if (entity.entityType == currentEntity.entityType)
        {
            return;
        }
        if (entity.entityType == Tile.NEUTRAL)
        {
            return;
        }
        entity.takeDamage();
    }

    private bool checkLineofSight(int sourceX, int sourceY, int x, int y)
    {
        bool lineOfSight = true;
        if (sourceX == x && sourceY < y && y - sourceY <= 2)
        {
            for (int i = 1; i < y - sourceY; i++)
            {
                if (MapGenerator.mapTiles[x, sourceY + i].getIsObstructed())
                {
                    lineOfSight = false;
                }
            }
        }
        else if (sourceX == x && sourceY > y && sourceY - y <= 2)
        {
            for (int i = 1; i < sourceY - y; i++)
            {
                if (MapGenerator.mapTiles[x, y + i].getIsObstructed())
                {
                    lineOfSight = false;
                }
            }
        }
        else if (sourceY == y && sourceX < x && x - sourceX <= 2)
        {
            for (int i = 1; i < x - sourceX; i++)
            {
                if (MapGenerator.mapTiles[sourceX + i, y].getIsObstructed())
                {
                    lineOfSight = false;
                }
            }
        }
        else if (sourceY == y && sourceX > x && sourceX - x <= 2)
        {
            for (int i = 1; i < sourceX - x; i++)
            {
                if (MapGenerator.mapTiles[x + i, y].getIsObstructed())
                {
                    lineOfSight = false;
                }
            }
        }
        else
        {
            lineOfSight = false;
        }
        return lineOfSight;
    }

}
