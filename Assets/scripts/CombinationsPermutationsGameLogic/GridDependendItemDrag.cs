﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridDependendItemDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // Start is called before the first frame update
    [SerializeField] private Canvas canvas;

    public int speed = 1000;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform startTransform;
    private Vector3 startPosition;
    private bool isNeedToMove = false; 
    private bool badgeSpoted = false;
    
    void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if(isNeedToMove)
            MoveTowardsTarget();
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup  = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    private void MoveTowardsTarget() {
        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, startPosition, step);
        if(transform.position == startPosition)
            isNeedToMove = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
   		GameObject canvas_parent = GameObject.Find("Canvas");
        FillGridWithNumbers src = canvas_parent.GetComponent<FillGridWithNumbers>();
        src.currentDragObject = gameObject;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
   		canvasGroup.alpha = 1f;
   		canvasGroup.blocksRaycasts = true;
   		GameObject canvas = GameObject.Find("Canvas");
        ControlSequance src = canvas.GetComponent<ControlSequance>();
        bool spotted = false;
        foreach(var spot in src.spots)
            if(spot.transform.position == gameObject.transform.position)
                spotted = true;
        if(!spotted){
            isNeedToMove = true;
            badgeSpoted = true;   
        }
    }

        public void OnPointerDown(PointerEventData eventData)
        {
        	//var obj = new GameObject(gameObject.name);
        	//obj = gameObject;
            //Debug.Log("OnPointerDown");
        }
}
