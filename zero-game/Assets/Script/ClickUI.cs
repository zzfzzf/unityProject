using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickUI : MonoBehaviour {


	void Start(){
		
	}

	public void onClick (){
		if(transform.name.Equals("closeBag")){
			UIManager.instance.OpenOrCloseBag ();  
			this.gameObject.SetActive (false);
		}else if(transform.name.Equals("uiBag")){
			UIManager.instance.OpenOrCloseBag ();  
			                 
		}else if(transform.name.Equals("uiSkill")){
			UIManager.instance.OpenOrCloseSkill ();
		}

	}  

	public void onClickx (GameObject obj){
		if(transform.name.Equals("closeBag")){
			obj.SetActive (false);  
			this.gameObject.SetActive (false);
		}else if(transform.name.Equals("uiBag")){
			GameObject closeBag=obj.transform.Find ("closeBag").gameObject;
			if (obj.activeSelf) {
				obj.SetActive (false);	
				closeBag.SetActive (false);
			} else {
				obj.SetActive (true);
				closeBag.SetActive (true); 
			}
		
		}

	}  

}
