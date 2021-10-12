using System;
using System.Collections.Generic;
using System.Linq;

public static class ArrayLib
{
    public static void ExpandArray<Type>(ref Type[] Array, Type ToAdd)
    {
        if (Array == null)
        {
            Array = new Type[] { ToAdd };
            return;
        }
        int mainlen = Array.Length;
        System.Array.Resize(ref Array, mainlen + 1);
        Array[mainlen] = ToAdd;
    }
    public static void ExpandArray<Type>(ref Type[] MainArray, Type[] ArrayToAdd)
    {
        if (MainArray == null)
        {
            MainArray = new Type[0];
        }
        if (ArrayToAdd == null)
        {
            return;
        }
        int mainlen = MainArray.Length;
        System.Array.Resize(ref MainArray, MainArray.Length + ArrayToAdd.Length);
        for (int i = 0; i < ArrayToAdd.Length; i++)
        {
            MainArray[i + mainlen] = ArrayToAdd[i];
        }
    }

    public static T[] RandomizeArray<T>(T[] arrayToRandomize)
    {
        T[] array = (T[])arrayToRandomize.Clone();
        RandomizeArray(ref array);
        return array;
    }

    public static List<T> RandomizeArray<T>(List<T> listToRandomize)
    {
        List<T> list = new List<T>(listToRandomize);
        RandomizeArray(ref list);
        return list;
    }
    public static void RandomizeArray<T>(ref T[] arrayToRandomize)
    {
        for (int i = 0; i < arrayToRandomize.Length; i++)
        {
            int rand = UnityEngine.Random.Range(i, arrayToRandomize.Length);
            T temp = arrayToRandomize[rand];
            arrayToRandomize[rand] = arrayToRandomize[i];
            arrayToRandomize[i] = temp;
        }
    }
    public static void RandomizeArray<T>(ref List<T> listToRandomize)
    {
        for (int i = 0; i < listToRandomize.Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, listToRandomize.Count);
            T temp = listToRandomize[rand];
            listToRandomize[rand] = listToRandomize[i];
            listToRandomize[i] = temp;
        }
    }

    public static T GetRandomElement<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
    public static T GetRandomElement<T>(this List<T> array)
    {
        return array[UnityEngine.Random.Range(0, array.Count)];
    }


    public static void RemoveAt<Type>(ref Type[] Array, int i)
    {
        if (Array == null)
        {
            return;
        }
        for (++i; i < Array.Length; ++i)
        {
            Array[i - 1] = Array[i];
        }
        System.Array.Resize(ref Array, Array.Length - 1);
    }

    public static void ForEach<T>(this IEnumerable<T> ienumerable, MethodTypes.voidfunctionT<T> action)
    {
        foreach(T element in ienumerable)
        {
            action(element);
        }
    }

}


public static class StringLib
{
    public static string Capitalize1stLetter(this string str)
    {
        return char.ToUpper(str[0]) + str.Substring(1);
    }
}