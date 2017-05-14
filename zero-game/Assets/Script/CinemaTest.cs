using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hitInfo;
			if (Physics.Raycast (ray, out hitInfo)) {
				Debug.DrawLine(ray.origin, hitInfo.point);//划出射线，在scene视图中能看到由摄像机发射出的射线
				GameObject gameObj = hitInfo.collider.gameObject;
				print (gameObj.name);
			}

		}
			}
}
