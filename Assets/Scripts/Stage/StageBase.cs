using System.Collections.Generic;
using NUnit.Framework;
using Tiles;
using Towers;
using UnityEngine;

namespace Stage
{
    public abstract class StageBase : MonoBehaviour
    {
        public virtual int MapIndex { get; protected set; }
        
        public TileBase[,] Map;
        public (int Row, int Column) MapSize => (Map.GetLength(0),  Map.GetLength(1));

        public List<TowerBase> towers = new();

        public GameObject StartObject { get; protected set; }
        public GameObject EndObject {  get; protected set; }
        
        public List<(int Row, int Column)> Path;
        
        public virtual int StartRow { get; protected set; }
        public virtual int EndRow { get; protected set; }
        
        public virtual float Length { get; protected set; }

        public GameObject startSprite;
        public GameObject endSprite;
        
        public Tile1 tile1;
        public Tile2 tile2;
        public Tile3 tile3;
        public Tile4 tile4;

        public void ResetStage()
        {
            foreach (TileBase tile in Map)
            {
                tile.Restore();
                tile.transform.rotation = Quaternion.identity;
            }

            foreach (TowerBase tower in towers)
            {
                Destroy(tower.gameObject);
            }
            towers.Clear();
        }
        
        public bool FindPath()
        {
            bool[, ] checkMap = new bool[MapSize.Row, MapSize.Column];
            (int Row, int Column)[,] preMap = new (int Row, int Column)[MapSize.Row, MapSize.Column];
            
            Queue<(int Row, int Column)> queue = new();
            
            if (Map[StartRow, 0].Left == true && Map[EndRow, MapSize.Column - 1].Right == true)
            {
                checkMap[StartRow, 0] = true;
                preMap[StartRow, 0] = (StartRow, -1);
                queue.Enqueue((StartRow, 0));

                while (queue.Count > 0)
                {
                    var (row, column) = queue.Dequeue();

                    if (row == EndRow && column == MapSize.Column - 1)
                    {
                        List<(int Row, int Column)> path = new();

                        (int beforeRow, int beforeColumn) = (row, column);
                        
                        while (beforeColumn != -1)
                        {
                            path.Add((beforeRow, beforeColumn));
                            (beforeRow, beforeColumn) = preMap[beforeRow, beforeColumn];
                        }
                        
                        path.Reverse();

                        Path = path;
                        
                        return true;
                    }
                    
                    if (row + 1 < MapSize.Row && Map[row + 1, column].IsEnabled && checkMap[row + 1, column] == false &&
                        Map[row, column].Bottom == true && Map[row + 1, column].Top == true)
                    {
                        checkMap[row + 1, column] = true;
                        preMap[row + 1, column] = (row, column);
                        queue.Enqueue((row + 1, column));
                    }
                    
                    if (row - 1 >= 0 && Map[row - 1, column].IsEnabled && checkMap[row - 1, column] == false &&
                        Map[row, column].Top == true && Map[row - 1, column].Bottom == true)
                    {
                        checkMap[row - 1, column] = true;
                        preMap[row - 1, column] = (row, column);
                        queue.Enqueue((row - 1, column));
                    }
                    
                    if (column + 1 < MapSize.Column && Map[row, column + 1].IsEnabled && checkMap[row, column + 1] == false &&
                        Map[row, column].Right == true && Map[row, column + 1].Left == true)
                    {
                        checkMap[row, column + 1] = true;
                        preMap[row, column + 1] = (row, column);
                        queue.Enqueue((row, column + 1));
                    }
                    
                    if (column - 1 >= 0 && Map[row, column - 1].IsEnabled && checkMap[row, column - 1] == false &&
                        Map[row, column].Left == true && Map[row, column - 1].Right == true)
                    {
                        checkMap[row, column - 1] = true;
                        preMap[row, column - 1] = (row, column);
                        queue.Enqueue((row, column - 1));
                    }
                }
            }

            return false;
        }
    }
}
