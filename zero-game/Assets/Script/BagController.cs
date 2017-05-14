using UnityEngine;  
using System.Collections;  
using UnityEngine.UI;  
using System.Collections.Generic;  

public class BagController : MonoBehaviour {
	// 一页背包格子数量
	public int gridCount = 60;
	public List<Transform> gridList = new List<Transform>();
	public GameObject obj;
	private string gridPrefabName = "Prefab/Object/Grid"; 
	// Use this for initialization
	void Start () {
		
	}
	void Awake ()   
	{  
		for (int i = 0; i < gridCount; i++)  
		{  
			GameObject go = Instantiate(Resources.Load(gridPrefabName)) as GameObject;  
			go.name = "grid" + (i+1);
			go.transform.SetParent(transform, false);  
			gridList.Add(go.transform);   
		}  
	}  

	void Update(){
		if(Input.GetKey(KeyCode.K)){
			AddItem ("测试图片");
		}
		if(Input.GetKey(KeyCode.Y)){
			AddItem ("魔法屋");
		}
		if(Input.GetKey(KeyCode.R)){
			print ("整理背包");
			Tidy();
		}
	}

	//添加物品  
	public void AddItem(string itemName, int count = 1)  
	{  
		//如果有相同的物品，则只是更改包里该物品的数量；否则实例化该物品，改数量  
		bool hasSameItem = false;  
		for (int i = 0; i < gridList.Count; i++)  
		{  
			//如果有物品  
			if(gridList[i].childCount > 0)  
			{  
				Transform image = gridList[i].GetChild(0);
			

				//如果有该物品 
				if (itemName.Equals(image.GetComponent<Image>().sprite.name))  
				{  
					hasSameItem = true;  
					// 物品计数+1
					ModifyCount(image.GetChild(0).GetComponent<Text>(), count);  
					break;  
				}  
			}  
		}  
	
		if(!hasSameItem)  
		{  
			for (int i = 0; i < gridList.Count; i++)  
			{  
				if(gridList[i].childCount == 0)  
				{  
					// 给当前格子新建子对象
					GameObject go = Instantiate(Resources.Load("Prefab/Object/itemImage")) as GameObject; 
					// 这个sp从数据库来
					go.GetComponent<Image>().sprite =  Resources.Load<Sprite>("images/"+itemName);
			
					print ("新建一个物体"+go.GetComponent<Image>().sprite);
					go.transform.SetParent(gridList[i], false);  
					go.transform.GetChild(0).GetComponent<Text>().raycastTarget =false;
					ModifyCount(go.transform.GetChild(0).GetComponent<Text>(), count);  
					break;  
				}  
			}  
		}  
	}  

	private void ModifyCount(Text text, int count)  
	{  
		int temp = int.Parse(text.text);  
		temp += count;  
		text.text = temp.ToString();  
	}  

	//整理  
	public void Tidy()  
	{  
		List<Transform> tempList = new List<Transform>();  
		for (int i = 0; i < gridList.Count; i++)  
		{  
			if (gridList [i].childCount > 0) {
				tempList.Add (gridList [i].GetChild (0));
			}
		}  

		for (int i = 0; i < tempList.Count; i++)  
		{  
			tempList[i].SetParent(gridList[i]);  
			tempList[i].position = gridList[i].position;  
		}  
	}  


}
