using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    [System.Serializable]
    public class Pool {
        public GameObject Prefab;
        public int Size;
    }

    public List<Pool> pools;
    public Dictionary<GameObject, Queue<GameObject>> poolDictionary;

    private void Start () {
        poolDictionary = new Dictionary<GameObject, Queue<GameObject>> ();
        foreach (Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject> ();
            for (int i = 0; i <= pool.Size; i++) {
                GameObject go = Instantiate (pool.Prefab);
                go.SetActive (false);
                objectPool.Enqueue (go);
            }
            poolDictionary.Add (pool.Prefab, objectPool);
        }
    }

    public GameObject Spawn (GameObject prefab, Vector3 position, Quaternion rotation) {

        if (!poolDictionary.ContainsKey (prefab)) {
            Debug.LogWarning ("Pool with prefab " + prefab + " does not exist");
            return null;
        }

        GameObject obj = poolDictionary[prefab].Dequeue ();

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive (true);

        poolDictionary[prefab].Enqueue (obj);

        return obj;
    }
}
