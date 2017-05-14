using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour {
	/// 力量
	public int str{get;set;}
	/// 敏捷
	public int agi{get;set;}
	/// 智力
	public int inte{get;set;}
	/// 幸运
	public int luk{get;set;}
	/// 生命值
	public int hp{get;set;}
	/// 法力值
	public int mp{get;set;}
	/// 称号荣誉
	public int honor{get;set;}
	/// 行会
	public string group{get;set;} 
	/// 名称
	public string roleName{get;set;}
	/// 登记
	public int level{get;set;}
	/// 种族
	public string race{get;set;} 
	/// 物理攻击
	public int pAttack{get;set;}
	/// 魔法攻击
	public int mAttack{get;set;}
	/// 物理防御
	public int pDef{get;set;}
	/// 魔法防御
	public int mDef{get;set;}
	/// 暴击几率
	public int crit{get;set;}
	/// 命中率
	public int hit{get;set;}
	/// 闪避
	public int evade{get;set;}
	/// 暴击伤害
	public int critDamage{get;set;}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
