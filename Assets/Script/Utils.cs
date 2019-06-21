using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisApp
{
    public class Utils
    {
        public static List<T> CreateFilledList<T>(int count, Func<int, T> fill)
        {
            return Enumerable.Range(0, count).Select(index => fill(index)).ToList();
        }

        public static List<List<T>> Create2DList<T>(int rowCount, int colCount, Func<int, int, T> fill)
        {
            return CreateFilledList(rowCount, i => CreateFilledList(colCount, j => fill(i, j)));
        }
    }
}