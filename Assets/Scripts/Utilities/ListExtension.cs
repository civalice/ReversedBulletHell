using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class ListExtension
    {
        public static T RandomItem<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static void RandomRemove<T>(this List<T> list, int count)
        {
            while (list.Count > count)
            {
                list.RemoveAt(Random.Range(0, list.Count));
            }
        }
    }
}
