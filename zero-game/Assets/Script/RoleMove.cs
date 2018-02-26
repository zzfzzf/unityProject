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
	// 移动目标点
	Vector2 targetPosition = new Vector2 (0,0);
	public float speed = 4.0f;  
	private bool moving = false; 
	// Use this for initialization
	public Rigidbody2D rd = null;


	Animator an = null;
	void Start () {
		rd=GetComponent<Rigidbody2D>();
		targetPosition = rd.position;
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
	void OnTriggerEnter2D(Collider2D coll)
	{
		

	}
	void OnTriggerExit2D(Collider2D coll)
	{
		

	}
	Vector2 linshi = new Vector2();
	// 移动以鼠标为主
	void walk(){
		print (Vector2.Distance(rd.position,targetPosition)<0.3);
		Debug.DrawLine(rd.position,targetPosition, Color.blue);
		// 重点附近停止移动(碰撞点和目标点有细微区别)
		if(Vector2.Distance(rd.position,targetPosition) < 0.3f ){
			moving = false;
			an.SetInteger ("direction", stand);
		}
		// float moveX = 0;
		// float moveY = 0;

		if(Input.GetMouseButton(1)){
			moving = true;
			an.SetInteger ("direction", direction (targetPosition));
			targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (rd.position,(targetPosition-rd.position).normalized,Vector2.Distance(rd.position,targetPosition),1 << LayerMask.NameToLayer("background"));
	
			if (hit.collider!=null) {
				// 如果行走方向有边界,那么与边界碰撞处为终点
				print (targetPosition+"已经改成"+hit.point);
				targetPosition = hit.point;
			}
		}

	
		if (moving && rd.position != targetPosition) {
			

			rd.MovePosition(Vector2.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime));

		} else {
			moving = false;
		}

		if(Input.GetMouseButtonUp(1)){
	 
		}
		/*if (!moving) {
			if(Input.GetKey(KeyCode.UpArrow)){
				an.SetInteger ("direction",up);
				moveY = moveY + speed * Time.deltaTime;

			}
			if(Input.GetKey(KeyCode.DownArrow)){
				an.SetInteger ("direction",down);
				moveY = moveY - speed * Time.deltaTime;
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				an.SetInteger ("direction",left);
				moveX = moveX - speed * Time.deltaTime;
				print ("当前位置--"+rd.position);
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				an.SetInteger ("direction",right);
				moveX = moveX + speed * Time.deltaTime;
			}
			Vector2 endPosition = new Vector2 (moveX, moveY);
			rd.MovePosition(rd.position+endPosition);
		}*/

	}
	void OnDrawGizmos() {

	}



	// Update is called once per frame
	void Update () {
		walk ();
	}

	void FixedUpdate(){
		
	}
}
