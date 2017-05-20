using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMove : MonoBehaviour {
	// 角色行走动画的方向
	private int up = 0;
	private int down = 1;
	private int left =2;
	private int right =3;
	private int leftUp =4;
	private int rightUP = 5;
	private int leftDown = 6;
	private int rightDown = 7;
	private int stand = 8;

	public float speed = 3f;
	private bool moving = false;
	// Use this for initialization
	public Rigidbody2D rd = null;
	Animator an = null;
	void Start () {
		rd=GetComponent<Rigidbody2D>();
		an = GetComponent<Animator> ();
		for (int i = 0; i < 10; i++) {
			
		}
	
	}
	int direction(Vector2 tp){
		Vector2 cp = transform.position;
		Vector2 x2 = new Vector2 (1,0);//与x轴的夹角

		if (tp.x - cp.x > 0) {
			if (tp.y - cp.y > 0) {
				if (Vector2.Angle(x2,tp-cp) <= 60 && Vector2.Angle(x2,tp-cp) >= 30 ) {
					return rightUP;
				} else if (Vector2.Angle(x2,tp-cp) < 30) {
					return right;
				} else {
					return up;
				}
			} else {
				if (Vector2.Angle(x2,tp-cp) <= 60 && Vector2.Angle(x2,tp-cp) >= 30) {
					return rightDown;
				} else if (Vector2.Angle(x2,tp-cp) < 30) {
					return right;
				} else {
					return down;
				}
			} 
		} else {
			if (tp.y - cp.y > 0) {
				if (Vector2.Angle(x2,tp-cp) <= 150 && Vector2.Angle(x2,tp-cp) >= 120) {
					return leftUp;
				} else if (Vector2.Angle(x2,tp-cp) < 150) {
					return up;
				} else {
					return left;
				}
			} else {
				if (Vector2.Angle(x2,tp-cp) <= 150 && Vector2.Angle(x2,tp-cp) >= 120) {
					return leftDown;
				} else if (Vector2.Angle(x2,tp-cp) < 120) {
					return down;
				} else {
					return left;
				}
			} 
		}


	}

	// 移动以鼠标为主
	void walk(){
		
		float moveX = 0;
		float moveY = 0;
	
		if(Input.GetMouseButton(1)){
			moving = true;
			Vector2 targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			an.SetInteger ("direction",direction(targetPosition));
	        transform.position = Vector2.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);
		}
		if(Input.GetMouseButtonUp(1)){
			moving = false;
			an.SetInteger ("direction",8);
		}
		if (!moving) {
			if(Input.GetKey(KeyCode.UpArrow)){
				an.SetInteger ("direction",0);
				moveY = moveY + speed * Time.deltaTime;
			}
			if(Input.GetKey(KeyCode.DownArrow)){
				an.SetInteger ("direction",1);
				moveY = moveY - speed * Time.deltaTime;
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				an.SetInteger ("direction",2);
				moveX = moveX - speed * Time.deltaTime;
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				an.SetInteger ("direction",3);
				moveX = moveX + speed * Time.deltaTime;
			}
			Vector2 endPosition = new Vector2 (moveX, moveY);
			rd.MovePosition(rd.position+endPosition);

		}

	}
	void OnDrawGizmos() {

	}

	void OnControllerColliderHit2D(ControllerColliderHit hit) {    
		print ("发生碰撞");
	}

	// Update is called once per frame
	void Update () {
		walk ();
	}

	void FixedUpdate(){
		
	}
}
