using UnityEngine;
using System.Collections;

public class ForkliftHit : ExplodingProp {
	
	public DeadForklift deadlift;
	public Transform deadliftTarget;
	
	public override void OnHit (Vector3 position, float blastRadius){
		base.OnHit (position, blastRadius);
		DeadForklift deadliftInst = Instantiate(deadlift, transform.position, transform.rotation) as DeadForklift;
		deadliftInst.DriveLocation = deadliftTarget;
		GameObject.Destroy(this.gameObject);
		GameObject.Destroy (deadliftInst.gameObject, 2.0f);
	}
	

}
