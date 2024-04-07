using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

using Object = UnityEngine.Object;

public static class TestHelpers
{
    public static GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    public static T LoadScriptableObject<T>(string scriptableObjectName) where T : ScriptableObject
    {
        return LoadResource<T>(typeof(T).FullName, scriptableObjectName);
    }

    public static T InstantiatePrefab<T>(string prefabName, Vector3 position) where T : MonoBehaviour
    {
        var prefab = LoadResource<T>("prefab", prefabName);
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }

    private static T LoadResource<T>(string typeName, string resourceName) where T: Object
    {
        string[] allGuids = AssetDatabase.FindAssets($"t:{typeName} {resourceName}");
        List<string> guids =  new List<string>();
        if (allGuids.Length == 0)
            Assert.Fail($"No {typeName} found named {resourceName}");

        foreach (var guid in allGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var name = path[(path.LastIndexOf('/') + 1)..path.LastIndexOf('.')];
            if (name.Equals(resourceName, StringComparison.OrdinalIgnoreCase))
                guids.Add(guid);
        }

        if (guids.Count > 1)
            Debug.LogWarning($"More than one {typeName} found named {resourceName}, taking first one");

        return (T)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(T));
    }
}
