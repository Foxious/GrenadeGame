using UnityEngine;
using System.Collections;

public class WindController : MonoBehaviour {
	
	public ProjectileTracer tracer;
	public FanSpin[] fans;
	public float minSpeed;
	public float maxSpeed;
	
	void Start(){
		SetNewWind ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPowered){
			return;
		}
		if (tracer.CanThrow()){
			if(resetWind){
				resetWind = false;
				SetNewWind();
			}
		}
		else{
			resetWind = true;
		}
	}
	
	public void KillPower(){
		isPowered = false;
		minSpeed = 0;
		maxSpeed = 0;
		SetNewWind();
		foreach (FanSpin f in fans){
			f.KillPower();
		}
	}
	
	public Vector3 GetWindVector(){
		return windVector;
	}
	
	public void SetNewWind(){
		windSpeed = Random.Range(minSpeed, maxSpeed);
		windVector = windSpeed * new Vector3(-1, 0, 0);
		foreach (FanSpin f in fans){
			f.spinSpeed = windSpeed;
		}
	}
	
	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Vector3 endpoint = transform.position + new Vector3(-3, 0, 0);
		Gizmos.DrawLine(transform.position, endpoint);
		Gizmos.DrawLine (endpoint, endpoint + new Vector3(.5f, .5f, 0f));
		Gizmos.DrawLine (endpoint, endpoint + new Vector3(.5f,-.5f, 0f));
		
	}
	
	private float windSpeed;
	private Vector3 windVector;
	private bool resetWind = false;
	private bool isPowered = true;
}
