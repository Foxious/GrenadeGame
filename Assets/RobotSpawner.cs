using UnityEngine;
using System.Collections;

public class RobotSpawner : MonoBehaviour {
	
	public ScoreTracker score;
	public ExplodingProp[] propList;
	public float scoreBase;
	public float spawnInterval;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= spawnInterval){
			timeElapsed -= spawnInterval;
			int botIndex = Random.Range(0, propList.Length);
			ExplodingProp bot = Instantiate(
				propList[botIndex]
				, transform.position
				, Quaternion.Euler (0, 180, 0)
				) as ExplodingProp;
			bot.tracker = score;
			bot.Score = scoreBase;
		}
	
	}
	
	private void Spawn(){
		
	}
	
	private float timeElapsed = 0;
}
