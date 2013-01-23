using UnityEngine;
using System.Collections;

public class ExplodingProp : MonoBehaviour {
	
	public ScoreTracker tracker;
	public float Score;
	
	public virtual void OnHit(Vector3 position, float blastRadius){
		tracker.AddScore(Score);
	}
	
	public void ShakeScreen(float strength, float speed){
		Camera.main.transform.Translate(new Vector3(Mathf.Sin (Time.time * speed) * strength, 0, 0), Space.Self);
	}
}
