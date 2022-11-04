
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DraggableUIElement : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler
{
    enum DraggableState { Fly, Water }

    [SerializeField]DraggableState CurrentState;
    [SerializeField] float ItemSpeed = 0.05f;
    [SerializeField]RectTransform LastPosition;
    bool CapturedData=false;
    public Action<int> ValueCurrentlyBeingDragged;
    public Action<bool> DroppedInArea;
    RectTransform rectTransform;
    Vector3 Velocity = Vector3.zero;
    bool Moving=false;
    public bool inArea;
    bool movingWithFinger = false;


    private void Awake()
    {
        rectTransform = transform as RectTransform;
       
    }


    
   


    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform,eventData.position,eventData.pressEventCamera,out var globalMouseData)) 
        {
            rectTransform.position = Vector3.SmoothDamp(rectTransform.position, globalMouseData, ref Velocity, ItemSpeed);
            movingWithFinger = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DroppedInArea(inArea);
        Moving = true;
        movingWithFinger = false;
        Debug.Log("Dropped");
      
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!CapturedData) 
        {
            
            CapturedData = true;
        }
        ValueCurrentlyBeingDragged(ConvertEnumToInt());

        Moving = false;
    }

    private void Update()
    {
        if (Moving) 
        {
           
            rectTransform.position = Vector3.SmoothDamp(rectTransform.position, LastPosition.position, ref Velocity, ItemSpeed);
        }
    }

    int ConvertEnumToInt() 
    {
        switch (CurrentState)
        {
            case DraggableState.Fly:
                return 1;
            default:
                return 2;
                
        }
    }
    
    public bool IsMoving() 
    {
        return !Moving&&movingWithFinger;
    }

    public Vector3 GrabPos() 
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out var pos);
        return pos;
    }



}
