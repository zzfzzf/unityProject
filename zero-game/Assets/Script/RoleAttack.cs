using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAttack : MonoBehaviour {
	private float timer = 1.0f;
	private GameObject skill;
	private Vector3 thisPosition;
	private Vector3 targetPosition;
	public float skillSpeed = 3.0f;
	// Use this for initialization
	public Component role;
	public string mouseLeftSkill{ get; set;}
	public string mouseRightSkill{ get; set;}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// 开始施法
		beginSkill ();
	}
	void beginSkill(){
		if (timer > 0) {
			timer -= Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Q)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hitInfo = Physics2D.GetRayIntersection (ray,1000f,1 << LayerMask.NameToLayer("background"));	
			// 击中目标处理逻辑
			if(hitInfo.collider!=null){
				GameObject gameObj = hitInfo.collider.gameObject;
				print (gameObj.name);
			}

			this.thisPosition = transform.position;
			if (timer <= 0) {
				targetPosition=Camera.main.ScreenToWorldPoint (Input.mousePosition);
				targetPosition.z = 0;
				skill= Instantiate (GameObject.Find ("Skill"), transform.position, transform.rotation);
				timer = 1.0f;
				Destroy (skill,3);
			}
		}
		if (skill != null) {

			skill.transform.position = Vector3.MoveTowards (skill.transform.position,targetPosition,skillSpeed*Time.deltaTime);
		}

	}
}
