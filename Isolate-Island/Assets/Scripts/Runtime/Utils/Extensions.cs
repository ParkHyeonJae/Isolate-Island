using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T GetOrAddComponent<T>(this GameObject @object) where T : MonoBehaviour
    {
        if (@object.TryGetComponent<T>(out T component))
            return component;
        return @object.AddComponent<T>();
    }
}
