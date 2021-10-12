using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OnCompleteLib
{



    public static Tween OnComplete(this Tween tween, TweenCallback action)
    {
        tween.onComplete += action;
        return tween;
    }
    public delegate void functionT<T>(T parameter);
    public static Tween OnComplete<T>(this Tween tween, functionT<T> action, T parameter)
    {
        tween.onComplete += () => action(parameter);
        return tween;
    }
    public static Tween ResetOnComplete(this Tween tween)
    {
        tween.onComplete = null;
        return tween;
    }


    public static IEnumerator OnCompleteAddToIEnumerator(this IEnumerator routine, Action onCompleteDo)
    {
        yield return routine;
        onCompleteDo();
    }

    /*
    public static void OnComplete(this Coroutine coroutine, Action onCompleteDo, MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(CheckFinish(coroutine,onCompleteDo));
    }
    private static IEnumerator CheckFinish(Coroutine coroutine, Action onCompleteDo)
    {
        while (coroutine.Running())
        {
            yield return null;
        }
        onCompleteDo();
    }
    */

    
    public static CoroutineExtended StartCoroutineX(this MonoBehaviour monoBehaviour, IEnumerator routine)
    {
        return new CoroutineExtended().StartCoroutine(monoBehaviour, routine);
    }
}

public class CoroutineExtended
{
    private Coroutine _coroutine;
    private Action _onComplete;

    public CoroutineExtended()
    {
    }

    public static implicit operator Coroutine(CoroutineExtended d) => d._coroutine;


    private IEnumerator SetupCoroutine(IEnumerator routine)
    {
        yield return routine;
        _onComplete?.Invoke();
    }

    public CoroutineExtended StartCoroutine(MonoBehaviour monoBehaviour, IEnumerator routine)
    {
        _coroutine = monoBehaviour.StartCoroutine(SetupCoroutine(routine));
        return this;
    }

    public CoroutineExtended OnComplete(Action action)
    {
        _onComplete += action;
        return this;
    }
    public CoroutineExtended OnComplete<T>(OnCompleteLib.functionT<T> action, T parameter)
    {
        _onComplete += () => action(parameter);
        return this;
    }

    public CoroutineExtended ResetOnComplete()
    {
        _onComplete = null;
        return this;
    }
}
