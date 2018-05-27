using NaughtyAttributes;
using UnityEngine;

public class FootstepsSounder : MonoBehaviour {

    [MinMaxSlider (1f, 3f)]
    [SerializeField]
    private Vector2 pitch = new Vector2 (1f, 2f);

    private AudioSource audioSource;

    private void Start () {
        audioSource = gameObject.AddComponent<AudioSource> ();
    }

    public void OnFootstep () {
        RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector3 (0, 0, -1));
        if (hit && hit.collider) {
            ConsistingOfSubstance consistingOfSubstance = hit.collider.GetComponent<ConsistingOfSubstance> ();
            if (consistingOfSubstance) {
                Substance substance = consistingOfSubstance.GetSubstance ();
                audioSource.clip = substance.FootstepsSound;
                audioSource.pitch = Random.Range (pitch.x, pitch.y);
                audioSource.Play ();
            } else {
                Debug.LogWarning ("Looks like there is no substance on ground " + hit.collider.name);
            }
        }
    }
}
