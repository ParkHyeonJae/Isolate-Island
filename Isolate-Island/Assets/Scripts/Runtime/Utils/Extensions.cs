using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T GetOrAddComponent<T>(this GameObject @object) where T : Behaviour
    {
        if (@object.TryGetComponent<T>(out T component))
            return component;
        return @object.AddComponent<T>();
    }

    public static IEnumerator<float> LerpTo(this float value, float destination)
    {
        var start = value;
        var end = destination;

        float t = 0;
        while (t <= 1.0f)
        {
            value = Mathf.Lerp(start, end, t);
            t += Time.deltaTime;

            yield return value;
        }
        value = destination;

        yield return value;
    }
}
