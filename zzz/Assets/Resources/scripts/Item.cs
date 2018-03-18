using UnityEngine;  
using System.Collections.Generic;  
using UnityEngine.UI;  
using UnityEngine.EventSystems;  

//ICanvasRaycastFilter可以控制射线是否穿透物品，也可以使用CanvasGroup的blocksRaycasts  
public class Item  : MonoBehaviour{  
	public Transform itemTip;
	public string id="";
	public string itemName = "";
	public int itemNum;
	public Image icon;
	// 判断当前物品类型是物品还是装备
	public string type="item";
	// 附加的所有属性
	public List<Dictionary<string,double>> properties =new List<Dictionary<string, double>>();
	// 装备技能
	public List<Skill> skills = new List<Skill>();
	// 附加技能
	public Skill addSkill1; 
	public Skill addSkill2;
	public Dictionary<string,double> baseProperty = new Dictionary<string, double> ();
	void Start(){
		itemTip = UIManager.instance.bagSub.Find ("itemTip");
	}

	void Update() {
		// item json数据为 依次为property1 2 3 4 5 skill1，skill2 用list装起来
		// 触屏显示
		if (Input.touchCount == 1) { // 一个手指
			if (Input.touches [0].phase == TouchPhase.Began) {
				
				// 装备属性设置 只要有一个没找到 后面都找不到
				itemTip.gameObject.SetActive(true);
				itemTip.Find ("name/Text").GetComponent<Text>().text="巨峰";
				itemTip.Find ("level/num").GetComponent<Text>().text="10";
				itemTip.Find ("quaility/Text").GetComponent<Text>().text="稀有";
				itemTip.Find ("baseProperty/num").GetComponent<Text> ().text = "+300攻击力";
				itemTip.Find ("icon").GetComponent<Image> ().sprite = Resources.Load("Image/baseSword", typeof(Sprite)) as Sprite;
				// 多个属性由json过来 


				string propertyStr = "";
				// 属性颜色
				string color="black";
				// 循环显示所有附加属性带技能
				for(int i =0;i<properties.Count;i++){
					if (i != 0) {
						propertyStr += "\n";
					}
					if (i == 1){
						color="blue";
					}else if(i==2){
						color="purple";
					}else if(i==3){
						color = "yellow";
					}else{
						color="orange";
					}

					propertyStr +="<color="+color+">";
					propertyShow (properties[i],propertyStr);
					propertyStr +="</color>";

				}
				itemTip.Find ("addProperty/Text").GetComponent<Text> ().text = propertyStr;

				itemTip.Find ("addSkill1/Text").GetComponent<Text> ().text = "+300攻击力\n<color=red>+400攻击力</color>\n+<color=blue>+300暴击</color>";

				itemTip.Find ("addSkill2/Text").GetComponent<Text> ().text = "";

				// 定位itemtip
				Vector2 itemTipPos;
				Vector2 gridPos = this.transform.parent.GetComponent<RectTransform>().anchoredPosition;
				if (gridPos.x <= 530 && gridPos.y > -720) {
					itemTipPos = new Vector2 (180, -300);
				} else if (gridPos.x > 530 && gridPos.y > -720) {
					itemTipPos = new Vector2 (-120, -300);
				} else if (gridPos.x > 530 && gridPos.y <= -720) {
					itemTipPos = new Vector2 (-120, 120);
				} else {
					itemTipPos = new Vector2 (180, 120);
				}
				itemTip.GetComponent<RectTransform> ().anchoredPosition = gridPos+ itemTipPos;
			}
			// 松手面板消失
			if(Input.touches[0].phase == TouchPhase.Ended){
				itemTip.gameObject.SetActive(false);
			}
		}
	}

	// 遍历显示 百分比属性应该要加%%
	private double propertyShow(Dictionary<string,double> dy,string str){
		if (dy == null)
			return 0;
		if (str == null) {
			return 0;
		}
	
		if(dy.ContainsKey("maxHp")){
			str += "+" + dy ["maxHp"] + "最大生命";
		}else if(dy.ContainsKey("maxMp")){
			str += "+" + dy ["maxMp"] + "最大能量";
		}else if(dy.ContainsKey("attack")){
			str += "+" + dy ["attack"] + "攻击力";
		}else if(dy.ContainsKey("crit")){
			str += "+" + dy ["crit"]*100 + "%暴击";
		}else if(dy.ContainsKey("critDamage")){
			str += "+" + dy ["critDamage"]*100 + "%暴击伤害";
		}else if(dy.ContainsKey("speed")){
			str += "+" + dy ["speed"] + "速度";
		}else if(dy.ContainsKey("defend")){
			str += "+" + dy ["defend"] + "防御";
		}else if(dy.ContainsKey("drawHp")){
			str += "+" + dy ["drawHp"]*100 + "%生命偷取";
		}else if(dy.ContainsKey("drawMp")){
			str += "+" + dy ["drawMp"]*100 + "%能量偷取";
		}else if(dy.ContainsKey("hit")){
			str += "+" + dy ["hit"] + "命中";
		}else if(dy.ContainsKey("dodge")){
			str += "+" + dy ["dodge"] + "闪避";
		}else if(dy.ContainsKey("derate")){
			str += "+" + dy ["derate"]*100 + "%减伤";
		}else if(dy.ContainsKey("luck")){
			str += "+" + dy ["luck"] + "幸运";
		}else if(dy.ContainsKey("reflect")){
			str += "+" + dy ["reflect"]*100 + "%反射";
		}
		return 0;
	}

}  