using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Renderer))]
public class ScrollingConveyorBelt : MonoBehaviour {
	
	public float scrollSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time * scrollSpeed;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		renderer.material.SetTextureOffset("_BumpMap", new Vector2(offset, 0));
	}
}
