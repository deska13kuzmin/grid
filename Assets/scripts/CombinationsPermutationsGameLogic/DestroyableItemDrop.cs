using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DestroyableItemDrop : MonoBehaviour, IDropHandler
{
	public GameObject obj;
	public GameObject StartOfGridObject;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null){
        	//eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        	var v = GetComponent<RectTransform>().anchoredPosition;
        	CreateDependendBlock(v);
        }
    }

    public void CreateDependendBlock(Vector2 v)
    {
    	GameObject canvas = GameObject.Find("Canvas");
        FillGridWithNumbers scr = canvas.GetComponent<FillGridWithNumbers>();
        var obj_ = scr.GetDependedObject(gameObject);
    	gameObject.GetComponent<Text>().text = obj_.GetComponent<Text>().text;    	
    	scr.CObject.transform.SetSiblingIndex(1000);
    	scr.AObject.transform.SetSiblingIndex(1000);
    }

}
