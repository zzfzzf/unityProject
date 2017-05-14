using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	
	public static UIManager instance = null;
	/// 包裹对象
	public GameObject bag;
	/// 物品提示
	public GameObject itemTip;
	/// 技能提示  
	public GameObject skillTip;
	/// 技能树对象
	public GameObject skill;
	/// 是否允许显示物品信息提示
	public bool ShowItemTips = true;
	/// 是否允许显示技能信息提示
	public bool ShowSkillTips = true;
	// 用于拖拽最大父类
	public GameObject canvasTra;  

	public GameObject dragSkillObj;

	void Awake()
	{
		instance = SingletonManger.GetSingleton<UIManager> ();
	}

	void Start () {
		itemTip.SetActive(false);
		dragSkillObj.SetActive (false);
		//SortItems();
	}

	void Update () {
		if (Input.GetKeyUp(KeyCode.B) || Input.GetKeyUp(KeyCode.I))
		{
			OpenOrCloseBag();
		}
	}

	/// <summary>
	/// 打开或关闭包裹界面
	/// </summary>
	public void OpenOrCloseBag()
	{
		bag.SetActive(!bag.activeSelf);
	}

	public void OpenOrCloseSkill(){
		skill.SetActive(!skill.activeSelf);
	}
}
