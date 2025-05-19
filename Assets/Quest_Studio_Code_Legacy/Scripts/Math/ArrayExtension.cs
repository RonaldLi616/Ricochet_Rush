using UnityEngine;
using System;

namespace Quest_Studio
{
    public class ArrayExtension
    {
        public static void RemoveElement<T>(ref T[] arr, int index)
        {
            for (int i = index; i < arr.Length - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            Array.Resize(ref arr, arr.Length - 1);
        }

        public static void SortArray<T>(ref T[] arr)
        {
            Array.Sort(arr);
        }

        public static void AddElement<T>(ref T[] arr, ref T element)
        {
            Array.Resize(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = element;
        }

        public static int FindElement<T>(ref T[] arr, ref T element)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(element))
                {
                    return i;
                }
            }
            return -1;
        }

        public static T GetRandomElement<T>(ref T[] arr)
        {
            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }
    }
}