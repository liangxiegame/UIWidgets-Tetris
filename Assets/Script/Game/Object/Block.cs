using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.debugger;
using UnityEngine;

namespace TetrisApp
{
    public enum BlockType
    {
        O = 0,
        Z = 1,
        S = 2,
        T = 3,
        J = 4,
        L = 5,
        I = 6
    }

    public class Block
    {
        public static Dictionary<BlockType, List<List<int>>> BLOCK_SHAPES = new Dictionary<BlockType, List<List<int>>>()
        {
            {
                BlockType.O, new List<List<int>>()
                {
                    new List<int>() {1, 1},
                    new List<int>() {1, 1}
                }
            },
            {
                BlockType.Z, new List<List<int>>()
                {
                    new List<int>() {1, 1, 0},
                    new List<int>() {0, 1, 1}
                }
            },
            {
                BlockType.S, new List<List<int>>()
                {
                    new List<int>() {0, 1, 1},
                    new List<int>() {1, 1, 0}
                }
            },
            {
                BlockType.T, new List<List<int>>()
                {
                    new List<int>() {0, 1, 0},
                    new List<int>() {1, 1, 1}
                }
            },
            {
                BlockType.J, new List<List<int>>()
                {
                    new List<int>() {1, 0, 0},
                    new List<int>() {1, 1, 1}
                }
            },
            {
                BlockType.L, new List<List<int>>()
                {
                    new List<int>() {0, 0, 1},
                    new List<int>() {1, 1, 1}
                }
            },
            {
                BlockType.I, new List<List<int>>()
                {
                    new List<int>() {1, 1, 1, 1},
                }
            }
        };

        public static Dictionary<BlockType, List<List<int>>> BLOCK_ORIGINS =
            new Dictionary<BlockType, List<List<int>>>()
            {
                {
                    BlockType.O, new List<List<int>>()
                    {
                        new List<int>() {0, 0},
                    }
                },
                {
                    BlockType.Z, new List<List<int>>()
                    {
                        new List<int>() {0, 0}
                    }
                },
                {
                    BlockType.S, new List<List<int>>()
                    {
                        new List<int>() {0, 0}
                    }
                },
                {
                    BlockType.T, new List<List<int>>()
                    {
                        new List<int>() {1, 0}, // right 
                        new List<int>() {-1, 1}, // down
                        new List<int>() {0, -1}, // left
                        new List<int>() {0, 0} // up
                    }
                },
                {
                    BlockType.J, new List<List<int>>()
                    {
                        new List<int>() {0, 0}
                    }
                },
                {
                    BlockType.L, new List<List<int>>()
                    {
                        new List<int>() {0, 0}
                    }
                },
                {
                    BlockType.I, new List<List<int>>()
                    {
                        new List<int>() {1, -1}, // ---
                        new List<int>() {-1, 1} // ||
                    }
                }
            };

        public static Dictionary<BlockType, List<int>> START_XY =
            new Dictionary<BlockType, List<int>>()
            {
                {BlockType.O, new List<int> {0, 4}},
                {BlockType.Z, new List<int> {0, 4}},
                {BlockType.S, new List<int> {0, 4}},
                {BlockType.T, new List<int> {0, 4}},
                {BlockType.J, new List<int> {0, 4}},
                {BlockType.L, new List<int> {0, 4}},
                {BlockType.I, new List<int> {1, 4}},
            };


        private Block()
        {
        }

        /// <summary>
        /// 下一个随机的
        /// </summary>
        public static Block Next
        {
            get
            {
                var type = (BlockType) Random.Range(0, 7);


                Debug.Log(BLOCK_SHAPES[type]);
                Debug.Log(type);

                return new Block()
                {
                    RowIndex = START_XY[type][0],
                    ColIndex = START_XY[type][1],
                    Type = type,
                    Shape = BLOCK_SHAPES[type]
                };
            }
        }

        public BlockType Type;

        public int RowIndex    = 0;
        public int ColIndex    = 0;
        public int RotateIndex = 0;

        public List<List<int>> Shape;

        public bool IsValidateInData(List<List<int>> data)
        {
            Debug.Log(Shape);

            if (RowIndex + Shape.Count > 20 ||
                RowIndex < 0 ||
                ColIndex + Shape[0].Count > 10 ||
                ColIndex < 0)
            {
                return false;
            }

            for (var i = 0; i < Shape.Count; i++)
            {
                for (int j = 0; j < Shape[0].Count; j++)
                {
                    if (data[i + RowIndex][j + ColIndex] == 1
                        && Get(i + RowIndex, j + ColIndex) == 1)
                    {
                        return false;
                    }
                }
            }


            return true;
        }

        public int? Get(int rowIndex, int colIndex)
        {
            rowIndex -= RowIndex;
            colIndex -= ColIndex;

            if (rowIndex < 0 ||
                rowIndex >= Shape.Count ||
                colIndex < 0 ||
                colIndex >= Shape[0].Count)
            {
                return null;
            }
            else
            {
                return Shape[rowIndex][colIndex];
            }
        }

        public Block Right()
        {
            return new Block()
            {
                RowIndex = RowIndex,
                ColIndex = ColIndex + 1,
                Shape = Shape,
                Type = Type,
                RotateIndex = RotateIndex
            };
        }


        public Block Down()
        {
            return new Block()
            {
                RowIndex = RowIndex + 1,
                ColIndex = ColIndex,
                Shape = Shape,
                Type = Type,
                RotateIndex = RotateIndex
            };
        }

        public Block Rotate()
        {
            var oldShape = Shape;

            var newShape = Utils.Create2x2List(oldShape[0].Count, oldShape.Count,
                (rowIndex, colIndex) => oldShape[oldShape.Count - colIndex - 1][rowIndex]);

            var nextRowIndex = RowIndex + BLOCK_ORIGINS[Type][RotateIndex][1];
            var nextColIndex = ColIndex + BLOCK_ORIGINS[Type][RotateIndex][0];

            var nextRotateIndex = RotateIndex + 1 >= BLOCK_ORIGINS[Type].Count ? 0 : RotateIndex + 1;

            return new Block()
            {
                RowIndex = nextRowIndex,
                ColIndex = nextColIndex,
                Shape = newShape,
                Type = Type,
                RotateIndex = nextRotateIndex
            };
        }
    }
}