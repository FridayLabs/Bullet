using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stat {
    public string name;
    public float value;
}

public class StatsHolder : MonoBehaviour {
    public List<Stat> Stats = new List<Stat>();

    public float GetStatValue(string statName) {
        return FindStat(statName).value;
    }

    public void SetStatValue(string statName, float value) {
        Stat stat = FindStat(statName);
        stat.value = value;
    }

    private Stat FindStat(string statName) {
        foreach (var stat in Stats) {
            if (stat.name == statName) {
                return stat;
            }
        }
        throw new Exception("Stat " + statName + " does not exist");
    }
}