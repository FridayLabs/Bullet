using System;
using System.Collections.Generic;
using UnityEngine;

public class GameContainer : MonoBehaviour {
    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object> ();

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize () {
        GameObject[] setups = GameObject.FindGameObjectsWithTag ("[SETUP]");
        foreach (GameObject go in setups) {
            Debug.LogWarning ("Please remove " + go.name + " from scene");
            Destroy (go);
        }
        SetupGameObject ();
    }

    public static GameObject SetupGameObject () {
        return singleton<GameObject> (delegate {
            GameObject setupGO = Instantiate (Resources.Load ("[SETUP]") as GameObject);
            DontDestroyOnLoad (setupGO);
            return setupGO;
        });
    }

    public static ObjectPooler ObjectPooler () {
        return singleton<ObjectPooler> (delegate {
            return SetupGameObject ().GetComponent<ObjectPooler> ();
        });
    }

    public static GameInputManager InputManager () {
        return singleton<GameInputManager> (delegate {
            return new GameInputManager ();
        });
    }

    public static Overviewer Overviewer () {
        return singleton<Overviewer> (delegate {
            return Camera.main.GetComponent<Overviewer> ();
        });
    }

    private static T singleton<T> (Func<T> constructor) {
        if (!services.ContainsKey (typeof (T))) {
            services.Add (typeof (T), constructor.Invoke ());
        }
        return (T) services[typeof (T)];
    }
}
