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

        var camera = scene.GetRootGameObjects () [0];

        GameObject setupObj = new GameObject ("[SETUP]");
        setupObj.AddComponent<ObjectPooler>();

        var worldGO = new GameObject ("[WORLD]").transform;
        new GameObject ("[UI]");

        var camerasGO = new GameObject ("Cameras").transform;
        camerasGO.SetParent (worldGO);
        camera.transform.SetParent (camerasGO);
    }
}
