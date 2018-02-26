using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using UnityEngine.EventSystems;  
public class SkillButton : MonoBehaviour,ICanvasRaycastFilter,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler, IDragHandler, IEndDragHandler{
	private bool isRaycastLocationValid = true;//默认射线不能穿透物品  
	///  技能名称
	public string skillName{get;set;}
	///  该技能是否有buff
	public bool hasBuff = false;
	///  技能等级
	public float skillLevel{get;set;}
	///  技能cd
	public float skillCd{get;set;}
	///  技能魔法消耗
	public float skillMc{get;set;}
	///  技能伤害
	public float skillDamage{get;set;}
	///  技能品阶 
	public string skillWorth{get;set;}
	///  技能特殊效果
	public string skillSpecial{get;set;}
	///  技能获取方式
	public string skillGet{get;set;}
	///  技能升级所需熟练度
	public float totalExp{get;set;}
	///  技能当前熟练度
	public string currentExp{get;set;}
	///  技能熟练度文字
	public int skillExp{get;set;}
	///  技能类型 战斗 生活 
	public string skillType{get;set;}
	///  是否是持续技能(不为1则持续)
	public float duration{ get; set;}
	///  飞行距离 0就是顺发
	public float flyDistance{ get; set;}
	///  施法距离 
	public float spellDistance{ get; set;}

	/// 技能拖拽对象
	private GameObject dragGo;
	// Use this for initialization
	private GameObject parentObj;
	///  快捷栏当前技能
	private GameObject currentSkill;


	public float timer{ get; set;}

	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	private bool AllowMove(){
		return !currentSkill.GetComponent<Image> ().sprite.name.Equals (DefaultSet.instance.QuickSkillIcon.name) && !currentSkill.GetComponent<Image> ().sprite.name.Equals (DefaultSet.instance.MouseSkillLeftIcon.name) && !currentSkill.GetComponent<Image> ().sprite.name.Equals (DefaultSet.instance.MouseSkillRightIcon.name);
		
	}
	public void OnPointerEnter(PointerEventData eventData){//鼠标进入UI调用该方法
		if (UIManager.instance.ShowSkillTips)
		{

			//显示提示信息
			if (!UIManager.instance.skillTip.activeSelf)
			{
				UIManager.instance.skillTip.SetActive(true);
				Transform skillTip = UIManager.instance.skillTip.transform;
				// 以下数据应该由数据库获取到skillController处理之后的属性变量，并且注意替换占位符
				// 技能名字 
				skillTip.FindChild("skillName").GetChild(0).GetComponent<Text>().text="冰魄";
				// 技能等级
				skillTip.FindChild("skillLevel").GetChild(0).GetComponent<Text>().text=71+"级";
				// 技能冷却 单位s
				skillTip.FindChild("skillCd").GetChild(0).GetComponent<Text>().text=10+"s";
				// 技能消耗
				skillTip.FindChild("skillMc").GetChild(0).GetComponent<Text>().text=20+"";
				// 技能熟练度
				skillTip.FindChild("skillExp").GetChild(2).GetComponent<Text>().text=20+"/"+200;
				// 技能品阶
				skillTip.FindChild("skillWorth").GetChild(0).GetComponent<Text>().text="玄";
				// 技能伤害 系数(0.0N)*技能等级*(魔攻+技能每级基础伤害)
				skillTip.FindChild("skillDamage").GetChild(0).GetComponent<Text>().text="对单个敌人造成<color=green>【560%*（魔攻+50）】</color>法术伤害";
				// 技能效果
				skillTip.FindChild("skillSpecial").GetChild(0).GetComponent<Text>().text="降低敌方防御<color=green>142</color>点防御x";
				// 获取方式
				skillTip.FindChild("skillGet").GetChild(0).GetComponent<Text>().text="打怪，活动，随机事件";

			}
		}
	}
	public void OnPointerExit(PointerEventData evenData){//鼠标离开UI调用该方法
		UIManager.instance.skillTip.SetActive(false); 
	}

	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)  
	{  
		
		return isRaycastLocationValid;  
	}  

	public void OnBeginDrag(PointerEventData eventData)  
	{  	
		parentObj = eventData.pointerPressRaycast.gameObject.transform.parent.gameObject;
		dragGo = UIManager.instance.dragSkillObj;
		currentSkill = eventData.pointerPressRaycast.gameObject;
		print (currentSkill.name);
		if(AllowMove()){
			dragGo.SetActive (true);
			dragGo.transform.SetParent (UIManager.instance.canvasTra.transform);
			dragGo.GetComponent<Image> ().sprite = eventData.pointerPressRaycast.gameObject.GetComponent<Image> ().sprite;
			isRaycastLocationValid = false;  
		}
	}  

	public void OnDrag(PointerEventData eventData)  
	{  

		dragGo.transform.position = Input.mousePosition;  
	}  

	public void OnEndDrag(PointerEventData eventData)  
	{  
		GameObject go = eventData.pointerCurrentRaycast.gameObject; 
		transform.SetParent (parentObj.transform);
		isRaycastLocationValid = true;  
		dragGo.SetActive (false);
		if(AllowMove()){
			// 判断落点目标是否有对象
			if (go!=null && "Skill".Equals (go.tag)) {
				// 如果是快捷栏就交换两个技能 
				if (go.transform.parent.gameObject.name.IndexOf ("quickSkill") > -1 && currentSkill.transform.parent.name.IndexOf("quickSkill")>-1) {
					currentSkill.GetComponent<Image> ().sprite =go.transform.GetComponent<Image> ().sprite;
				}
				if ("mouseLeft".Equals (go.transform.parent.name)) {
					GameObject.Find ("player").GetComponent<RoleAttack> ().mouseLeftSkill = this;
				} else if ("mouseRight".Equals (go.transform.parent.name)) {
					GameObject.Find ("player").GetComponent<RoleAttack> ().mouseRightSkill = this;
				}
				go.transform.GetComponent<Image> ().sprite = dragGo.GetComponent<Image> ().sprite;
			} else {
				if (currentSkill != null) {
					string parentName = currentSkill.transform.parent.name;

					if("mouseLeft".Equals(parentName)){
						currentSkill.GetComponent<Image> ().sprite = DefaultSet.instance.MouseSkillLeftIcon;
					}else if("mouseRight".Equals(parentName)){
						currentSkill.GetComponent<Image> ().sprite = DefaultSet.instance.MouseSkillRightIcon;
					}else if(parentName.IndexOf("quickSkill")>-1){
						currentSkill.GetComponent<Image> ().sprite = DefaultSet.instance.QuickSkillIcon;
					}
				}
			}
		}
		currentSkill = null;
	}  

}
