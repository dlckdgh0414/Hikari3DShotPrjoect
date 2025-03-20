using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class TypeLoader : MonoBehaviour
{
    public static T[] LoadType<T>(T type)
    {
        Type[] types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsSubclassOf(type.GetType()) && !t.IsAbstract)
            .ToArray();

        return types as T[];
    }
}
