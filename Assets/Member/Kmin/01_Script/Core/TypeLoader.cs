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

    /*public static ScriptableObject LoadScriptableObject<T>(T type, string name)
    {
        Type[] soTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(ScriptableObject)) 
                        && !t.IsAbstract && t.IsSubclassOf(type.GetType()))
            .ToArray();
        
        //return soTypes.Select()
    }*/
}
