using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;

	public void QuitRequest() {
		//If we are running in a standalone build of the game
    #if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
    #endif
 
        //If we are running in the editor
    #if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
	}

	//In case we have a splash screen
	//We invoke next scene after the splash in autoLoadNextLevelAfter seconds
	void Start () {
		if (autoLoadNextLevelAfter <= 0) {
			Debug.Log ("Level auto load disabled, use a postive number is seconds");
		} else {
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}

	//Loading a scene with a specific name
	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		SceneManager.LoadScene (name);
	}

	//Loading a next scene in build
	public void LoadNextLevel() {
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
