using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {

	public AudioMixer masterMixer;

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
