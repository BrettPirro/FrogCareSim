using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Frog.UI 
{
    public class UIStatUpdate : MonoBehaviour
    {

        public static UIStatUpdate current;

        public Func<int> UpdateFlyNum;
        public Func<int> UpdateWaterNum;
        public Func<int> UpdateMoneyNum;

        public Func<float> UpdateHunger;
        public Func<float> UpdateThirst;
        public Func<float> UpdateMood;

        [SerializeField] Slider Hunger;
        [SerializeField] Slider Thirst;
        [SerializeField] Slider Mood;
        [SerializeField] Image ButtonImageAccessConfirmed;

        [SerializeField] Text[] FlyNum;
        [SerializeField] Text[] WaterNum;
        [SerializeField] Text[] MoneyNum;
        


        private void Awake()
        {
            current = this;
        }


        public void MoveImageUp() 
        {
            LeanTween.moveY(ButtonImageAccessConfirmed.gameObject.GetComponent<RectTransform>(), 195f, 0.1f);
            
        }
        public void ChangeColorYellow() 
        {
            ButtonImageAccessConfirmed.color = Color.yellow;
        }

        public void ChangeColorGreen()
        {
            ButtonImageAccessConfirmed.color = Color.green;
        }

        public void ChangeColorRed()
        {
            ButtonImageAccessConfirmed.color = Color.red;
        }


        public void MoveImageDown()
        {
            LeanTween.moveY(ButtonImageAccessConfirmed.gameObject.GetComponent<RectTransform>(), 144f , 0.01f);

        }


        void Update()
        {

            if (UpdateFlyNum != null || UpdateWaterNum != null|| UpdateMoneyNum!=null) 
            {
               foreach(Text FlyTex in FlyNum) 
               {
                    FlyTex.text = UpdateFlyNum().ToString();
               }

               foreach(Text WaterTex in WaterNum) 
               {
                    WaterTex.text= UpdateWaterNum().ToString();
               }

               foreach(Text Moneytex in MoneyNum) 
               {
                    Moneytex.text = UpdateMoneyNum().ToString();     
               }
            }

            if (UpdateHunger!=null || UpdateMood != null|| UpdateThirst != null) 
            {
                Hunger.value = UpdateHunger();
                Mood.value = UpdateMood();
                Thirst.value = UpdateThirst();
            }



        }
    }
}
