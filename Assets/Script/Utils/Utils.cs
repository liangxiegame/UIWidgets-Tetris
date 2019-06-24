using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisApp
{
    public static class Utils
    {
        public static List<T> CreateFilledList<T>(int count, Func<int, T> fill)
        {
            return Enumerable.Range(0, count).Select(index => fill(index)).ToList();
        }

        public static List<List<T>> Create2DList<T>(int rowCount, int colCount, Func<int, int, T> fill)
        {
            return CreateFilledList(rowCount, i => CreateFilledList(colCount, j => fill(i, j)));
        }

        public static void ForEachTable<T>(this List<List<T>> table, Func<int, int, T> fill)
        {
            for (var i = 0; i < table.Count; i++)
            {
                for (var j = 0; j < table[i].Count; j++)
                {
                    table[i][j] = fill(i, j);
                }
            }
        }
    }
}