using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Biology {
	public string name="";
	public double hp;
	public double mp;
	public double maxHp;
	public double maxMp;
	// 暴击率 百分比
	public double crit;
	public double speed;
	public double defend;
	// 闪避
	public double dodge;
	public double hit = 1;
	public double attack;
	// 减伤 百分比
	public double derate;
	// 暴击伤害 百分比
	public double critDamage;
	//吸血 百分比
	public double drawHp;
	//吸藍 百分比
	public double drawMp;
	//伤害减免 百分比
	public double reflect;
	// 受伤回血 自己最大生命的百分比
	public double hurtReplay;
	public int luck;
	public double attackUp;
	public Image hpImage;
	public Image mpImage;
	public Text hpNum;
	public Text mpNum;
	public List<Skill> skills = new List<Skill>();


}
