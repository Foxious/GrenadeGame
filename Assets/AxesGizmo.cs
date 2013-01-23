using UnityEngine;
using System.Collections;

public class AxesGizmo : MonoBehaviour {
	
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Vector3 lineEnd = new Vector3(2, 0, 0) + transform.position;
		Gizmos.DrawLine(transform.position, lineEnd);
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(0.5f, 0.5f, 0));
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(0.5f, -0.5f, 0));
		
		Gizmos.color = Color.green;
		lineEnd = new Vector3(0, 2, 0) + transform.position;
		Gizmos.DrawLine (transform.position, lineEnd);
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(0.5f, 0.5f, 0));
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(-0.5f, 0.5f, 0));
		
		Gizmos.color = Color.blue;
		lineEnd = new Vector3(0, 0, 2) + transform.position;
		Gizmos.DrawLine (transform.position, lineEnd);
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(0, 0.5f, 0.5f));
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(0, -0.5f, 0.5f));
	}
}
			
			
