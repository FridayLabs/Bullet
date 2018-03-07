using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepsSounder : MonoBehaviour {

    public void OnFootstep() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit && hit.collider) {
            Substance s = hit.collider.gameObject.GetComponent<ConsistingOfSubstance>().GetSubstance();

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = s.FootstepsSound;
//            audioSource.pitch = Random.Range(0, 1); // TODO
            audioSource.Play();
        }
    }
}