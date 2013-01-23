using UnityEngine;
using System.Collections;

public class DeadForklift : ExplodingProp {
	
	public Transform DriveLocation;
	public float turnTime;
	
	void Start(){
		startDirection = transform.rotation;
		turnDirection = Quaternion.LookRotation(DriveLocation.position);
		moveVector = DriveLocation.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		elapsed += Time.deltaTime;
		transform.rotation = Quaternion.Lerp(startDirection, turnDirection, elapsed / turnTime);
		transform.position += ( moveVector * (Time.deltaTime/2) );
		ShakeScreen(2, 100);
	}
	
	void OnCollisionEnter(Collision other){
		ExplodingProp p = other.gameObject.GetComponent<ExplodingProp>();
		if (p != null){
			p.OnHit(transform.position, 10);
			tracker.AddScore(p.Score * (Score / 10));
		}
	}
	
	private float elapsed;
	private Quaternion startDirection;
	private Quaternion turnDirection;
	private Vector3 moveVector;
	private float unitsPerSecond;
}
