using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class MyDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Vector3 mouseDownPosition;
	private RectTransform rt;
	private Vector3 bagBeginPosition;
	// transform the screen point to world point int rectangle
	Vector3 globalMousePos;
	public void Start(){
	}
	// begin dragging
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (eventData.selectedObject == null) {
			mouseDownPosition = Input.mousePosition; 
			bagBeginPosition = transform.GetComponent<RectTransform>().position;

		}
;
			
	}

	// during dragging
	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.selectedObject == null) { 	
		Vector3 offset = Input.mousePosition - mouseDownPosition;
		mouseDownPosition = Input.mousePosition;
		bagBeginPosition = bagBeginPosition + offset;

		transform.GetComponent<RectTransform>().position = bagBeginPosition;
		}
	}

	// end dragging
	public void OnEndDrag(PointerEventData eventData)
	{
		
	}


}