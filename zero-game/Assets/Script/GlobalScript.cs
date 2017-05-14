using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 当前类保留所有基础对象不被销毁，在初始化场景的时候调用，往后不在调用（否则会造成多次的DontDestroyOnload）
public class GlobalScript : MonoBehaviour {

	void Awake(){
		Object[] initsObjects = GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (Object go in initsObjects) {
			DontDestroyOnLoad(go);
		}
	}
}
