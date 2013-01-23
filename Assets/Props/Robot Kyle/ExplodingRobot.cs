using UnityEngine;
using System.Collections;

public class ExplodingRobot : ExplodingProp {
	
	public Rigidbody deadBot;
	public float blastStrength;
	
	public override void OnHit (Vector3 position, float blastRadius){
		base.OnHit(position, blastRadius);
		GameObject.Destroy(gameObject);
		Rigidbody newDeadBot = Instantiate(deadBot, transform.position, transform.rotation) as Rigidbody;
		newDeadBot.AddExplosionForce(blastStrength, position, blastRadius);
		GameObject.Destroy(newDeadBot.gameObject, 2.0f);
	} // end onHit
	
}
