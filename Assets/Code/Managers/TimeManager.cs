using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	public Text timerText;
	public Canvas findGameCanvas;

	private float secondsCount;
	private int minuteCount;

	void Update() {
		UpdateTimerUI();
	}

	//Calling this on Update
	public void UpdateTimerUI() {
		if(findGameCanvas.isActiveAndEnabled) {
			//Setting timer UI
			secondsCount += Time.deltaTime;

			if(secondsCount < 10) {
				timerText.text = minuteCount + ":0" + (int)secondsCount;
			} else {
				timerText.text = minuteCount + ":" + (int)secondsCount;
			}

			//Checking if it has already been a minute
			if(secondsCount >= 60) {
				minuteCount++;
				secondsCount = 0;
			}

			//If we're waiting more than 10mins we change time with infinity symbol
			if(minuteCount >= 10) {
				timerText.text = "∞";
			}

			//DELETE THIS! IT'S FOR PRESENTATION!
			if(secondsCount >= 5) {
				ApplicationManager.LoadLevel("Map1");
			}
		} else {
			secondsCount = 0f;
			minuteCount = 0;
		}
	}
}
