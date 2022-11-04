using System;
using UnityEngine;
using Frog.ResourceGen;
using Frog.SceneManagement;

namespace Frog.ValMangment 
{
    public class WaterCollectedManager : MonoBehaviour,ITransfer
    {
        public Action<int> DisplayVal;
        public Action uiCollectedDisplay;
        public static WaterCollectedManager collectedManager;


        [SerializeField] [Range(0, 100)] int WaterCollectionCap = 5;

        int WaterCollected = 0;

        int WaterFullyFiltered = 0;


        private void Awake()
        {
            collectedManager = this;

            if (WaterCollectionManagament.current != null)
            {

                WaterCollectionManagament.current.AddAmount += WaterCollectedInScene;
            }

        }




        void Update()
        {
            DisplayVal(WaterCollected);

        }

        public void WaterCollectedInScene(int addition)
        {

            Debug.Log("added");


            if (WaterCollected >= WaterCollectionCap) { uiCollectedDisplay(); WaterFullyFiltered += 1; WaterCollected = 0; return; }

            WaterCollected += addition;



        }

        public object SaveData()
        {
            return (object)WaterFullyFiltered;
        }
    }
}
