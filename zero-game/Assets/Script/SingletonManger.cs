using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManger {  
	private static SingletonManger instance; 
	// 初始化 将所有固定对象保存 以便切换场景不回收

	public static void CreateInstance () {  
		
		if (instance == null) {  
			instance = new SingletonManger ();  
			// 持久对象初始化
		
			instance.main = GameObject.Find ("Main");
			if (instance.main == null) {  
				instance.main = new GameObject ("Main");  
			} 
			Object.DontDestroyOnLoad (instance.main);  
		}  
	}  

	GameObject main;  

	public static SingletonManger SharedInstance {  
		get {  
			if (instance == null) {  
				CreateInstance ();  
			}  
			return instance;  
		}  
	}  

	public static T GetSingleton <T> () where T : UnityEngine.Component {
		
		T t = SharedInstance.main.GetComponent<T> ();  
		if (t != null) {  
			return t;  
		}  
		return SharedInstance.main.AddComponent <T> ();  
	}  
		
}