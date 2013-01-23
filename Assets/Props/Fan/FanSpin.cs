using UnityEngine;
using System.Collections;

public class FanSpin : MonoBehaviour {
	
	public float spinSpeed;
	public float speedDecrease;
	public ParticleEmitter smoke;
	public Vector3 smokePos;
	public WindTrail windTrail;
	public float trailSpawnRate; // trails / second
	public float trailLengthMult;

	// Use this for initialization
	void Start () {
		isPowered = true;
		timePerTrail = 1 / trailSpawnRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPowered){
			spinSpeed = Mathf.Max (spinSpeed-speedDecrease * Time.deltaTime, 0.0f);
		}
		else{
			RenderTrail();
		}
		transform.Rotate(0,spinSpeed, 0);
	}
	
	public void KillPower(){
		isPowered = false;
		if (smoke != null){
			Instantiate(smoke, smokePos + transform.position, Quaternion.identity);
		}
	}
	
	private void RenderTrail(){
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= timePerTrail){
			elapsedTime -= timePerTrail;
			Vector3 startPosition = transform.position + smokePos;
			float y = Random.Range(-4.0f, 4);
			float z = Random.Range(-4.0f, 4);
			startPosition += new Vector3(-5, y, z);
			WindTrail trail = Instantiate(windTrail, startPosition, Quaternion.identity) as WindTrail;
			trail.speed = spinSpeed * -trailLengthMult;
			GameObject.Destroy (trail, Mathf.Abs(spinSpeed));
			
		}
		
	}
	
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Vector3 lineEnd = new Vector3(2, 0, 0) + transform.position + smokePos;
		Gizmos.DrawLine(transform.position + smokePos, lineEnd);
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(0.5f, 0.5f, 0));
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(0.5f, -0.5f, 0));
		
		Gizmos.color = Color.green;
		lineEnd = new Vector3(0, 2, 0) + transform.position + smokePos;
		Gizmos.DrawLine (transform.position + smokePos, lineEnd);
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(0.5f, 0.5f, 0));
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(-0.5f, 0.5f, 0));
		
		Gizmos.color = Color.blue;
		lineEnd = new Vector3(0, 0, 2) + transform.position + smokePos;
		Gizmos.DrawLine (transform.position + smokePos, lineEnd);
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(0, 0.5f, 0.5f));
		Gizmos.DrawLine(lineEnd, lineEnd - new Vector3(0, -0.5f, 0.5f));
	}
	
	private bool isPowered;
	private float timePerTrail;
	private float elapsedTime = 0;
}
