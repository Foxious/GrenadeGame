using UnityEngine;
using System.Collections;

public class CommandKeysScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape)){
			Application.Quit();
		}
	
	}
}
