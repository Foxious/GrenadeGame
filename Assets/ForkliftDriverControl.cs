using UnityEngine;
using System.Collections;

public class ForkliftDriverControl : MonoBehaviour {
	
	public Transform forklift;
	public float radius;
	public float speed;
	public float startTime;
	// Use this for initialization
	void Start () {
		//forkLiftInst = Instantiate(forklift, transform.position, Quaternion.identity) as Transform;
		totalTime = startTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (forklift == null){
			GameObject.Destroy(gameObject);
			return;
		}
		totalTime += Time.deltaTime;
		UpdatePosition(totalTime);
	}
		
	private void UpdatePosition(float time){
		Vector3 pos = PositionAtTime (time);
		forklift.position = pos * radius + transform.position;
		Vector3 nextPosVec = PositionAtTime(time+0.1f) - pos;
		forklift.rotation = Quaternion.LookRotation(nextPosVec);
	}
	
	private Vector3 PositionAtTime(float time){
		float x = Mathf.Cos(time*speed);
		float z = Mathf.Sin(time*speed);
		return new Vector3(x, 0 , z);
	}
	
	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, new Vector3(2, 2, 2));
	}
	
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radius);
		float x = Mathf.Cos(startTime);
		float z = -Mathf.Sin(startTime);
		Vector3 startBox = new Vector3(x, 0, z);
		Gizmos.DrawWireCube(transform.position + startBox*radius, new Vector3(1, 1, 1));
	}
	
	//private Transform forkLiftInst;
	private float totalTime = 0.0f;
}
