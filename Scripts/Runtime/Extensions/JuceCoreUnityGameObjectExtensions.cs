using UnityEngine;

public static class JuceCoreUnityGameObjectExtensions
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();

        if (component != null)
        {
            return component;
        }

        return go.AddComponent<T>();
    }

    public static GameObject Instantiate(this GameObject go, Transform parent = null)
    {
        return MonoBehaviour.Instantiate(go, parent);
    }

    public static T InstantiateAndGetComponent<T>(this GameObject go, Transform parent = null) where T : Component
    {
        GameObject goInstance = MonoBehaviour.Instantiate(go, parent);

        return goInstance.GetComponent<T>();
    }

    public static void SetParent(this GameObject go, GameObject parent, bool worldPositionStays = true)
    {
        if (parent == null)
        {
            go.transform.SetParent(null, worldPositionStays);
        }
        else
        {
            go.transform.SetParent(parent.transform, worldPositionStays);
        }
    }


    public static void SetParent(this GameObject go, Transform parent, bool worldPositionStays = true)
    {
        if (parent == null)
        {
            go.transform.SetParent(null, worldPositionStays);
        }
        else
        {
            go.transform.SetParent(parent, worldPositionStays);
        }
    }

    public static void RemoveParent(this GameObject go, bool worldPositionStays = true)
    {
        go.transform.SetParent(null, worldPositionStays);
    }
}