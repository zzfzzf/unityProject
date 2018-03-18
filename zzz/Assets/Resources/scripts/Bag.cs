using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bag : MonoBehaviour {
	public int gridCount=41;
	public List<Transform> gridList = new List<Transform>();
	public string gridPrefabName = "prefab/grid";
	// 当前背包物品
	public List<Item> itemList = new List<Item>();

	void Awake(){
		// 遍历生成背包格子
		bagInit();

	}
	 
	void bagInit (){
		for (int i = 0; i < gridCount; i++)  
		{  
			GameObject go = Instantiate(Resources.Load(gridPrefabName)) as GameObject;  
			go.name = "grid" + (i+1);
			go.transform.SetParent(transform, false);  
			gridList.Add(go.transform);   
		}  

	}
	void Update () {
		
	}
    
	// 创建装备 type 装备类型 data 制作材料 makeLevel 锻造等级
	public void createEquip(string equipName,string type,string data,int makeLevel){
		// i 01234 对应普通 高级 稀有 史诗 传奇
		// 物品公式 基础属性  Random(1,锻造等级*21) 

		Item equip = gameObject.AddComponent<Item>();
		equip.itemName = equipName;
		if("arch".Equals(type)){
			// 给当前格子新建子对象

		//	baseProperty.Add ("hit",Random.Range(1,makeLevel*20));

		}else if("axe".Equals(type)){
		//	baseProperty.Add ("hit",Random.Range(1,makeLevel*20));

		}else if("knife".Equals(type)){
	//		baseProperty.Add ("hit",Random.Range(1,makeLevel*20));
		}else if("sword".Equals(type)){
		//	baseProperty.Add ("hit",Random.Range(1,makeLevel*20));
		}
			
		// 给当前格子新建子对象 
		GameObject go = Instantiate(Resources.Load("prefab/item")) as GameObject; 
		// 设置物品名称
		go.GetComponent<Item> ().itemName = equipName;

		// 设置物品图片
		setEquipIcon(go,type,makeLevel);
		// 设置item父类对象为当前空格子
		go.transform.SetParent(gridList[itemList.Count], false);  


	}
	//添加物品  
	public void createItem(string itemName, int count = 1)  
	{  
		
		// 判断当前格子是否足够
		if(itemList.Count>=gridCount){
			// 物品格子已满,添加物品失败
			return;
		}
		//如果有相同的物品，则只是更改包里该物品的数量；否则实例化该物品，改数量    
		for (int i = 0; i < itemList.Count; i++)  
		{  
			//如果有物品 判断相同 没有物品则添加物品 
			if(itemList[i].itemName.Equals(itemName))  
			{  
					// 给当前item增加一个数量 
					itemList[i].itemNum+=count;
					// 物品计数+1 更新文字显示
					itemList[i].transform.Find("itemNum").GetComponent<Text>().text=itemList[i].itemNum+"";
					return;  
			}  
		}  


					// 给当前格子新建子对象 
					GameObject go = Instantiate(Resources.Load("prefab/item")) as GameObject; 
					// 设置物品名称
					go.GetComponent<Item> ().itemName = itemName;
					// 设置物品数量
					go.GetComponent<Item> ().itemNum = count;
					// 设置物品图片
					setItemIcon(go,itemName);
					// 设置item父类对象为当前空格子
					go.transform.SetParent(gridList[itemList.Count], false);  
					// 设置当前item数量
					go.GetComponent<Item> ().itemNum+=count;
					// 更新当前item数量显示
					go.transform.Find("itemNum").GetComponent<Text>().text=go.GetComponent<Item> ().itemNum+"";
					return;  
			
	}  
	// 设置物品图片 （根据name来判断）
	private void setItemIcon (GameObject go,string itemName){
		// 图片编号 组图
		int iconNum  =0;
		if("gold".Equals(itemName)){
			iconNum = 81;
		}else if("silver".Equals(itemName)){
			iconNum = 5;
		}else if("copper".Equals(itemName)){
			iconNum = 0;
		}else if("iron".Equals(itemName)){
			iconNum = 2;
		}
		go.GetComponent<Image> ().sprite = UIManager.instance.uiImages [iconNum];
	}
	private void setEquipIcon(GameObject go,string type,int makeLevel){
		// 定义一个装备组 0~锻造等级上限 等级为图片编号 
		int iconNum  =0;
		if("arch".Equals(type)){
			iconNum = 81;
		}else if("axe".Equals(type)){
			iconNum = 5;
		}else if("knife".Equals(type)){
			iconNum = 0;
		}else if("sword".Equals(type)){
			iconNum = 2;
		}
	}

	// 删除装备

	// 删除物品
	public void removeItem(Item item){
		// 删除当前item对应的对象
		DestroyImmediate(item.gameObject);
		// 从物品列表中移除item
		itemList.Remove (item);

	}
	// 拆分物品
	public void splitItem(Item item){
		
	}

	//整理  可能有bug
	public void tidy()  { 
		List<Item> tempList = new List<Item>();  
		for (int i = 0; i < itemList.Count; i++) {
			if (itemList [i].type.Equals ("item")) {
				tempList.Add (itemList[i]);
				itemList.Remove (itemList[i]);
			}
		}
		// 把材料的item又添加回来
		itemList.AddRange (tempList);
	
		for(int i=0;i<gridList.Count;i++){
				itemList [i].transform.SetParent (gridList [i]);
				itemList [i].transform.position = gridList [i].position;
		}  
	
	}  

}
