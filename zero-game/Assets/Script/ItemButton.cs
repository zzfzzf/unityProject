using UnityEngine;  
using System.Collections;  
using UnityEngine.UI;  
using UnityEngine.EventSystems;  

//ICanvasRaycastFilter可以控制射线是否穿透物品，也可以使用CanvasGroup的blocksRaycasts  
public class ItemButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ICanvasRaycastFilter,IPointerEnterHandler,IPointerExitHandler {  

	private static Transform canvasTra;  
	private Transform nowParent;//一般来说，物品是格子的子物体，nowParent记录的是当前物品属于哪个格子  
	private bool isRaycastLocationValid = true;//默认射线不能穿透物品  
	GameObject itemTip;
	public void Start(){
		canvasTra = GameObject.Find("Canvas").transform; 

	}
	public void OnBeginDrag(PointerEventData eventData)  
	{  
			
		if (canvasTra == null) canvasTra = GameObject.Find("Canvas").transform;  

		nowParent = transform.parent;  
		transform.SetParent(canvasTra);//将当前拖拽的物品置前    
		isRaycastLocationValid = false;  
	}  

	public void OnDrag(PointerEventData eventData)  
	{  
		
		transform.position = Input.mousePosition;  
	}  

	public void OnEndDrag(PointerEventData eventData)  
	{  

		GameObject go = eventData.pointerCurrentRaycast.gameObject;  

		if (go != null)  
		{  

			if (go.tag.Equals("Grid"))//放置到空格子  
			{  
				SetParentAndPosition(transform, go.transform);  
			}  
			else if (go.tag.Equals("Item"))//交换位置，注意可能需要把物品下的子物体的Raycast Target关掉  
			{  
				
				SetParentAndPosition(transform, go.transform.parent);  
				SetParentAndPosition(go.transform, nowParent);  
			}  
			else  
			{  
				SetParentAndPosition(transform, nowParent);  
			}  
		}  
		else  
		{  
			SetParentAndPosition(transform, nowParent);  
		}  
		isRaycastLocationValid = true;  
	}  

	private void SetParentAndPosition(Transform child, Transform parent)  
	{  
		child.SetParent(parent);  
		child.position = parent.position;  
	}  

	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)  
	{  
		return isRaycastLocationValid;  
	}  

	public void OnPointerEnter(PointerEventData eventData){//鼠标进入UI调用该方法
		UIManager.instance.itemTip.GetComponent<Text>().text="\t\t\t中华人民共\n和国";
		if (UIManager.instance.ShowItemTips)
		{
			//鼠标悬在同一物品上，则已经显示的tips保持原样，避免闪烁
			if (eventData.lastPress == eventData.pointerEnter)
			{
				return;
			}

			//显示提示信息
			if (!UIManager.instance.itemTip.activeSelf)
			{
				
				UIManager.instance.itemTip.transform.position =  transform.position + new Vector3(120, -120, 0);
				UIManager.instance.itemTip.SetActive(true);

			}
		}
	}
	public void OnPointerExit(PointerEventData evenData){//鼠标离开UI调用该方法
		UIManager.instance.itemTip.SetActive(false);
	}

}  