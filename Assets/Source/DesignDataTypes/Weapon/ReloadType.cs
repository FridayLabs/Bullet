using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu (fileName = "ReloadType", menuName = "Weapon/ReloadType")]
public class ReloadType : ScriptableObject {

    public bool Batch = true;

    public IEnumerator Reload (Weapon weapon, int count, Action start, Func<int, bool> reload, Action finish) {
        float reloadCD = weapon.ReloadPerIteration / 1000;
        start ();
        yield return new WaitForSeconds (reloadCD);
        for (int i = 0; i < (Batch ? 1 : count); i++) {
            reload ((Batch ? count : 1));
            yield return new WaitForSeconds (reloadCD);
        }
        finish ();
    }

    public IEnumerator Unload (Weapon weapon, Action start, Func<int, bool> unload, Action finish) {
        float reloadCD = weapon.UnloadPerIteration / 1000;
        int count = weapon.CurrentAmmoCount;
        start ();
        yield return new WaitForSeconds (reloadCD);
        for (int i = 0; i < (Batch ? 1 : count); i++) {
            unload ((Batch ? count : 1));
            yield return new WaitForSeconds (reloadCD);
        }
        finish ();
    }
}
