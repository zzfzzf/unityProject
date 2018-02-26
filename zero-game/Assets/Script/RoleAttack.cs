using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAttack : MonoBehaviour {
	/// 当前使用技能对象 
	private GameObject thisSkillObj;
	/// 当前技能
	private SkillButton thisSkill;
	/// 技能目标
	private Vector3 targetPosition;
	/// 技能释放速度
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

	/// 技能列表
	public List<GameObject> skillList = new List<GameObject>();
	/// 装入所有使用过的技能刷新cd
	public List<SkillButton> cdList = new List<SkillButton>();

	void Start () {
		// 初始化左键为普攻
		mouseLeftSkill = transform.gameObject.AddComponent<SkillButton> ();
		mouseLeftSkill.skillCd = 5;
		mouseLeftSkill.skillType="battle";
		mouseLeftSkill.skillName="水晶盾";
		mouseLeftSkill.duration = 2;
		mouseLeftSkill.flyDistance = 10;
		mouseLeftSkill.spellDistance = 3000;
		mouseLeftSkill.hasBuff = false;

	}
	// 刷新所有技能cd
	void refreshCd(){
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
		refreshCd();
		// 开始施法
		beginSkill ();
		// 技能显示
		skillShow();

			
	}
	void beginSkill(){
		// 按下鼠标左键
		if(Input.GetMouseButton(0)){
			CreateSkill(mouseLeftSkill);
		}
	}
	/// 计算角度 360°
	float Angle360(Vector2 origin,Vector2 target){
		// 0~180
		if ((target - origin).y > 0) {
			return Vector2.Angle (target-origin,Vector2.left);
		} else {
			return 360-Vector2.Angle (target-origin,Vector2.left);
		}
	}

	/// <summary>
	/// 技能定位微调显示
	/// </summary>
	void skillShow(){
		for(int i=0;i<skillList.Count;i++){
			GameObject go = skillList [i];
			if (go != null) {
				skillPosition (go);
			} else {
				skillList.Remove (go);
			}
		}

	}
	/// 技能定位
	void skillPosition(GameObject go){
		SkillObj so = go.GetComponent<SkillObj> ();

		if ("剑刺".Equals (so.skillName)) {
			go.transform.SetPositionAndRotation (go.transform.position, Quaternion.Euler (new Vector3 (0, 0, 0 - Angle360 (so.currentPosition, so.targetPosition))));
		} else if("水晶盾".Equals(so.skillName)) {
			go.transform.position = transform.position;
		}

	}
	/// 技能伤害的逻辑
	void skillDamage(SkillObj so){
		if(so.skillName.Equals("流星火雨")){
			// 获取角色技能,根据角色属性,射线 判断造成伤害；
		}
	}

	
	// 施法skill技能
	void CreateSkill(SkillButton skill){
		// 当前技能设置   
		targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// 鼠标的z轴在-10 
		targetPosition.z = 0;
		thisSkill = skill;
		if(skill!=null){
			// 如果当前时间-使用技能时间>技能cd 说明cd完毕
			if (!cdList.Contains (skill)) {
				// 使技能进入cd
				skill.timer = skill.skillCd;
				cdList.Add (skill);
				// 如果施法距离是>0的技能 则判断距离是否过远
				if (skill.spellDistance > 0) {

					if (Vector2.Distance (transform.position, targetPosition) < skill.spellDistance * 0.01) {
						thisSkillObj = Instantiate (Resources.Load<GameObject> ("Prefab/Skill/skill" ), transform.position, transform.rotation);
						thisSkillObj.transform.GetComponent<Animator> ().Play (skill.skillName);
						thisSkillObj.GetComponent<SkillObj> ().skillName = skill.skillName;
						// 设置对象开始的位置坐标
						thisSkillObj.GetComponent<SkillObj> ().currentPosition = thisSkillObj.transform.position;
						// 设置坐标
						thisSkillObj.GetComponent<SkillObj> ().targetPosition = targetPosition;
						skillList.Add (thisSkillObj);
						// 调用buff控制器添加buff
						transform.GetComponent<RoleBuff> ().add2me (skill);
					}else {
						// 施法距离太远
						return;
					}
				}
				// 如果该技能效果有buff
				if(skill.hasBuff){
					// 添加buff
					transform.GetComponent<RoleBuff> ().add2me (skill);
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
