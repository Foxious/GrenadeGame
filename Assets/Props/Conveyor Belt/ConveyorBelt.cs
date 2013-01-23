using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {
	
	public Transform startPosition;
	public float moveAmount;
	
	// Update is called once per frame
	void Update () {
		float moveAmt = moveAmount * Time.deltaTime;
		transform.position += new Vector3(moveAmt, 0.0f, 0);
		if (transform.localPosition.z > 40){
			transform.position = startPosition.transform.position;
		}
	}
}
