
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

	public static UIManager instance = null;

	public Transform mainPage;
	// 加载所有uiImage图片 
	public Sprite[] uiImages ;
	// 加载所有剑图片 


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

	public Transform miningDetail ;
	public Transform totalMining ;
	public Transform petGet ;
	public Transform petDetail;
	// 主角
	public Role role=null;
	// 锻造显示
	Transform itemShow ;




	void Awake()
	{
		uiInit ();

	}

    private void uiInit(){
		instance = SingletonManger.GetSingleton<UIManager> ();
		uiImages = Resources.LoadAll<Sprite>("Image/uiImage"); 

		miningDetail = instance.transform.Find ("Canvas/miningDetail");
		mainPage=instance.transform.Find ("Canvas/mainPage");
		petGet =instance.transform.Find ("Canvas/petGet");
		petDetail = instance.transform.Find ("Canvas/petDetail");
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
		totalMining = instance.transform.Find ("Canvas/totalMining");

		GameObject[] mainButtons = GameObject.FindGameObjectsWithTag ("mainButton");
		GameObject[] subButtons = GameObject.FindGameObjectsWithTag ("subButton");
		// 主页面按钮
		for(int i=0;i<mainButtons.Length;i++){
			// 该变量解决闭包问题
			GameObject go = mainButtons [i];
			go.GetComponent<Button> ().onClick.AddListener (delegate(){mainButtonClick(go);});
		}
		// 子页面按钮
		for(int i=0;i<subButtons.Length;i++){
			// 该变量解决闭包问题
			GameObject go = subButtons [i];
			go.GetComponent<Button> ().onClick.AddListener (delegate(){subButtonClick(go);});
		}
		itemShow = makeSub.Find ("itemShow");
		print ("itemshow" +itemShow);
	}
	void Start () {

	}

	void Update(){
		
	}
	private void closeAllUI (){
		miningDetail.gameObject.SetActive (false);
		petGet .gameObject.SetActive (false);
		petDetail .gameObject.SetActive (false);
		roleSub.gameObject.SetActive (false);
		bagSub.gameObject.SetActive (false); 
		bagItem.gameObject.SetActive (false);
		makeSub.gameObject.SetActive (false);
		saleSub.gameObject.SetActive (false);
		friendSub.gameObject.SetActive (false);
		setSub.gameObject.SetActive (false);
		guildSub.gameObject.SetActive (false);
		domainSub.gameObject.SetActive (false);
		darkDoor.gameObject.SetActive (false);
		battleSub.gameObject.SetActive (false);
		totalMining.gameObject.SetActive (false); 
	}
	private void friendController(GameObject go){
		if("searchPlayer".Equals(name)){

		}
	}
	private void makeController(GameObject go){
		itemShow.Find ("equipLevel/Text").GetComponent<Text> ().text ="12";
		string equipType = "arch";  
		string makeData = "gold"; // 锻造材料 默认黄金
	
		if("arch".Equals(go.name)){
			equipType = "arch";
		}else if("axe".Equals(go.name)){
			equipType = "axe";
		}else if("sword".Equals(go.name)){
			equipType = "sword";
		}else if("knife".Equals(go.name)){
			equipType = "knife";
		}else if("cloth".Equals(go.name)){
			equipType = "cloth";
		}else if("hand".Equals(go.name)){
			equipType = "hand";
		}else if("helmet".Equals(go.name)){
			equipType = "helmet";
		}else if("jewel".Equals(go.name)){
			//制作宝石
			equipType = "jewel";
		}else if("neckLace".Equals(go.name)){
			equipType = "neckLace";
		}else if("ring".Equals(go.name)){
			equipType = "ring";
		}else if("shoes".Equals(go.name)){
			equipType = "shoes";
		}else if("belt".Equals(go.name)){
			// 制作腰带
			equipType = "belt";
		}else if("makeButton".Equals(go.name)){
		//	item.createEquip (	makeSub.Find ("equipName").GetComponent<InputField>().text,equipType,makeData,role.makeLevel);
		}else if("gold".Equals(go.name)){
			makeData = "gold";
		}else if("iron".Equals(go.name)){
			makeData = "iron";
		}else if("silver".Equals(go.name)){
			makeData = "silver";
		}else if("copper".Equals(go.name)){
			makeData = "copper";

		}
		// 重置其他材料外框
		makeSub.Find ("iron").GetComponent<Image>().sprite = uiImages [89];
		makeSub.Find ("gold").GetComponent<Image>().sprite = uiImages [89];
		makeSub.Find ("silver").GetComponent<Image>().sprite = uiImages [89];
		makeSub.Find ("copper").GetComponent<Image>().sprite = uiImages [89];
		makeSub.Find (makeData).GetComponent<Image>().sprite = uiImages [71];
		

		itemShow.Find("icon/image").GetComponent<Image>().sprite =Resources.Load("Image/"+equipType, typeof(Sprite)) as Sprite;
	}
	private void saleController(GameObject go){
		 if("searchItem".Equals(name)){

		}
	}
	private void battleController(GameObject go){

	}
	private void roleController(GameObject go){

	}
	private void domainController(GameObject go){
		 if("noCapture".Equals(name)){

		}else if("captured".Equals(name)){

		}else if("robCapture".Equals(name)){

		}
	}

	private void bagController(GameObject go){
		 if("sale".Equals(name)){

		}else if("put".Equals(name)){

		}else if("split".Equals(name)){

		}else if("equip".Equals(name)){

		}else if("explain".Equals(name)){

		}else if("tidy".Equals(name)){

		}
	}
	private void setController(GameObject go){
		// 背景音乐
		if("bgMusic".Equals(name)){

		}else if("soundButton".Equals(name)){ // 音效 .

		}
	}
	private void guildController(GameObject go){
		if("searchGuild".Equals(name)){

		}
	}

	public void subButtonClick(GameObject go){
		// 获取父类名字
		string name = go.transform.parent.name;
		if (go == null)
			return;
	
		if("domainSub".Equals(name)){
			domainController (go);
		}else if("friendSub".Equals(name)){
			friendController (go);
		}else if("makeSub".Equals(name)){
			makeController (go);
		}else if("guildSub".Equals(name)){
			guildController (go);
		}else if("roleSub".Equals(name)){
			roleController (go);
		}else if("saleSub".Equals(name)){
			saleController (go);
		}else if("battleSub".Equals(name)){
			battleController (go);
		}else if("bagSub".Equals(name)){
			bagController (go);
		}else if("setSub".Equals(name)){
			setController (go);
		}


	}

	public void mainButtonClick(GameObject go){
		string name = go.name;
		if("exit".Equals(name)){
			// 退出按钮隐藏界面
			go.transform.parent.gameObject.SetActive (false);
			return;
		}
		// 关闭所有ui 
		closeAllUI ();
		Transform tf=null;
		if("mainCapture".Equals(name)){
			tf = domainSub;

		}else if("mainMake".Equals(name)){
			tf = makeSub;
			itemShow.Find ("equipLevel/Text").GetComponent<Text>().text= ""+role.makeLevel;
			makeSub.Find ("makeLevel/Text").GetComponent<Text>().text= ""+role.makeLevel;
		}else if("mainSale".Equals(name)){
			tf = saleSub;
		}else if("mainBag".Equals(name)){
			tf = bagSub;
		}else if("mainDoor".Equals(name)){
			tf = darkDoor;
		}else if("mainSocial".Equals(name)){
			tf = friendSub;
		}else if("setButton".Equals(name)){
			tf = setSub;
		}

		if(tf!=null){
			tf.gameObject.SetActive (true);
			tf.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0,-46);
		}

	}
}
