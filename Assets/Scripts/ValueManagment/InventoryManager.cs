
using UnityEngine;
using Frog.UI;
using Frog.InputSystem;
using Frog.SaveManagment;
using Frog.ADs;
using System;

namespace Frog.ValMangment 
{
    public class InventoryManager : MonoBehaviour,ISaveable,IRecive
    {
        [SerializeField]string GlobalId;


        [SerializeField] [Range(0, 100)] int flys = 0;
        [SerializeField] [Range(0, 100)] int water = 0;
        [SerializeField] [Range(0, 5000)] int Coins = 0;



        private void Start()
        {
       


            if (UIStatUpdate.current != null)
            {
                UIStatUpdate.current.UpdateMoneyNum += ReturnMoney;  
                UIStatUpdate.current.UpdateFlyNum += ReturnFlys;
                UIStatUpdate.current.UpdateWaterNum += ReturnWater;
                
            }

            if (FlyTapped.current != null)
            {
                FlyTapped.current.AddFlyToCount += AddFlys;
            }

            if (TriggerEatResource.current != null) 
            {
                TriggerEatResource.current.SubResource += SubValues;
                TriggerEatResource.current.IsInventoryZero += CheckIfValIsZero;
            }


            if (AdManager.current != null)
            {
      
                AdManager.current.RewardWater += AddWater;
                AdManager.current.RewardMoney += AddMoney;
                AdManager.current.RewardFly += AddFlys;
            }




        }

        public void Test() 
        {
            Debug.Log("Test");
        }


        public void UpdateCurrentFlyTappedRef()
        {
            if (FlyTapped.current != null)
            {
                FlyTapped.current.AddFlyToCount += AddFlys;
            }
        }



        public void AddFlys(int add)
        {
            flys += add;
        }

      


        public void AddWater(int add) 
        {
            water += add;
        }

        public void AddMoney(int add) 
        {
            Coins += add;
        }




        public void SubCoins(int sub) 
        {
            if (Coins - sub < 0) { return; }

            Coins -= sub;



        }




        public int ReturnFlys()
        {
            return flys;
        }

        public int ReturnWater()
        {
            return water;
        }

        public int ReturnMoney() 
        {
            return Coins;
        }


        public bool[] CheckIfValIsZero() 
        {
            bool[] Returnable = new bool[2];

            Returnable[0] = flys <= 0;
            Returnable[1] = water <= 0;
            return Returnable;


        }

        public bool UseCoins(int UsedAmount) 
        {
            if ((Coins - UsedAmount) < 0) 
            {
                return false;
            }
            else 
            {
                Coins -= UsedAmount;
                return true;
            }
        }



        public void SubValues(int[] newVals)
        {
            
            flys -= newVals[0]*(int)(Mathf.Clamp(Mathf.Sign(flys-1),0,1));
          
          

            water -= newVals[1] * (int)(Mathf.Clamp(Mathf.Sign(water-1), 0, 1));
        }

        public object SaveState()
        {
            SaveData saveData = new SaveData()
            {
                Food = flys,
                WaterDrops=water,
                Money=Coins
            };

            return saveData;

        }

        public void LoadState(object state)
        {
            var saveData = (SaveData)state;
            flys = saveData.Food;
            water = saveData.WaterDrops;
            Coins = saveData.Money;
        }

       

       

        [Serializable]
        private struct SaveData 
        {
            public int Food;
            public int WaterDrops;
            public int Money;
        }




        public void Load(object val)
        {
            water += (int)val;
            Debug.Log("Loaded");
        }

        public string GrabId()
        {
            return GlobalId;
        }




    }

}