using UnityEngine;
using System.Collections;

public class GrenadeController : MonoBehaviour {
	
	public ParticleEmitter explosion;
	public WindController wind;
	public Vector3 throwVector = new Vector3(0.0f, 0.0f, 0.0f);
	public float speed;
	public float grenadeLife;
	public float blastRadius;
	
	// Use this for initialization
	void Start () {
		time = 0.0f;
		startPos = this.transform.position;
	}
	
	void Update () {
		time += Time.deltaTime * speed;
		if (time > grenadeLife * speed){
			Explode();
		}
		else{
			Vector3 oldPos = transform.position;
			transform.position = startPos + (throwVector + wind.GetWindVector()) *time + Physics.gravity * time * time * 0.5f;
			Ray trajectorySlice = new Ray(oldPos, transform.position - oldPos);
			float distance = (transform.position - oldPos).magnitude * 3;
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(trajectorySlice,out hit, distance)){
				Explode();	
			}
		}
	}
	
	void Explode(){
		ParticleSystem blast = Instantiate(explosion, transform.position, Quaternion.identity) as ParticleSystem;
		Collider[] hits = Physics.OverlapSphere(transform.position, blastRadius); // maybe use a mask
		foreach (Collider c in hits){
			ExplodingProp target = c.GetComponent<ExplodingProp>();
			if (target != null){
				target.OnHit(transform.position, blastRadius);
			}
		}
		GameObject.Destroy(this.gameObject);
	}
	
	private void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, blastRadius);
	}
	
	private float time;
	private Vector3 startPos;
}
