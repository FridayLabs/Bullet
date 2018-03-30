using NaughtyAttributes;
using UnityEngine;

public class AttackSounder : MonoBehaviour {

    [MinMaxSlider (1f, 3f)]
    [SerializeField]
    private Vector2 pitch = new Vector2 (1f, 2f);

    private AudioSource audioSource;

    private void Start () {
        audioSource = gameObject.AddComponent<AudioSource> ();
    }

    public void PlayAttackSound (Weapon weapon) {
        playSound (weapon.AttackSound, Random.Range (pitch.x, pitch.y));
    }

    public void PlayMisfireSound (Weapon weapon) {
        playSound (weapon.MisfireSound, Random.Range (pitch.x, pitch.y));
    }

    private void playSound (AudioClip clip, float pitch) {
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.Play ();
    }
}
