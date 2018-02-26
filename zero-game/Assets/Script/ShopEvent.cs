using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
public class ShopEvent : MonoBehaviour {
	private Vector2 npcPosition;
	private bool isTalking = false;
	public void start (){
		
	}

	void OnMouseDown() {
		if(Input.GetMouseButtonDown(0)){
			Camera scenceCamera;
			GameObject gameObject=GameObject.Find("scenceCamera");
			scenceCamera= gameObject.GetComponent<Camera>();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit2D hit = Physics2D.GetRayIntersection (ray,1000f,1 << LayerMask.NameToLayer("background"));
			  
			if(hit.collider!=null){
				isTalking = true;
				npcPosition =scenceCamera.WorldToScreenPoint( hit.transform.position);
				print (scenceCamera.WorldToScreenPoint( hit.transform.position));
				print (Camera.main.WorldToScreenPoint( hit.transform.position));

			}
		
			UIManager.instance.npcSay.transform.FindChild ("say").GetComponent<Text>().text = "xxxxxxxxxxxxxxxxxxxxxxxx";
			UIManager.instance.npcSay.transform.position = npcPosition;
			print (npcPosition);
		}
	}

}
