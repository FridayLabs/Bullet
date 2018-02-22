using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container {

    private static Container _instance;
    
    private Dictionary<Type, object> _components = new Dictionary<Type, object>();

    [RuntimeInitializeOnLoadMethod]
    private static Container getInstance() {
        if (_instance == null) {
            _instance = new Container();
        }

        return _instance;
    }
    
    //TODO FFS. Research c# conatiner
}
