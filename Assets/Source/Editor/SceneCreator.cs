using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SceneCreator {

    static SceneCreator () {

        EditorSceneManager.newSceneCreated += CreateScene;
    }

    private static void CreateScene (Scene scene, NewSceneSetup setup, NewSceneMode mode) {
        GameObject camera = scene.GetRootGameObjects () [0];

        Transform worldGO = new GameObject ("[WORLD]").transform;

        new GameObject ("[STATIC]").transform.transform.SetParent (worldGO);

        Transform dynamicGO = new GameObject ("[DYNAMIC]").transform;
        dynamicGO.SetParent (worldGO);
        camera.transform.SetParent (dynamicGO);

        new GameObject ("[UI]");
    }
}
