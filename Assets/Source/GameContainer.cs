using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public class GameContainer {
    private static GameContainer _instance;
//    private Dictionary<Type, object> _components = new Dictionary<Type, object>();

    [RuntimeInitializeOnLoadMethod]
    private static GameContainer getInstance() {
        if (_instance == null) {
            _instance = new GameContainer();
        }

        return _instance;
    }

}