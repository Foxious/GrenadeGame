using UnityEngine;
using System.Collections;

public class GeneratorExplosion : ExplodingProp {
	
	public WindController wind;
	public float shakeDuration;
	public ParticleEmitter explosion;
	
	void Start(){
	}
	
	void Update(){
		if (isHit && shakeDuration > 0){
			shakeDuration -= Time.deltaTime;
			ShakeScreen(3, 100);
		}
		
	}
	public override void OnHit (Vector3 position, float blastRadius){
		if (!isHit){
			base.OnHit (position, blastRadius);
			wind.KillPower();
			isHit = true;
			Instantiate (explosion, transform.position, Quaternion.identity);
		}
	}	
	private bool isHit = false;
}
