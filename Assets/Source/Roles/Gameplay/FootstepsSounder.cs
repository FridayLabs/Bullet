using UnityEngine;

public class FootstepsSounder : MonoBehaviour {

    [SerializeField]
    [Range(1f, 3f)]
    private float minPitchValue = 1f;

    [SerializeField]
    [Range(1f, 3f)]
    private float maxPitchValue = 2f;

    private AudioSource audioSource;

    private void Start() {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnFootstep() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit && hit.collider) {
            ConsistingOfSubstance consistingOfSubstance = hit.collider.GetComponent<ConsistingOfSubstance>();
            if (consistingOfSubstance) {
                Substance substance = consistingOfSubstance.GetSubstance();
                audioSource.clip = substance.FootstepsSound;
                audioSource.pitch = Random.Range(minPitchValue, maxPitchValue);
                audioSource.Play();
            } else {
                Debug.LogWarning("Looks like there is no substance on ground " + hit.collider.name);
            }
        }
    }
}
