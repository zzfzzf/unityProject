using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 该单例管理对象只存在一个 管理各种单例
public class SingletonManger {  
	private static SingletonManger instance; 

	public static void CreateInstance () {  
		
		if (instance == null) {  
			instance = new SingletonManger ();  
			// Main为最大的一个对象,用于获取子对象
			instance.main = GameObject.Find ("Main");
			if (instance.main == null) {  
				instance.main = new GameObject ("Main");  
			} 
 
		}  
	}  

	GameObject main;  
	// 如果该单例对象为创建则创建 并且初始化Main 否则要去判断该单例是否为null
	public static SingletonManger SharedInstance {  
		get {  
			if (instance == null) {  
				CreateInstance ();  
			}  
			return instance;  
		}  
	}  
	// 每创造一个单例 就在main对象增加一个单例组件
	public static T GetSingleton <T> () where T : UnityEngine.Component {

		T t = SharedInstance.main.GetComponent<T> ();  
		if (t != null) {  
			return t;  
		}  
		return SharedInstance.main.AddComponent <T> ();  
	}  

}
