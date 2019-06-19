using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TerisGame
{
    public enum BlockType
    {
        I,
        L,
        J,
        Z,
        S,
        O,
        T
    }

    public class Block
    {
        public static Dictionary<BlockType, List<List<int>>> BLOCK_SHAPES = new Dictionary<BlockType, List<List<int>>>()
        {
            {BlockType.I, new List<List<int>>() {new List<int> {1, 1, 1, 1}}},
            {
                BlockType.L, new List<List<int>>()
                {
                    new List<int> {0, 0, 1},
                    new List<int> {1, 1, 1}
                }
            },
            {
                BlockType.J, new List<List<int>>()
                {
                    new List<int> {1, 0, 0},
                    new List<int> {1, 1, 1}
                }
            },
            {
                BlockType.Z, new List<List<int>>()
                {
                    new List<int> {1, 1, 0},
                    new List<int> {0, 1, 1}
                }
            },
            {
                BlockType.S, new List<List<int>>()
                {
                    new List<int> {0, 1, 1},
                    new List<int> {1, 1, 0}
                }
            },
            {
                BlockType.O, new List<List<int>>()
                {
                    new List<int> {1, 1},
                    new List<int> {1, 1}
                }
            },
            {
                BlockType.T, new List<List<int>>()
                {
                    new List<int> {0, 1, 0},
                    new List<int> {1, 1, 1}
                }
            }
        };

        // 方块初始化时的位置
        public static Dictionary<BlockType, List<int>> START_XY = new Dictionary<BlockType, List<int>>()
        {
            {BlockType.I, new List<int>() {3, 0}},
            {BlockType.L, new List<int>() {4, -1}},
            {BlockType.J, new List<int>() {4, -1}},
            {BlockType.Z, new List<int>() {4, -1}},
            {BlockType.S, new List<int>() {4, -1}},
            {BlockType.O, new List<int>() {4, -1}},
            {BlockType.T, new List<int>() {4, -1}}
        };

        // 方块变换时的中心点
        public static Dictionary<BlockType, List<List<int>>> ORIGIN = new Dictionary<BlockType, List<List<int>>>()
        {
            {
                BlockType.I, new List<List<int>>
                {
                    new List<int>() {1, -1},
                    new List<int>() {-1, 1}
                }
            },
            {
                BlockType.L, new List<List<int>>()
                {
                    new List<int>() {0, 0}
                }
            },
            {
                BlockType.J, new List<List<int>>()
                {
                    new List<int>() {0, 0}
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
                BlockType.O, new List<List<int>>()
                {
                    new List<int>() {0, 0}
                }
            },
            {
                BlockType.T, new List<List<int>>()
                {
                    new List<int>() {0, 0},
                    new List<int>() {0, 1},
                    new List<int>() {1, -1},
                    new List<int>() {-1, 0}
                }
            }
        };

        public BlockType       Type;
        public List<List<int>> Shape;
        public List<int>       XY;
        public int             RotateIndex;

        public Block(BlockType type, List<List<int>> shape, List<int> xy, int rotateIndex)
        {
            Type = type;
            Shape = shape;
            XY = xy;
            RotateIndex = rotateIndex;
        }

        public Block Fall(int step = 1)
        {
            return new Block(Type, Shape, new List<int>() {XY[0], XY[1] + step}, RotateIndex);
        }

        public Block Right()
        {
            return new Block(Type, Shape, new List<int>() {XY[0] + 1, XY[1]}, RotateIndex);
        }

        public Block Left()
        {
            return new Block(Type, Shape, new List<int>() {XY[0] - 1, XY[1]}, RotateIndex);
        }

        public Block Rotate()
        {
            var result = Enumerable.Range(0, Shape[0].Count).Select(i => null as List<int>).ToList();

            for (int row = 0; row < Shape.Count; row++)
            {
                for (int col = 0; col < Shape[row].Count; col++)
                {
                    if (result[col] == null)
                    {
                        result[col] = Enumerable.Range(0, Shape.Count).Select(index => 0).ToList();
                    }

                    result[col][row] = Shape[Shape.Count - 1 - row][col];
                }
            }

            var nextXy = new List<int>()
            {
                XY[0] + ORIGIN[Type][RotateIndex][0],
                XY[1] + ORIGIN[Type][RotateIndex][1]
            };

            var nextRotateIndex = RotateIndex + 1 >= ORIGIN[Type].Count ? 0 : RotateIndex + 1;

            return new Block(Type, result, nextXy, nextRotateIndex);
        }

        public bool IsValidInMatrix(List<List<int>> matrix)
        {
            if (XY[1] + Shape.Count > GameConstants.GAME_PAD_MATRIX_H ||
                XY[0] < 0 ||
                XY[0] + Shape[0].Count > GameConstants.GAME_PAD_MATRIX_W
            )
            {
                return false;
            }

            for (var i = 0; i < matrix.Count; i++)
            {
                var line = matrix[i];
                for (int j = 0; j < line.Count; j++)
                {
                    if (line[j] == 1 && Get(j, i) == 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int? Get(int x, int y)
        {
            x -= XY[0];
            y -= XY[1];
            if (x < 0 || x >= Shape[0].Count || y < 0 || y >= Shape.Count)
            {
                return null;
            }

            return Shape[y][x] == 1 ? 1 : (int?) null;
        }

        static Block FromType(BlockType type)
        {
            var shape = BLOCK_SHAPES[type];
            return new Block(type, shape, START_XY[type], 0);
        }

        public static Block GetRandom()
        {
            var i = Random.Range(0, 7);
            return FromType((BlockType) i);
        }
    }
}