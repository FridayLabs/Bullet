using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {

	public AudioMixer masterMixer;
	public AudioSource audioSource;

	private static bool IsAlreadyCreated = false;

	void Start() {

		Debug.Log("KEKEKE");
		Debug.Log(MusicManager.IsAlreadyCreated);

		if (MusicManager.IsAlreadyCreated) {
			Destroy(gameObject);
			return;
		}

		MusicManager.IsAlreadyCreated = true;
		DontDestroyOnLoad(this);


	}

	public void SetMasterLevel (float masterLevel) {
		masterMixer.SetFloat("MasterVol", masterLevel);
	}

	public void SetMusicLevel (float musicLevel) {
		masterMixer.SetFloat("MusicVol", musicLevel);
	}

	public void SetSFXLevel (float sfxLevel) {
		masterMixer.SetFloat("SFXVol", sfxLevel);
	}
}
