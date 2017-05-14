using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEvent : MonoBehaviour {

	void OnMouseDown() {
		if(Input.GetMouseButtonDown(0)){
			print ("按了鼠标左键");
		}
	}
}
