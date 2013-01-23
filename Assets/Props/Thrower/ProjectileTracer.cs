using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer)) ]
public class ProjectileTracer : MonoBehaviour {
	
	public Transform grenade;
	public Camera gameCamera;
	public Projector projector;
	public ScoreTracker score;
	public WindController wind;
	
	public Vector3 throwDirection;
	
	public float cameraMoveOffset;
	public float maxCamHeight;
	public float minCameight;
	public float force;
	public float turnThreshold;
	public float turnSpeed;
	public float minHeight;
	public float maxHeight;
	
	public int numGrenades;
	
	void Start(){
		turnDegrees = 0.0f;
		thisGrenade = null;
		throwArc = GetComponent<LineRenderer>();
		score.SetGrenades(numGrenades);
	}
	
	// Update is called once per frame
	void Update () {
		if (CanThrow()){
			throwArc.enabled = true;
			GetArc();
			MoveCamera ();
			PlotTrajectory(0.10f, 6.0f);
			ThrowGrenade();
		}
		else{
			throwArc.enabled = false;
		}
	}
	
	private void MoveCamera(){
		float gameCameraY = gameCamera.transform.position.y;
		float moveAmount = Input.GetAxis ("Mouse Y") * cameraMoveOffset;
		if ((moveAmount > 0.0f && gameCameraY > maxCamHeight) ||
			(moveAmount < 0.0f && gameCameraY < minCameight) ){
			moveAmount = 0.0f;
		}
		Vector3 offset = new Vector3(0.0f,  moveAmount, 0.0f);
		gameCamera.transform.position += offset;	
	}
	
	private void GetArc(){
		float height = throwDirection.y + Input.GetAxis("Mouse Y");
		height = Mathf.Min(height, maxHeight);
		height = Mathf.Max(minHeight, height);
		turnDegrees += Input.GetAxis("Mouse X") * turnSpeed;
		turnDegrees = Mathf.Min (turnThreshold, turnDegrees);
		turnDegrees = Mathf.Max (-turnThreshold, turnDegrees);
		Vector2 turnVec = new Vector2(Mathf.Sin(turnDegrees), Mathf.Cos(turnDegrees));
		throwDirection = new Vector3(turnVec.x, height, turnVec.y);
	}
	
	private void ThrowGrenade(){
		if ( Input.GetButtonUp("Fire1")){
			thisGrenade = Instantiate(grenade, transform.position, Quaternion.identity) as Transform;
			GrenadeController controller = thisGrenade.GetComponent<GrenadeController>();
			controller.wind = wind;
			controller.throwVector = throwDirection * force;
			--numGrenades;
			score.SetGrenades(numGrenades);
		}
	}
	
	public bool CanThrow(){
		if (thisGrenade == null){
			if (numGrenades > 0){
				return true;
			}
			score.SetGameOver();
		}
		return false;
	}
	
	public Vector3 GetTrajectoryAtTime(float time){
		return transform.position + (throwDirection * force)*time + Physics.gravity * time * time * 0.5f;
	}
	
	public void PlotTrajectory(float timestep, float maxtime){
		Vector3 prev = transform.position;
		RaycastHit hitInfo = new RaycastHit();
		int steps = (int)(maxtime / timestep);
		throwArc.SetVertexCount(steps);
		for (int i = 0;i < steps;++i){
			float t = timestep*i;
			Vector3 pos = GetTrajectoryAtTime(t);
			if(Physics.Linecast(prev, pos, out hitInfo)){
				SetProjector (hitInfo.point.x, hitInfo.point.z);
				throwArc.SetVertexCount(i);
				return;
			}
			throwArc.SetPosition(i, pos);
			prev = pos;
		}
		SetProjector(prev.x, prev.z);
	}
	
	private void SetProjector(float x, float z){
		projector.transform.position = new Vector3(x, projector.transform.position.y, z);
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawRay(transform.position, throwDirection);
	}
	
	private float turnDegrees;
	private Transform thisGrenade;
	private LineRenderer throwArc;
}
