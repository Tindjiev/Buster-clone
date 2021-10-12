using System;
using System.Collections;
using UnityEngine;

public static class GenericLib
{
    public static T As<T>(this object toDownCast) where T : class
    {
        return toDownCast as T;
    }

    public static void As<T>(this object toDownCast, out T returnVariable) where T : struct
    {
        try
        {
            returnVariable = (T)toDownCast;
        }
        catch (InvalidCastException)
        {
            returnVariable = default;
        }
    }


    public static CoroutineExtended DoActionInTime(this MonoBehaviour monoBehaviour, Action action, float seconds)
    {
        return monoBehaviour.StartCoroutineX(DoInSeconds(action, seconds));
    }
    public static CoroutineExtended DoActionInTimeRepeating(this MonoBehaviour monoBehaviour, Action action, float seconds)
    {
        return monoBehaviour.StartCoroutineX(DoInSeconds(action, seconds, seconds));
    }
    public static CoroutineExtended DoActionInTimeRepeating(this MonoBehaviour monoBehaviour, Action action, float secondsStart, float secondsRepeating)
    {
        return monoBehaviour.StartCoroutineX(DoInSeconds(action, secondsStart, secondsRepeating));
    }
    public static CoroutineExtended DoActionInNextFrame(this MonoBehaviour monoBehaviour, Action action)
    {
        return monoBehaviour.StartCoroutineX(DoNextFrame(action));
    }

    private static IEnumerator DoInSeconds(Action action, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }
    private static IEnumerator DoInSeconds(Action action, float secondsStart, float secondsRepeat)
    {
        yield return DoInSeconds(action, secondsStart);
        while (true)
        {
            yield return new WaitForSeconds(secondsRepeat);
            action();
        }
    }
    private static IEnumerator DoNextFrame(Action action)
    {
        yield return null;
        yield return new WaitForEndOfFrame();
        action();
    }
}
