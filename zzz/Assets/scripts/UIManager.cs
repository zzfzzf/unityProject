
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public static UIManager instance = null;

	public Transform mainPage;
	public Transform petSub  ;
	public Transform roleSub ;
	public Transform bagSub  ;
	public Transform bagItem ;
	public Transform makeSub ;
	public Transform saleSub  ;
	public Transform friendSub ;
	public Transform setSub  ;
	public Transform guildSub ;
	public Transform domainSub;
	public Transform darkDoor ;
	public Transform battleSub ;

	void Awake()
	{
		instance = SingletonManger.GetSingleton<UIManager> ();
		mainPage=instance.transform.Find ("Canvas/mainPage");
		petSub =instance.transform.Find ("Canvas/petSub");
		roleSub=instance.transform.Find ("Canvas/roleSub");
		bagSub =instance.transform.Find ("Canvas/bagSub");
		bagItem=instance.transform.Find ("Canvas/bagItem");
		makeSub=instance.transform.Find ("Canvas/makeSub");
		saleSub=instance.transform.Find ("Canvas/saleSub");
		friendSub=instance.transform.Find ("Canvas/friendSub");
		setSub =instance.transform.Find ("Canvas/setSub");
		guildSub=instance.transform.Find ("Canvas/guildSub");
		domainSub=instance.transform.Find ("Canvas/domainSub");
		darkDoor=instance.transform.Find ("Canvas/darkDoor");
		battleSub=instance.transform.Find ("Canvas/battleSub");


	}

	void Start () {

	}

	void Update () {
		
	}


}
