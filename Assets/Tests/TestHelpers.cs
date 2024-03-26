using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        var prefab = LoadResource<T>("Prefab", prefabName);
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }

    private static T LoadResource<T>(string typeName, string resourceName) where T: Object
    {
        string[] guids = AssetDatabase.FindAssets($"t:{typeName} {resourceName}");
        if (guids.Length == 0)
            Assert.Fail($"No {typeName} found named {resourceName}");

        if (guids.Length > 1)
            Debug.LogWarning($"More than one {typeName} found named {resourceName}, taking first one");

        return (T)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(T));
    }
}
