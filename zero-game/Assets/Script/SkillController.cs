using UnityEngine;  
using System.Collections;  
using UnityEngine.UI;  
using System.Collections.Generic; 
public class SkillController : MonoBehaviour {
	// 从数据库查询该角色所有技能
	public List<Object> skillList = new List<Object>();
	private string skillPrefabName = "Prefab/Object/skill";

	private GameObject skills;
	// Use this for initialization
	void Start () {
		


		skills = UIManager.instance.skill.transform.Find ("battleSkill/skills").gameObject;
		for (int i = 0; i < skillList.Count; i++)  
		{  
			// 技能对象名
		} 
	}
	void Awake ()   
	{  
		

		for (int i = 0; i < skillList.Count; i++)  
		{  
			GameObject skill = Instantiate(Resources.Load(skillPrefabName)) as GameObject;  
			// 遍历添加该角色所有技能，并设置属性
			skill.name = "skill" + (i+1);
			skill.transform.SetParent(skills.transform);  
			SkillButton sb = skill.GetComponent ("SkillButton") as SkillButton; 
			sb.skillName = skillList[0].ToString();
			sb.skillCd =1;
			sb.skillMc =1;
			sb.skillDamage =1;
			sb.skillExp =1;
			sb.skillGet =skillList[0].ToString();
			sb.skillLevel =1;
			sb.skillSpecial =skillList[0].ToString();
			sb.skillWorth =skillList[0].ToString();

		}  
	}  
	// Update is called once per frame
	void Update () {

	}
	void AddSkill (string skillName,int skillLevel =0 ){
		GameObject skill = Instantiate(Resources.Load(skillPrefabName)) as GameObject;  
		skill.transform.SetParent (skills.transform,false);

	}
}
