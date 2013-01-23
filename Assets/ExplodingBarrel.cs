using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ExplodingBarrel : ExplodingProp {
	
	public ParticleEmitter explosion;
	public float explosionRadius;
	public float explosionStrength;
	public float cameraShakeDuration;
	
	public void Start(){
		isHit = false;
	}
	
	public void Update(){
		if (isHit){
			timeElapsed += Time.deltaTime;
			if (timeElapsed < cameraShakeDuration){
				ShakeScreen(1, 100);
			}
		}
	}
	
	public override void OnHit (Vector3 position, float blastRadius){
		if (!isHit){
			isHit = true;
			base.OnHit(position, blastRadius);
			ParticleEmitter blast = Instantiate(explosion, transform.position, Quaternion.identity) as ParticleEmitter;
			rigidbody.AddExplosionForce(explosionStrength * 65, position, blastRadius);
			// explosion chain
			Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius); // maybe use a mask
			foreach (Collider c in hits){
				ExplodingProp p = c.GetComponent<ExplodingProp>();
				if (p != null){
					p.OnHit(transform.position, explosionStrength);
					tracker.AddScore(p.Score * (Score / 100));
				}
			} // end foreach
			GameObject.Destroy(this.gameObject, 3.0f);
		} // end if
	}
	
	private void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
	
	private bool isHit;
	private float timeElapsed = 0;
}
