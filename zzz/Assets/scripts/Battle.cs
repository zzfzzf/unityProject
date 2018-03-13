using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Battle : MonoBehaviour {
	public Text battleContent; 
	// 多久执行一次 1为1秒
	public float time=1.0f;
	public bool isBattle = false;
	// 控制文字显示到最低端
	public Scrollbar sb;
	// 被攻击者
	public Biology hurt=new Biology();
	// 被攻击者临时属性
	public TempProperty hurtTp=new TempProperty();
	// 攻击者
	public Biology attacker = new Biology();
	//攻击者临时属性
	public TempProperty attackTp=new TempProperty();

	Skill attackSkill = new Skill ();
	Skill defendSkill = new Skill ();
	// 这里是通过网络获取并赋值的
	public Role enemy=new Role();
	public Role me = new Role();
	public Pet myPet = new Pet();
	public Pet enemyPet = new Pet ();

	// 攻击次数
	public int round = 0;
	// 用来判断先行顺序
	public List<Biology> rpList = new List<Biology> ();
	//友军
	public List<Biology> ourList = new List<Biology>();
	//敌军
	public List<Biology> otherList = new List<Biology>();


	// 将要释放的攻击技能 攻击防御N选1触发
	List<Skill> attackSkills = new List<Skill> ();
	// 将要释放的防御技能（目标）
	List<Skill> defendSkills = new List<Skill> ();
	// 辅助技能战斗开始触发 一直存在
	// 攻击者辅助技能
	List<Skill> attackHelpSkills = new List<Skill> ();
	// 受伤者辅助技能
	List<Skill> hurtHelpSkills = new List<Skill> ();

	void Start () {


		me.hpImage=UIManager.instance.battleSub.Find ("myHp/image").GetComponent<Image>();
		myPet.hpImage=UIManager.instance.battleSub.Find ("myPetHp/image").GetComponent<Image>();
		enemy.hpImage=UIManager.instance.battleSub.Find ("enemyHp/image").GetComponent<Image>();
		enemyPet.hpImage=UIManager.instance.battleSub.Find ("enemyPetHp/image").GetComponent<Image>();


		me.hpNum=UIManager.instance.battleSub.Find ("myHp/num").GetComponent<Text>();
		myPet.hpNum=UIManager.instance.battleSub.Find ("myPetHp/num").GetComponent<Text>();
		enemy.hpNum=UIManager.instance.battleSub.Find ("enemyHp/num").GetComponent<Text>();
		enemyPet.hpNum=UIManager.instance.battleSub.Find ("enemyPetHp/num").GetComponent<Text>();

		me.mpImage=UIManager.instance.battleSub.Find ("myMp/image").GetComponent<Image>();
		myPet.mpImage=UIManager.instance.battleSub.Find ("myPetMp/image").GetComponent<Image>();
		enemy.mpImage=UIManager.instance.battleSub.Find ("enemyMp/image").GetComponent<Image>();
		enemyPet.mpImage=UIManager.instance.battleSub.Find ("enemyPetMp/image").GetComponent<Image>();


		me.mpNum=UIManager.instance.battleSub.Find ("myMp/num").GetComponent<Text>();
		myPet.mpNum=UIManager.instance.battleSub.Find ("myPetMp/num").GetComponent<Text>();
		enemy.mpNum=UIManager.instance.battleSub.Find ("enemyHp/num").GetComponent<Text>();
		enemyPet.mpNum=UIManager.instance.battleSub.Find ("enemyPetMp/num").GetComponent<Text>();


		me.hp = 1000;
		myPet.hp = 1000;
		enemy.hp = 1000;
		enemyPet.hp = 1000;

		me.mp = 1000;
		myPet.mp = 1000;
		enemy.mp = 1000;
		enemyPet.mp = 1000;

		me.dodge = 1;
		myPet.dodge = 1;
		enemy.dodge = 1;
		enemyPet.dodge = 1;

		me.maxMp = 1000;
		myPet.maxMp = 1000;
		enemy.maxMp = 1000;
		enemyPet.maxMp = 1000;

		me.maxHp = 1000;
		myPet.maxHp = 1000;
		enemy.maxHp = 1000;
		enemyPet.maxHp = 1000;

		me.hit = 1000;
		Skill s = new Skill ();
		s.mp = 100;
		s.name = "黑龙波";
		s.damage = 0.3;
		s.odds = 100;
		s.type="attack";

		me.skills.Add(s);
		me.name = "我";
		myPet.name="我的宠物";
		enemy.name = "敌人";
		enemyPet.name = "敌人宠物";
		enemy.speed = 300;
		enemyPet.speed = 400;
		me.speed = 10000;	
		myPet.speed = 100;

		me.attack = 400;
		myPet.attack = 500;
		enemy.attack = 200;
		enemyPet.attack = 300;

		rpList.Add (enemy);
		rpList.Add (me);
		rpList.Add (myPet);
		rpList.Add (enemyPet);
		ourList.Add (me);
		ourList.Add (myPet);
		otherList.Add (enemy);
		otherList.Add (enemyPet);
	}
	
	// Update is called once per frame
	void Update () {
	}
	private void battleContentLayout(){
		// 文字始终处于底部
		sb.value = 0;
		battleContent.resizeTextMaxSize = (int)battleContent.preferredHeight/33;
	}
	public void batting(){
		
		// 选定攻击者
		selectAttacker ();
		// 选择攻击目标
		selectTarget ();
		//战斗显示
		battleShow();
		// 游戏胜利判断 
		victory ();
	}

	private void  victory(){
		sb.value = 0;
		int ourSurvival = 0;
		int otherSurvival = 0;
		for(int i=0;i<rpList.Count;i++){
			if (ourList.Contains (rpList [i])) {
				ourSurvival++;
			} else {
				otherSurvival++;
			}
		}
		if (ourSurvival == 0) {
			
			battleContent.text=battleContent.text+"敌方胜利\n";

			exitBattle ();
			return;
		} else if(otherSurvival==0){
			battleContent.text=battleContent.text+"我方胜利\n";
			exitBattle ();
			return;
		}
	}
	// 战利品
	private void prize(){
	
	}
	// 当前该谁攻击 返回序号
	public int selectAttacker(){
		// 决定当次攻击由谁开始
		round++;
		if(round>=rpList.Count){
			round = 0;
			// 一回合过后重新根据速度排列（可能技能加速度）
			rpList = sort (rpList);

		}
		// 速度最快的为攻击者 在每次回合完之前该值不会变动
		attacker = rpList[round];
		return round;
	}
	private void selectTarget(){
		int target = Random.Range (0,2);
		// target 可以设置为固定 这样攻击目标将固定
		if (ourList.Contains (rpList [round])) {
			if(target==0){
				hurt = otherList [0].hp > 0 ? otherList [0] : otherList [1];
			}else{
				hurt = otherList [1].hp > 0 ? otherList [1] : otherList [0];
			}

		} else {
			if(target==0){
				hurt = ourList [0].hp > 0 ? ourList [0] : ourList [1];
			}else{
				hurt = ourList [1].hp > 0 ? ourList [1] : ourList [0];
			}
		}
	}
	public void battleShow(){
		sb.value = 0;
		// 伤害值
		double damageNum =attacking(attacker,hurt);
		float hpWidth;
		float mpWidth;
		if (damageNum==-1) {
			battleContent.text = battleContent.text + attacker.name + "对" + hurt.name + "未命中！！\n";
			return;
		} 
		// 血条显示
		hpWidth = (float)(140 * (1 - (attacker.maxHp-attacker.hp) / attacker.maxHp));
	    attacker.hpImage.GetComponent<RectTransform> ().sizeDelta = new Vector2 (hpWidth,30f);
		attacker.hpNum.text=attacker.hp+"/"+attacker.maxHp;
		hpWidth = (float)(140 * (1 - (hurt.maxHp-hurt.hp) / hurt.maxHp));
		hurt.hpImage.GetComponent<RectTransform> ().sizeDelta = new Vector2 (hpWidth,30f);
		hurt.hpNum.text=hurt.hp+"/"+hurt.maxHp;
		// 蓝条显示
		mpWidth = (float)(140 * (1 - (attacker.maxMp-attacker.mp) / attacker.maxMp));
		print (attacker.name+"蓝条宽度"+mpWidth);
		attacker.mpImage.GetComponent<RectTransform> ().sizeDelta = new Vector2 (mpWidth,30f);
		attacker.mpNum.text=attacker.mp+"/"+attacker.maxMp;
		mpWidth = (float)(140 * (1 - (hurt.maxMp-hurt.mp) / hurt.maxMp));
		hurt.mpImage.GetComponent<RectTransform> ().sizeDelta = new Vector2 (mpWidth,30f);
		hurt.mpNum.text=hurt.mp+"/"+hurt.maxMp;

	
		string attackStr= "";
		string defendStr= "";
		string attackHelpStr= "";
		string hurtHelpStr= "";

		if(attackSkill!=null){

			attackStr = "使用"+"【"+ attackSkill.name+"】";
		}
		if(defendSkill!=null){
			defendStr = "触发防御技"+"【"+ defendSkill.name+"】";
		}



		// 攻击者触发辅助技能
		if(attackHelpSkills.Count>0){
			attackHelpStr = attacker.name+"触发辅助技能【";
			for(int i=0;i<attackHelpSkills.Count;i++){
				if (i != attackHelpSkills.Count - 1) {
					attackHelpStr += attackHelpSkills [i].name + ",";
				} else {
					attackHelpStr += attackHelpSkills [i].name;
				}
			}
			attackHelpStr += "】\n";
		}

		// 受伤者触发辅助技能
		if(hurtHelpSkills.Count>0){
			hurtHelpStr = hurt.name+"触发辅助技能【";
			for(int i=0;i<hurtHelpSkills.Count;i++){
				if (i != hurtHelpSkills.Count - 1) {
					hurtHelpStr += hurtHelpSkills [i].name + ",";
				} else {
					hurtHelpStr += hurtHelpSkills [i].name;
				}
			}
			hurtHelpStr += "】\n";
		}


	
		// 显示文字信息
		// 触发辅助技能 (被动技能)
		if(!"".Equals(attackHelpStr)){
			attackHelpStr+="\n";
		}
		if(!"".Equals(hurtHelpStr)){
			hurtHelpStr+="\n";
		}
		battleContent.text += attackHelpStr + hurtHelpStr;
		battleContent.text += attacker.name + attackStr + "对" + hurt.name + "造成了" + damageNum + "点伤害，" + effectStr (attackSkill)+"\n";

		if(!"".Equals(defendStr)){
			battleContent.text += defendStr + ","+effectStr(defendSkill);
		}


	}


	// 开启战斗和准备
	public void battle(){
		
		// 战斗面板布局
		battleContentLayout ();
		// 战斗顺序排列
		rpList = sort (rpList);
		replyHp ();
		// 开启战斗
		InvokeRepeating("batting", 0, 1);
	}
	// 主角回复满血 敌人重新赋值不用管
	private void replyHp(){
		me.hp = me.maxHp;
		me.hpImage.GetComponent<RectTransform>().sizeDelta = new Vector2(140f,30f);
		me.hpNum.text = me.hp + "/" + me.maxMp;
	}
	// 退出战斗
	public void exitBattle(){
		CancelInvoke ("batting");
	}
	// 战斗序列排序 根据speed
	private List<Biology> sort(List<Biology> list){
		for(int i=0;i<list.Count-1;i++){
			for(int j=0;j<list.Count-1-j;j++){
				if(list[i].speed>list[j].speed){
					Biology temp = list [j];
					list [j] = list [i];
					list [i] = temp;
				}
			}
		}
		return list;
	}
		
	private double attacking(Biology attacker,Biology hurt){
		attackSkills.Clear();



		// 0~100随机数 判断释放几率
		int skillRandom = Random.Range (0, 100);
		// 为攻击者添加技能
		for (int i = 0; i < attacker.skills.Count; i++) {
			// 该技能如果40% >随机数30% 则释放成功 以左边为准就是随机数小于 右边为准就是随机数大于 0~100
			if (attacker.skills [i].odds > skillRandom) {
				if (attacker.skills [i].type.Equals ("attack")) {
					print ("给"+attacker.name+"添加"+attacker.skills[i].name);
					attackSkills.Add (attacker.skills [i]);
				} else if (attacker.skills [i].type.Equals ("help")) {
					attackHelpSkills.Add (attacker.skills [i]);
				}	
			}
		}
		skillRandom = Random.Range (0, 100);
		// 循环添加 需要释放的防御技能 和辅助技能
		for (int i = 0; i < hurt.skills.Count; i++) {
			if (hurt.skills [i].odds > skillRandom) {
				if (hurt.skills [i].type.Equals ("defend")) {
					defendSkills.Add (hurt.skills [i]);
				}else if(hurt.skills[i].type.Equals("help")){
					hurtHelpSkills.Add (hurt.skills[i]);
				}
			}
		}
		// 不清空 后面如果其他角色没技能会沿用第一个角色的技能 技能判断是根据name来的 
		attackSkill=null;
		defendSkill= null;
		// 随机选出一个攻击技能 和一个防御技能
		if (attackSkills.Count > 0) {
			attackSkill = attackSkills [Random.Range (0, attackSkills.Count)];
	
		}
		if (defendSkills.Count > 0) {
			defendSkill = attackSkills [Random.Range (0, defendSkills.Count)];
		}
			
	
			// 为攻击者判定技能特效  
			effect(attackTp,attackSkill);
			// 为受伤者判定技能特效
			effect(hurtTp,defendSkill);
	



		//　攻击辅助技能特效
		for(int i=0;i<attackHelpSkills.Count;i++){
			effect(attackTp,attackHelpSkills[i]);
		}

		//　防御辅助技能特效
		for(int i=0;i<attackHelpSkills.Count;i++){
			effect(hurtTp,hurtHelpSkills[i]);
		}

		double realDamage;
		// 真实攻击值等于 (基础攻击+技能装备附属攻击)*(1+攻击增幅n%)
		double realAttack =(attacker.attack + attackTp.attack) * (1 + attacker.attackUp + attackTp.attackUp);
		// 有技能承受技能加成的伤害
		if(attackSkill!=null){
			realAttack= (attacker.attack + attackTp.attack) * (1 + attacker.attackUp + attackTp.attackUp)*(1+attackSkill.damage);
		}

		double realHit = (attacker.hit + attackTp.hit);
		double realDodge =(hurt.dodge+hurtTp.dodge);
		// 真实反射值等于 (受伤者反射值+受伤者装备技能附属反射值)
		double realReflect = hurt.reflect + hurtTp.reflect;
		// 真实减伤等于 (受伤者防御+受伤者技能装备附属防御)/(受伤者防御+受伤者技能装备附属防御+100)+受伤者减伤+受伤者装备技能附属减伤
		double realderate = (hurt.defend + hurtTp.defend) / (hurt.defend + hurtTp.defend + 100) + (hurt.derate + hurtTp.derate);
		double realCirt = attacker.crit + attackTp.crit;
		double realCritDamage = attacker.critDamage + attackTp.critDamage;

		// 命中判断 
		if (realHit/(realHit+realDodge)*100<Random.Range(0,100)) {

			return -1; // 未命中
		}

		// 暴击判断
		if (Random.Range (0, 100) < realCirt) {
			//暴击
			realDamage = realAttack * (1.5 + realCritDamage) * (1 - realderate);
			
		} else {
			realDamage = realAttack * (1 + realCritDamage) * (1 - realderate);
		}

	

		// 攻击者血量计算（吸血 伤害反射）
		attacker.hp = attacker.hp + attackTp.hp + realDamage * (attacker.drawHp + attackTp.drawHp) - realDamage * (realReflect);
		// 吸血不能超过生命最高值
		if(attacker.hp>attacker.maxHp){
			attacker.hp = attacker.maxHp;
		}

		// 如果攻击者被反弹或者特效击杀则移除战斗
		if (attacker.hp <= 0) {
			rpList.Remove (attacker);
		}

		// 受伤者血量计算 (受伤回血) （受伤者当前血量+技能装备附属回血量）+最大生命值* （受伤者受伤回复百分比+ 装备技能特效附加受伤回复); 
		double realHurtHp=hurt.hp+hurtTp.hp + hurt.maxHp * (hurt.hurtReplay+hurtTp.hurtReplay) ;
		// 吸血不能超过生命最高值
		if(realHurtHp>hurt.maxHp){
			hurt.hp = hurt.maxHp;
		}

		// 判定受伤者血量
		if (realHurtHp<= realDamage) {
			hurt.hp = 0;
			// 将该目标移除战斗
			rpList.Remove (hurt);
		} else {
			hurt.hp = realHurtHp-realDamage;
		}

		// 攻击者查克拉计算 5%每次回复2%mp 
		attacker.mp = (attackTp.drawMp + attacker.drawMp) * realDamage + 0.02*(attacker.maxMp) +attacker.mp;
		// 查克拉上限不超过最大值
		if(attacker.mp>attacker.maxMp){
			attacker.mp = attacker.maxMp;
		}
		if(attackSkill!=null){
			if ( attacker.mp < attackSkill.mp) {
				attacker.mp = 0;
			} else {
				attacker.mp =attacker.mp - attackSkill.mp;
			}
		}
	

		// 受伤者查克拉计算
		hurt.mp = (hurtTp.drawMp + hurt.drawMp) * realDamage + 0.02*(hurt.maxMp) +hurt.mp;
		// 查克拉上限不超过最大值
		if(hurt.mp>hurt.maxMp){
			hurt.mp = hurt.maxMp;
		}
		if(defendSkill!=null){
			if (hurt.mp < defendSkill.mp) {
				hurt.mp = 0;
			} else {
				hurt.mp =hurt.mp - defendSkill.mp;
			}
		}

	

		return realDamage;
	}
	// 技能效果描述
	private string effectStr(Skill skill){
		if (skill == null)return "";
		switch(skill.effect){
		case 1: // 1为吸血
			return "增加"+skill.effectNum+"生命偷取";
		case 2: // 反射
			return "反射"+skill.effectNum+"点伤害";
		case 3: // 增加attack伤害值
			return "增加"+skill.effectNum+"点攻击力";
		case 4: // 增加defend防御值
			return "增加"+skill.effectNum+"点防御值";
		case 5: // 增加drawMp伤害百分比
			return "增加"+skill.effectNum+"能量偷取";
		case 6: // 被击中最大生命回血
			return "击中回复"+skill.effectNum+"最大生命值";
		case 7: // 增加命中
			return "增加"+skill.effectNum+"点命中";
		case 8: // 增加闪避
			return "增加"+skill.effectNum+"点闪避";
		case 9: // 增加暴击率
			return "增加"+skill.effectNum+"点暴击率";
		case 10: // 增加减伤
			return "增加"+skill.effectNum+"点减伤";
		case 11: // 增加暴击伤害
			return "增加"+skill.effectNum+"点暴击伤害";
		default :
			return "";
		}
	}
	// 技能特殊效果 默认单数为攻击技能 双数为防御技
	private void effect(TempProperty temp,Skill skill){
		if(temp==null)return;if (skill == null)return;
		switch(skill.effect){
		case 1: // 1为吸血
			temp.drawHp += skill.effectNum;
			break;
		case 2: // 反射
			temp.reflect += skill.effectNum;
			break;
		case 3: // 增加attack伤害值
			temp.attack += skill.effectNum;
			break;
		case 4: // 增加defend防御值
			temp.defend += skill.effectNum;
			break;
		case 5: // 增加drawHp伤害百分比
			temp.attackUp += skill.effectNum;
			break;
		case 6: // 被击中最大生命回血
			temp.hurtReplay += skill.effectNum;
			break;
		case 7: // 增加命中
			temp.hit += skill.effectNum;
			break;
		case 8: // 增加闪避
			temp.dodge += skill.effectNum;
			break;
		case 9: // 增加暴击率
			temp.crit += skill.effectNum;
			break;
		case 10: // 增加减伤
			temp.derate += skill.effectNum;
			break;
		case 11: // 增加暴击伤害
			temp.critDamage += skill.effectNum;
			break;
		}



	}
}
