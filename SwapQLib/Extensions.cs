using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SwapQLib
{
    internal static class Extensions
    {
        internal static string EnsureSize(this string input, int size)
        {
            if (input.Length < size)
                input = input.PadRight(size);
            else if (input.Length > size)
                input = input.Substring(0, size);
            return input;
        }

        internal static IEnumerable<DataRow> AsGeneric(this DataRowCollection collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                yield return collection[i];
            }
        }

        internal static IEnumerable<DataColumn> AsGeneric(this DataColumnCollection collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                yield return collection[i];
            }
        }
    }
}
