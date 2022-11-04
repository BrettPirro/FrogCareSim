using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Frog.ValMangment 
{
    
    public class TriggerEatResource : MonoBehaviour
    {
        enum DraggableState {Fly,Water,Null }

        [SerializeField] float WaterTaken = 15f;
        [SerializeField] float FoodTaken = 20f;


        [SerializeField]DraggableState state=DraggableState.Null;


        //1 is flys 2 is water

        int CurrentlyEattingWhat = 0;
        public Action<int[]> SubResource;
        public Func<int> UpdateCurrentlyEatting;
        public Func<bool> ItemDropped;


        public Action DropEstablished;
        public Action<float> Drinking;
        public Action<float> EattingFood;
        public Func<bool[]> IsInventoryZero;
        static public TriggerEatResource current;


        
        private void Awake()
        {
            current = this;
        }

        private void Update()
        {
            CurrentlyEattingWhat = UpdateCurrentlyEatting();
            switch (CurrentlyEattingWhat) 
            {
                case 1:
                    state = DraggableState.Fly;
                    break;
                case 2:
                    state = DraggableState.Water;
                    break;
                default:
                    state = DraggableState.Null;
                    break;

                
            }
            if (!ItemDropped()) { return; }
            Debug.Log("eatting");
            Eatting(CurrentlyEattingWhat);
            DropEstablished();


        }



        public void Eatting(int item) 
        {
            

            Debug.Log("called");
            switch (CurrentlyEattingWhat) 
            {
                case 1:
                    int[] Update2= new int[2];
                    Update2[0] = 1;
                    Update2[1] = 0;
                    if (IsInventoryZero()[0]) { return; }
                    SubResource(Update2);
                    EattingFood(FoodTaken);
                    break;

                case 2:
                    int[] Update3 = new int[2];
                    Update3[0] = 0;
                    Update3[1] = 1;
                    if (IsInventoryZero()[1]) { return; }
                    SubResource(Update3);
                    Drinking(WaterTaken);
                    break;
            
            
            }


        }









    }

}