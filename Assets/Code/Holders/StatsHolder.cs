using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public struct Stat {
    public string name;
    public float value;

    public Stat(string name, float value) {
        this.name = name;
        this.value = value;
    }
}

public class StatList : SyncListStruct<Stat> {}

public class StatsHolder : NetworkBehaviour {

    [SyncVar] public float HP = 1f;
    
    public StatList Stats = new StatList();

    void Start() {
        Stats.Add(new Stat("HP", 1));
        Stats.Add(new Stat("Movespeed", 9));
    }
    
    public float GetStatValue(string statName) {
        if (statName == "HP") {
            return HP;
        }
        return Stats[FindStatIdx(statName)].value;
    }

    public void SetStatValue(string statName, float value) {
        if (statName == "HP") {
            HP = value;
            return;
        }
        
        Stat stat = Stats[FindStatIdx(statName)];
        stat.value = value;
    }
    
    private int FindStatIdx(string statName) {
        for (var i = 0; i < Stats.Count; i++) {
            if (Stats[i].name == statName) {
                return i;
            }
        }
        throw new Exception("Stat " + statName + " does not exist");
    }

    public void PrintStats() {
        for (var i = 0; i < Stats.Count; i++) {
            Debug.Log(Stats[i].name + ": " + Stats[i].value);
        }
    }
}