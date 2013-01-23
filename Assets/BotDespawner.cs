using UnityEngine;
using System.Collections;

public class BotDespawner : MonoBehaviour {


	void OnTriggerEnter(Collider other){
		ExplodingProp p = other.GetComponent<ExplodingProp>();
		if (p != null){
			GameObject.Destroy(p.gameObject);
		}
	}
}
