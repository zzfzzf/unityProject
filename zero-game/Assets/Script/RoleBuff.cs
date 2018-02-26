using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleBuff : MonoBehaviour {

	public List<Buff> buffList = new List<Buff>();
	/// buff加成的角色属性 计算伤害的时候要加上这个
	private Role theBuff = new Role();
	// Update is called once per frame
	void Update () {
		refreshBuff ();
	}
	/// 给自己添加buff
	public void add2me(SkillButton sb){
		Buff buff = new Buff ();
		if(sb.skillName.Equals("水晶盾")){
			// 增加100力量的buff
			buff.type = "str";
			buff.num = 100;
		}
		// 增加buff属性
		addBuff (buff);
		buffList.Add (buff);
	}

	private void addBuff(Buff buff){
		if(buff.type.Equals("str")){
			theBuff.str += buff.num;
		}else if(buff.type.Equals("agi")){
			theBuff.agi += buff.num;
		}else if(buff.type.Equals("inte")){
			theBuff.inte += buff.num;
		}else if(buff.type.Equals("luk")){
			theBuff.luk += buff.num;
		}else if(buff.type.Equals("hp")){
			theBuff.hp += buff.num;
		}else if(buff.type.Equals("mp")){
			theBuff.mp += buff.num;
		}else if(buff.type.Equals("level")){
			theBuff.level += buff.num;
		}else if(buff.type.Equals("pAttack")){
			theBuff.pAttack += buff.num;
		}else if(buff.type.Equals("mAttack")){
			theBuff.mAttack += buff.num;
		}else if(buff.type.Equals("pDef")){
			theBuff.pDef += buff.num;
		}else if(buff.type.Equals("mDef")){
			theBuff.mDef += buff.num;
		}else if(buff.type.Equals("crit")){
			theBuff.crit += buff.num;
		}else if(buff.type.Equals("hit")){
			theBuff.hit += buff.num;
		}else if(buff.type.Equals("evade")){
			theBuff.evade += buff.num;
		}else if(buff.type.Equals("critDamage")){
			theBuff.critDamage += buff.num;
		}
	}

	private void refreshBuff(){
		for(int i=0;i<buffList.Count;i++){
			if (buffList [i] != null) {
				// 每次循环减少buff时间 时间到了则删除buff
				if (buffList [i].endTime > 0) {
					buffList [i].endTime--;
				} else {
					delBuff (buffList[i]);
					buffList.Remove (buffList[i]);
				}
			}
		}
	}

	private void delBuff(Buff buff){
		if(buff.type.Equals("str")){
			theBuff.str -= buff.num;
		}else if(buff.type.Equals("agi")){
			theBuff.agi -= buff.num;
		}else if(buff.type.Equals("inte")){
			theBuff.inte -= buff.num;
		}else if(buff.type.Equals("luk")){
			theBuff.luk -= buff.num;
		}else if(buff.type.Equals("hp")){
			theBuff.hp -= buff.num;
		}else if(buff.type.Equals("mp")){
			theBuff.mp -= buff.num;
		}else if(buff.type.Equals("level")){
			theBuff.level -= buff.num;
		}else if(buff.type.Equals("pAttack")){
			theBuff.pAttack -= buff.num;
		}else if(buff.type.Equals("mAttack")){
			theBuff.mAttack -= buff.num;
		}else if(buff.type.Equals("pDef")){
			theBuff.pDef -= buff.num;
		}else if(buff.type.Equals("mDef")){
			theBuff.mDef -= buff.num;
		}else if(buff.type.Equals("crit")){
			theBuff.crit -= buff.num;
		}else if(buff.type.Equals("hit")){
			theBuff.hit -= buff.num;
		}else if(buff.type.Equals("evade")){
			theBuff.evade -= buff.num;
		}else if(buff.type.Equals("critDamage")){
			theBuff.critDamage -= buff.num;
		}

	}
		
	/// 获取rolebuff属性
	public Role getRoleBuff(){
		return theBuff;
	}
}
