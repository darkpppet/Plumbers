using Tiles;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Stage
{
    public class Stage2 : StageBase
    {
        private const int RowSize = 5;
        private const int ColumnSize = 7;

        private const float StartX = -ColumnSize / 2.0f - 1.0f;
        private const float StartY = RowSize / 2.0f - 0.5f;
        
        private readonly int[,] _mapIndex = new int[RowSize, ColumnSize] 
        {
            { 1, 3, 4, 1, 3, 2, 3 },
            { 3, 2, 3, 1, 3, 4, 1 },
            { 3, 2, 3, 1, 3, 4, 1 },
            { 1, 1, 4, 3, 2, 3, 3 },
            { 3, 1, 3, 1, 3, 1, 3 }
        };

        public override int MapIndex { get; protected set; } = 1;
        public override int StartRow { get; protected set; } = 3;
        public override int EndRow { get; protected set; } = 1;

        public override float Length { get; protected set; } = 1.0f;
        
        void Awake()
        {
            Map = new TileBase[5, 7];
            for (int i = 0; i < _mapIndex.GetLength(0); i++)
            {
                for (int j = 0; j < _mapIndex.GetLength(1); j++)
                {
                    Map[i, j] = _mapIndex[i, j] switch
                    {
                        1 => Instantiate(tile1),
                        2 => Instantiate(tile2),
                        3 => Instantiate(tile3),
                        4 => Instantiate(tile4),
                        _ => null
                    };

                    float x = StartX + j * Length;
                    float y = StartY - i * Length;
                    
                    Map[i, j].transform.position = new Vector2(x, y);
                    Map[i, j].transform.SetParent(transform);
                    Map[i, j].Position = (i, j);
                }
            }
            
            StartObject = Instantiate(startSprite, new Vector2(StartX - Length * 1.5f, StartY - StartRow), Quaternion.identity);
            StartObject.transform.SetParent(transform);
            
            EndObject = Instantiate(endSprite, new Vector2(StartX + Length * (ColumnSize + 0.5f), StartY - EndRow), Quaternion.identity);
            EndObject.transform.SetParent(transform);
        }
    }
}
