using System.Collections.Generic;
using UnityEngine;


public static class GetVars
{
    public static T GetvarsInChildren<T>(Transform tr) where T : Component
    {
        T tempvars;
        int len = tr.childCount;
        for (int i = 0; i < len; i++)
        {
            if ((tempvars = GetvarsInChildren<T>(tr.GetChild(i))) != null)
            {
                return tempvars;
            }
        }
        return null;
    }

    public static T GetOtherChildComponent<T>(Transform tr) where T : Component
    {
        if (tr.parent != null)
        {
            return tr.parent.GetComponentInChildren<T>();
        }
        return null;
    }

    public static T[] GetComponentsInDirectChildren<T>(this Component component) where T : Component
    {
        if (component == null)
        {
            return null;
        }
        List<T> components = new List<T>();
        for (int i = 0; i < component.transform.childCount; ++i)
        {
            T TEMPcomponent = component.transform.GetChild(i).GetComponent<T>();
            if (TEMPcomponent != null)
                components.Add(TEMPcomponent);
        }

        return components.ToArray();
    }


    public static T getvars<T>(this Component component) where T : Component
    {
        if (component == null)
        {
            return null;
        }
        Transform tr = component.transform;
        T tempvars;
        if ((tempvars = tr.GetComponentInChildren<T>()) != null)
        {
            return tempvars;
        }
        while (tr != null)
        {
            if (tr.TryGetComponent(out tempvars))
            {
                return tempvars;
            }
            else if (tr.parent == null)
            {
                return tr.GetComponentInChildren<T>(true);
            }
            tr = tr.parent;
        }
        return null;
    }


    public static Transform getvarsTR<T>(this Component component) where T : Component
    {
        if (component == null)
        {
            return null;
        }
        Transform tr = component.transform;
        while (tr != null)
        {
            T tempvars;
            if (tr.TryGetComponent<T>(out _))
            {
                return tr;
            }
            else if (tr.parent == null)
            {
                if ((tempvars = tr.GetComponentInChildren<T>(true)) != null)
                {
                    return tempvars.transform;
                }
                return null;
            }
            else if (tr.parent != null && tr.parent.GetComponent<T>() != null)
            {
                return tr.parent;
            }
            tr = tr.parent;
        }
        //return component.transform;
        return null;
    }


    public static T[] getmanyvars<T>(this Component component) where T : Component
    {
        if (component == null)
        {
            return null;
        }
        T[] tempvars;
        T[] varsfound = new T[0];
        Transform tr = component.transform;
        while (tr != null)
        {
            if ((tempvars = tr.GetComponents<T>()).Length != 0)
            {
                ArrayLib.ExpandArray(ref varsfound, tempvars);
            }
            if ((tempvars = tr.GetComponentsInDirectChildren<T>()).Length != 0)
            {
                ArrayLib.ExpandArray(ref varsfound, tempvars);
            }
            tr = tr.parent;
        }
        return varsfound;
    }




    public static T[] GetComponentsInDirectChildren<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject == null)
        {
            return null;
        }
        List<T> components = new List<T>();
        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            T component = gameObject.transform.GetChild(i).GetComponent<T>();
            if (component != null)
                components.Add(component);
        }

        return components.ToArray();
    }



    public static T getvars<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject == null)
        {
            return null;
        }
        return gameObject.transform.getvars<T>();
    }

    public static Transform getvarsTR<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject == null)
        {
            return null;
        }
        return gameObject.transform.getvarsTR<T>();
    }

    public static T[] getmanyvars<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject == null)
        {
            return null;
        }
        return gameObject.transform.getmanyvars<T>();
    }




}



public static class ComponentExtensions
{


    public static void disable(this Behaviour Behaviour)
    {
        Behaviour.enabled = false;
    }
    public static void enable(this Behaviour Behaviour)
    {
        Behaviour.enabled = true;
    }
    public static void disable(this Renderer Renderer)
    {
        Renderer.enabled = false;
    }
    public static void enable(this Renderer Renderer)
    {
        Renderer.enabled = true;
    }

}

public static class GameObjectExtensions
{

    public static void SetActivetrue(this GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public static void SetActivefalse(this GameObject gameObject)
    {
        gameObject.SetActive(false);
    }


}


