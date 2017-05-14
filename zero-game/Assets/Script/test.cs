using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
	public GameObject gj;
	// Use this for initialization
	void Awake(){
		Object[] initsObjects = GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (Object go in initsObjects) {
			Object.DontDestroyOnLoad(go);
		}
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseEnter() {

	}
	void OnTriggerEnter2D(Collider2D cr){
		
		if(!"player".Equals(cr.transform.name)){
			print ("碰撞了怪物");
			Destroy (this.gameObject);
		}

	}
}
