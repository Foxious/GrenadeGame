using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {
	
	public float seconds;
	public float numGrenades;
	public GUISkin skin;
	
	void Update(){
		if (pointsPerSecond < 0.001f){
			return;
		}
		if (pointsToAdd < pointsPerSecond){
			score = Mathf.Round(pointsToAdd + score);
			pointsToAdd = 0;
			scoreAdding = 0;
		}
		else{
			float pointsThisFrame = pointsPerSecond * Time.deltaTime;
			score += pointsThisFrame;
			pointsToAdd -= pointsThisFrame;
		}
	}
	
	public void AddScore(float points){
		pointsToAdd += points;
		pointsPerSecond = pointsToAdd / seconds;
		scoreAdding += (int)Mathf.Round(points);
	}
	
	public void SetGrenades(int val){
		grenades = val;
	}
	
	public void SetGameOver(){
		gameOver = true;
	}
	
	public bool IsGameOver(){
		return gameOver;
	}
	
	void OnGUI(){
		GUI.skin = skin;
		if (scoreAdding > 0){
			GUI.Label(new Rect(20, 425, 100, 30), "+" + (int)scoreAdding);
		}
		GUI.Label(new Rect(20, 450, 400, 30), "Score: " + (int)score);
		GUI.Label(new Rect(600, 475, 400, 30), "Grenades: " + grenades);
		
		if (gameOver){
			GUI.Label (new Rect(150, 200, 400, 30), "GAME OVER!");
			GUI.Label (new Rect(150, 230, 400, 30), "Final Score: " + (int)score);
			GUI.Label (new Rect(150, 250, 400, 30), "Press escape to exit!");
		}
	}
	
	private float score = 0;
	private float pointsToAdd = 0;
	private int scoreAdding = 0;
	private float pointsPerSecond = 0;
	private float grenades = 0;
	private bool gameOver = false;
}
