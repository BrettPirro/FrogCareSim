using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Frog.ValMangment;


namespace Frog.UI 
{
    public class DraggableOBJManager : MonoBehaviour
    {
        int UpdatedInt = 0;
        bool CurrentlyDropped=false;
       

        private void Start()
        {
            if (TriggerEatResource.current != null) 
            {
                TriggerEatResource.current.UpdateCurrentlyEatting += GrabCurrentFoodInt;
                TriggerEatResource.current.ItemDropped += GrabCurrentlyDropped;
                TriggerEatResource.current.DropEstablished += DroppedEstablished;
            }


            foreach(DraggableUIElement i in FindObjectsOfType<DraggableUIElement>()) 
            {
                i.ValueCurrentlyBeingDragged += ReturnValueFromDrag;
                i.DroppedInArea += CheckifDroppedinArea;
            }
        }

        private void OnEnable()
        {
            if (TriggerEatResource.current != null)
            {
                TriggerEatResource.current.UpdateCurrentlyEatting += GrabCurrentFoodInt;
                TriggerEatResource.current.ItemDropped += GrabCurrentlyDropped;
                TriggerEatResource.current.DropEstablished += DroppedEstablished;
            }
        }

        private void OnDisable()
        {
            if (TriggerEatResource.current != null)
            {
                TriggerEatResource.current.UpdateCurrentlyEatting -= GrabCurrentFoodInt;
                TriggerEatResource.current.ItemDropped -= GrabCurrentlyDropped;
                TriggerEatResource.current.DropEstablished -= DroppedEstablished;
            }
        }


        private void Update()
        {


            if (CurrentlyDropped) 
            {
                Debug.Log("Ate: "+UpdatedInt);
               
            }

        }


        public int GrabCurrentFoodInt() 
        {
            return UpdatedInt;
        }

        public bool GrabCurrentlyDropped() 
        {
            return CurrentlyDropped;
        }


        public void CheckifDroppedinArea(bool val) 
        {
            CurrentlyDropped = val;
        }
        public void ReturnValueFromDrag(int val) 
        {
            UpdatedInt = val;
        }

        public void DroppedEstablished() 
        {
            CurrentlyDropped = false;
        }


    }

}
