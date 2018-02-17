public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {}
 
	void Awake () {
		DontDestroyOnLoad(this);
	}

}