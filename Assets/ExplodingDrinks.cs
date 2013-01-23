using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ExplodingDrinks : ExplodingProp {
	
	public Rigidbody sodaCan;
	public Vector3 canSpawnPoint;
	public float spawnForce;
	public float canSpitRate;
	
	void Start(){
		isHit = false;
	}
	
	void Update(){
		if (isHit){
			timeElapsed += Time.deltaTime;
			if (timeElapsed > canSpitRate){
				timeElapsed -= canSpitRate;
				Rigidbody can = Instantiate(sodaCan, GetCanSpawnPoint(), transform.localRotation) as Rigidbody;
				can.AddForce(GetCanLaunchVector(), ForceMode.Impulse);
				GameObject.Destroy(can.gameObject, 3.0f);			
			}
		}
	}
	
	public override void OnHit(Vector3 position, float blastRadius){
		if (!isHit){
			isHit = true;
			GameObject.Destroy(gameObject, 3.0f);
		}
	}
			
	private Vector3 GetCanSpawnPoint(){
		return transform.localPosition + canSpawnPoint;
	}
	
	private Vector3 GetCanLaunchVector(){
		return transform.forward * spawnForce;
	}
	
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.grey;
		Gizmos.DrawRay(transform.localPosition + canSpawnPoint, GetCanLaunchVector());
	}
	
	private bool isHit;
	private float timeElapsed;
}
