using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj : MonoBehaviour {
	///持续时间
	public float duration{set;get;}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// 检测技能碰撞
		Ray ray = Camera.main.ScreenPointToRay (Camera.main.WorldToScreenPoint(transform.position));
		RaycastHit2D hitInfo = Physics2D.GetRayIntersection (ray,1000f,1 << LayerMask.NameToLayer("background"));
		if(hitInfo.collider!=null){
			if(duration==0){
				Destroy (transform.gameObject,1);
			}
		}
	}

}
