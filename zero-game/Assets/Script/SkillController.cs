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
		for (int i = 0; i < 3; i++)  
		{  
			
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
			sb.skillCd =skillList[0].ToString();
			sb.skillMc =skillList[0].ToString();
			sb.skillDamage =skillList[0].ToString();
			sb.skillExp =skillList[0].ToString();
			sb.skillGet =skillList[0].ToString();
			sb.skillLevel =skillList[0].ToString();
			sb.skillSpecial =skillList[0].ToString();
			sb.skillWorth =skillList[0].ToString();

		}  
	}  
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.K)){
			
			AddSkill ("黑龙波");
		}	
	}
	void AddSkill (string skillName,int skillLevel =0 ){
		print ("添加技能");
		GameObject skill = Instantiate(Resources.Load(skillPrefabName)) as GameObject;  
		skill.transform.SetParent (skills.transform,false);

	}
}
