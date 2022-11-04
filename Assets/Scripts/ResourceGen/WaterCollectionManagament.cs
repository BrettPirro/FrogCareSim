using Frog.ValMangment;
using System;
using UnityEngine;

namespace Frog.ResourceGen 
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class WaterCollectionManagament : MonoBehaviour
    {
        BoxCollider2D Check;
        Color colorGen=Color.white;
        public Action<int> AddAmount;
        public Action<object, Color> FilterPositionUpdate;

        public static WaterCollectionManagament current;
        
       
        
        bool added=false;

        private void Awake()
        {
            current = this;
        }


        


        void Start()
        {
            Check = GetComponent<BoxCollider2D>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.GetComponent<WaterBlockIndex>()!=null) 
            {
                WaterBlockIndex blockIndex = collision.GetComponent<WaterBlockIndex>();

                if (blockIndex.GrabColor() == colorGen&&added==false) 
                {
                    
                    AddAmount(1);
                    colorGen = GenRandomColor();
                    FilterPositionUpdate((object)WaterBlockIndex.GenerateDict(), colorGen);
                    WaterBlockIndex.DestroyObjWithSpecificIndex(blockIndex.GrabIndex());
                
                    
                
                }
               



            }



        }

        Color GenRandomColor()
        {
            int RandomVal= UnityEngine.Random.Range(1,4);


            switch (RandomVal)
            {
                case 1:
                    return Color.red;

                case 2:
                    return Color.white;

                case 3:
                    return Color.yellow;

                default:
                    return Color.blue;

            }


        }






    }
}
