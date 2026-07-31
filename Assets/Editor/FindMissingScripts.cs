using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public static class FindMissingScripts
{
    [MenuItem("Tools/Find Missing Scripts In Scene")]
    public static void FindInScene()
    {
        int goCount = 0;
        int componentsCount = 0;
        int missingCount = 0;

        Scene scene = SceneManager.GetActiveScene();
        GameObject[] rootObjects = scene.GetRootGameObjects();

        foreach (GameObject g in rootObjects)
        {
            FindInGameObject(g, ref goCount, ref componentsCount, ref missingCount);
        }

        Debug.Log($"Scanned scene '{scene.name}': {goCount} GameObjects, {componentsCount} components checked, {missingCount} missing scripts found.");
    }






    private static void FindInGameObject(GameObject g, ref int goCount, ref int componentsCount, ref int missingCount)
    {
        goCount++;
        Component[] components = g.GetComponents<Component>();

        for (int i = 0; i < components.Length; i++)
        {
            componentsCount++;
            if (components[i] == null)
            {
                missingCount++;
                Debug.LogWarning($"Missing script found on GameObject: {GetFullPath(g)} (component number #{i})", g);
            }
        }

        // Recurse into children
        foreach (Transform child in g.transform)
        {
            FindInGameObject(child.gameObject, ref goCount, ref componentsCount, ref missingCount);
        }
    }





    private static string GetFullPath(GameObject g)
    {
        string path = g.name;
        Transform parent = g.transform.parent;
        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }
        return path;
    }
}
