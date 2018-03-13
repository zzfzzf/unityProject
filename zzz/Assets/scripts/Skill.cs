using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill {
	public string name="";
	// 百分比
	public double damage=0;
	// 基本数值
	public int defend=0;
	// 技能效果编号 吸血，减护甲等等 默认奇数是攻击 双数是防御
	public int effect;
	public int level=0;
	// 技能品质 1234 黄玄地天
	public int queality=0;
	// 技能释放概率
	public double odds =0;
	// 技能类型 attack defend help 攻击防御辅助
	public string type="";
	// 技能效果数值 根据effect 来确认是百分比还是数值
	public double effectNum;
	// 该技能所需mp
	public double mp;

}
