using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAttack : MonoBehaviour {

	/// 当前使用技能对象 
	private GameObject thisSkillObj;
	/// 当前技能
	private SkillButton thisSkill;
	/// 技能目标
	private GameObject targetObj;
	public float skillSpeed = 3.0f;
	/// 鼠标左键技能
	public SkillButton mouseLeftSkill{get;set;}
	public SkillButton mouseRightSkill{get;set;}
	public SkillButton quickSkill_1{ set; get;}
	public SkillButton quickSkill_2{ set; get;}
	public SkillButton quickSkill_3{ set; get;}
	public SkillButton quickSkill_4{ set; get;}
	public SkillButton quickSkill_5{ set; get;}
	public SkillButton quickSkill_6{ set; get;}
	public SkillButton quickSkill_7{ set; get;}
	/// 自身buff技能列表(包裹负面)
	public List<SkillButton> buffList = new List<SkillButton>();
	/// 装入所有使用过的技能刷新cd
	public List<SkillButton> cdList = new List<SkillButton>();

	void Start () {
		// 初始化左键为普攻
		mouseLeftSkill = transform.gameObject.AddComponent<SkillButton> ();
		mouseLeftSkill.skillCd = 0;
		mouseLeftSkill.skillType="single";
		mouseLeftSkill.skillName="剑刺";
		mouseLeftSkill.duration = 100;
		mouseLeftSkill.skillTarget ="enemy";
		mouseLeftSkill.flyDistance = 0;
		mouseLeftSkill.spellDistance = 3000;

	}
	// 刷新所有技能cd
	void RefreshCd(){
		for(int i=0;i<cdList.Count;i++){
			// 如果cd 没有则移除
			if (cdList [i] != null && cdList [i].timer <= 0) {
				cdList.Remove (cdList[i]);
			} else {
				cdList [i].timer -= Time.deltaTime;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		// 刷新技能cd
		RefreshCd();
		// 开始施法
		beginSkill ();
		// 技能的移动和伤害判断
		skill_move_damage ();
	}
	void beginSkill(){
		// 按下鼠标左键
		if(Input.GetMouseButton(0)){
			thisSkill = mouseLeftSkill;
			CreateSkill (mouseLeftSkill);
		}
	}
	float Angle360(GameObject origin,GameObject target){
		// 0~180
		if ((target.transform.position - origin.transform.position).y > 0) {
			return Vector2.Angle (target.transform.position-origin.transform.position,Vector2.left);
		} else {
			return 360-Vector2.Angle (target.transform.position-origin.transform.position,Vector2.left);
		}
	}
	void skill_move_damage(){
		if (thisSkillObj != null) {
			// 如果没有弹道(顺发)
			if(thisSkill.flyDistance==0){
				// 根据不同技能 去定位位置
				if (thisSkill.skillName.Equals ("剑刺")) {
					print (Angle360(targetObj,transform.gameObject));
					thisSkillObj.transform.SetPositionAndRotation (thisSkillObj.transform.position, Quaternion.Euler(new Vector3(0,0, 0-Angle360(transform.gameObject,targetObj))));
				} else {
					// 技能定位在目标位置
					// 目前技能为跟踪锁定技能
					thisSkillObj.transform.position = targetObj.transform.position; 
				}
			}else{
				// 技能移动到目标位置 (弹道飞行) 跟踪
				thisSkillObj.transform.position = Vector3.MoveTowards (thisSkillObj.transform.position,targetObj.transform.position,skillSpeed*Time.deltaTime);
			}
		}

	}
	// 施法skill技能
	void CreateSkill(SkillButton skill){

		if(skill!=null){
			

			// 如果当前时间-使用技能时间>技能cd 说明cd完毕
			if (!cdList.Contains (skill)) {
				// 使技能进入cd
				skill.timer = skill.skillCd;
				cdList.Add (skill);
				// 技能目标为自己
				if (skill.skillTarget.Equals ("mine")) {
					targetObj = transform.gameObject;


					thisSkillObj = Instantiate (Resources.Load<GameObject> ("Prefab/Skill/skill" ), transform.position, transform.rotation);
					thisSkillObj.transform.GetComponent<Animator> ().Play (skill.skillName);
				} else if (skill.skillTarget.Equals ("enemy")) {
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					// 射线只检测角色这层
					RaycastHit2D hitInfo = Physics2D.GetRayIntersection (ray, 1000f, 1 << LayerMask.NameToLayer ("role"));
					if (hitInfo.collider != null) {
						// 技能目标为射线检测的role层的对象
						targetObj = hitInfo.collider.gameObject;
						// 判断角色和目标间的距离是不是大于施法距离
						if (skill.spellDistance > 0) {
							// 距离单位都是N.N  所以这里除以100 实际以米为单位  比如100米 就是0.1
							if (Vector2.Distance (transform.position, targetObj.transform.position) < skill.spellDistance*0.01) {
								
								thisSkillObj = Instantiate (Resources.Load<GameObject> ("Prefab/Skill/skill"), transform.position, transform.rotation);
								thisSkillObj.transform.GetComponent<Animator> ().Play (skill.skillName);
							} else {
								// 施法距离太远
								return;
							}
						}
					}	
				} else {
					// 如果技能对象是友军
				}
			

				if(thisSkillObj!=null){
					// 设置技能对象碰撞后的持续时间
					thisSkillObj.GetComponent<SkillObj> ().duration = skill.duration;
					// 根据有无持续时间销毁技能对象
					if (skill.duration != 0) {
						Destroy (thisSkillObj, skill.duration);
					} else {
						// 默认十秒清空当前技能
						Destroy (thisSkillObj,10);					
					}
				}
			}
		}
	}
}
