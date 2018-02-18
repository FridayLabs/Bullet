using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletHolder : NetworkBehaviour {
	
	[SyncVar]
	public bool HasBullet = false;

	[Command]
	public void CmdGrabBullet() {
		HasBullet = true;
	}
	
	[Command]
	public void CmdLoseBullet() {
		HasBullet = false;
	}
}
