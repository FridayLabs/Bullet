using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour {
    public UnityEvent OnRoundStart = new UnityEvent();
    public UnityEvent OnRoundEnd = new UnityEvent();

    public int CurrentRoundNumber = 1;

    public void StartNextRound() {
        if (CurrentRoundNumber > 1) {
            OnRoundEnd.Invoke();
        }

        CurrentRoundNumber++;
        OnRoundStart.Invoke();
    }
}