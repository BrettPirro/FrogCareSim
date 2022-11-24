using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frog.UI;
using Frog.InputSystem;
using Frog.SaveManagment;
using System;
using Frog.SceneManagement;

namespace Frog.ValMangment 
{
    public class FrogStatManager : MonoBehaviour,ISaveable
    {
        [SerializeField] [Range(0, 100)] float Happiness=0;
        [SerializeField] [Range(0, 100)] float Hungry=0;
        [SerializeField] [Range(0,100)]float thirst=0;

        DateTime LastOpened;
        bool DataLoaded = false;


        private void Start()
        {
            if (TriggerEatResource.current != null) 
            {
                TriggerEatResource.current.Drinking += AddtoThirst;
                TriggerEatResource.current.EattingFood += AddtoHunger;
                
            }

            if (FrogTapped.Current != null) 
            {
                FrogTapped.Current.UpdateFrogMoodVal += ReturnHappiness;
            }




            if (UIStatUpdate.current != null) 
            {
                UIStatUpdate.current.UpdateHunger += ReturnHunger;
                UIStatUpdate.current.UpdateMood+=ReturnHappiness;
                UIStatUpdate.current.UpdateThirst+=ReturnThirst;
            }

            if (!DataLoaded) { return; }

                
                float span = (float)DateTime.Now.Subtract(LastOpened).TotalHours;
                Debug.Log(span);
                if (span >= 1) 
                {
                    Happiness = Happiness + (float)(span* .2);
                    Hungry = Hungry + (float)(span* .5);
                    thirst = thirst + (float)(span* .7);
                }


            
            

            



        }

        private bool CheckIfAnyValueIsZero() 
        {
            return Happiness < 100 || Hungry < 100 || thirst < 100;
        }



        private void OnEnable()
        {
            if (TriggerEatResource.current != null)
            {
                TriggerEatResource.current.Drinking += AddtoThirst;
                TriggerEatResource.current.EattingFood += AddtoHunger;
            }


            if (FrogTapped.Current != null)
            {
                FrogTapped.Current.UpdateFrogMoodVal += ReturnHappiness;
            }

            if (UIStatUpdate.current != null)
            {
                UIStatUpdate.current.UpdateHunger += ReturnHunger;
                UIStatUpdate.current.UpdateMood += ReturnHappiness;
                UIStatUpdate.current.UpdateThirst += ReturnThirst;
            }
        }

        private void OnDisable()
        {

            if (TriggerEatResource.current != null)
            {
                TriggerEatResource.current.Drinking -= AddtoThirst;
                TriggerEatResource.current.EattingFood -= AddtoHunger;
            }



            if (FrogTapped.Current != null)
            {
                FrogTapped.Current.UpdateFrogMoodVal -= ReturnHappiness;
            }

            if (UIStatUpdate.current != null)
            {
                UIStatUpdate.current.UpdateHunger -= ReturnHunger;
                UIStatUpdate.current.UpdateMood -= ReturnHappiness;
                UIStatUpdate.current.UpdateThirst -= ReturnThirst;
            }
        }

        public void FrogSubHappiness(float sub) 
        {
          
            Happiness += sub;
        }

        public void FrogAddHappiness(float add) 
        {
            Happiness -= add;
        }


        public float ReturnHappiness() 
        {
            return Happiness;
        }
        
        public void AddtoHunger(float add) 
        {
            if (Hungry - add < 0) { Hungry = 0; return; }
            Hungry -= add;
        }


        public float ReturnHunger()
        {
            return Hungry;
        }

        public void AddtoThirst(float add)
        {
            if (thirst - add < 0) { thirst = 0; return; }
            thirst -= add;
        }

        private void OnApplicationQuit()
        {
            LastOpened = DateTime.Now;
          
            

        }

        public float ReturnThirst()
        {
            return thirst;
        }

        [Serializable]
        private struct SaveData 
        {
            public string LastDate;
            public float happy;
            public float hunger;
            public float thirsty;
           
        }







        public object SaveState()
        {
            return new SaveData()
            {
                happy = Happiness,
                thirsty = thirst,
                hunger = Hungry,
                LastDate =DateTime.Now.ToString()
            };
        }

        public void LoadState(object state)
        {
            if (state == null) { return; }
            var Savedata = (SaveData)state;
            Happiness = Savedata.happy;
            thirst = Savedata.thirsty;
            Hungry = Savedata.hunger;
            LastOpened = DateTime.Parse(Savedata.LastDate);
            DataLoaded = true;
        }

       

       
    }


}
