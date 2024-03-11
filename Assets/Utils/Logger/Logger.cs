using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Logger
{
    private static void DoLog(Action<string, Object> logFunction, string prefix, object myObj, params object[] msg)
    {
#if UNITY_EDITOR
        var name = GetObjectName(myObj, out var unityObject);
        logFunction($"{prefix}[{name}]: {String.Join("; ", msg)}\n ", unityObject ? unityObject : null);
#endif
    }

    private static string GetObjectName(object myObj, out Object unityObject)
    {
        var name = "";
        unityObject = null;

        if (myObj != null)
        {
            var isUnityObject = myObj.GetType() == typeof(Object);

            if (isUnityObject)
            {
                unityObject = (Object)myObj;
                name = unityObject.name.Color("#0077FF");
            }

            else
            {
                var objName = myObj.ToString();
                name = objName.Color("#0077FF");
            }
        }

        else
        {
            name = "Null Object".Color("#FF4747");
        }

        return name;
    }
    
    public static void TempLog(this object myObj, params object[] msg)
    {
        DoLog(Debug.Log, "", myObj, msg);
    }

    public static void Log(this object myObj, params object[] msg)
    {
        DoLog(Debug.Log, "", myObj, msg);
    }

    public static void LogError(this object myObj, params object[] msg)
    {
        DoLog(Debug.LogError, "<!>".Color("#FF4747"), myObj, msg);
    }

    public static void LogWarning(this object myObj, params object[] msg)
    {
        DoLog(Debug.LogWarning, "⚠️".Color("yellow"), myObj, msg);
    }

    public static void LogSuccess(this object myObj, params object[] msg)
    {
        DoLog(Debug.Log, "☻".Color("green"), myObj, msg);
    }
    
    private static string Color(this string myStr, string color)
    {
        return $"<color={color}>{myStr}</color>";
    }

}