using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Frog.InputSystem;
using Frog.VisualAdj;
using Frog.UI;

namespace Frog.Gen 
{
    public class WaterBlockSpawner : MonoBehaviour
    {
        [SerializeField] Transform SpawnPos;
        [SerializeField] GameObject WaterBlock;
        [SerializeField][Range(0f,10f)]float SpawnInterval;
        public Func<bool> IsItemMoving;
       
        float timer;
        Dictionary< Vector2, Color> PositionalData = new Dictionary< Vector2, Color>();
        int AboutSpawnIndex;
     

        public static WaterBlockSpawner current;

        private void Awake()
        {
          
            current = this;
            
        }


        void Start()
        {
            if (MoveDropper.current != null) 
            {
                MoveDropper.current.DelayMove += DelaySpawn;
            }

            if (UIDropperDisplay.current != null) 
            {
                UIDropperDisplay.current.WaterBlockColorUpdate += ReturnColorVal;
            }


            AboutSpawnIndex = UnityEngine.Random.Range(1, 4);
        }

        private void OnEnable()
        {
            if (MoveDropper.current != null)
            {
                MoveDropper.current.DelayMove += DelaySpawn;
            }

            if (UIDropperDisplay.current != null)
            {
                UIDropperDisplay.current.WaterBlockColorUpdate += ReturnColorVal;
            }
        }

        private void OnDisable()
        {
            if (MoveDropper.current != null)
            {
                MoveDropper.current.DelayMove -= DelaySpawn;
            }

            if (UIDropperDisplay.current != null)
            {
                UIDropperDisplay.current.WaterBlockColorUpdate -= ReturnColorVal;
            }

        }


        private void FixedUpdate()
        {
            PositionalData = WaterBlockIndex.GenerateDict();
        }




        void Update()
        {
             
            
            if (WaterDroppletTrans.AreAllBlocksGrounded()) { MatchesFound(); }
          



            if (IsItemMoving() == true) { return; }
            else if (timer >= SpawnInterval)
            {
                
                SpawnNewBlock();
             
                
                return;
            }
           
            
            timer += Time.deltaTime;
            



        }

        private void SpawnNewBlock()
        {   
         
            timer = 0;
            
            GameObject obj = Instantiate(WaterBlock, SpawnPos.position, SpawnPos.rotation) as GameObject;
            obj.GetComponent<WaterBlockIndex>().SetIndexOnSpawn(new Vector2((float)PlacementX(new Vector2((float)Math.Round(obj.transform.position.x,2),0)), (float)PlacementY(new Vector2((float)PlacementX(new Vector2((float)Math.Round(obj.transform.position.x, 2), 0)), 0))));
            obj.GetComponent<WaterBlockIndex>().SetColor(ColorCheck(AboutSpawnIndex));
            obj.GetComponent<WaterDroppletTrans>().ColorAdjustment(AboutSpawnIndex);
            AboutSpawnIndex = UnityEngine.Random.Range(1, 5);


           
           
        }


        public int GrabCurrentIndex()
        {
            return AboutSpawnIndex;
        }
    
        public void DelaySpawn(float num) 
        {
            timer -= num;
        }


        int PlacementY(Vector2 pos) 
        {
           
            int returnableVal = 0;
            foreach(var item in PositionalData) 
            {
                if (item.Key.x == pos.x) { returnableVal++; }
            }
            return returnableVal;
        }

        int PlacementX(Vector2 pos) 
        {
            switch (pos.x) 
            {
                case -2.01f:
                    return 0;
                case -1.34f:
                    return 1;
                case -0.67f:
                    return 2;
                case 0f:
                    return 3;
                case 0.67f:
                    return 4;
                case 1.34f:
                    return 5;
                default:
                    return 6;

            }

        }

        

        Color ColorCheck(int val)
        {
            switch (val)
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


        private void DestroyBlock(Vector2 id) 
        {
            Debug.Log("Destroyed");
            WaterBlockIndex.DestroyObjWithSpecificIndex(id);
          

        }


       public Color ReturnColorVal() 
       {
            return ColorCheck(AboutSpawnIndex); 
        
       }


       
        private void MatchesFound() 
        {
           
            foreach(var item in PositionalData) 
            {
                //make the delete Method able to delete lists
                List<Vector2> Matches = new List<Vector2>();
                PositionalData = WaterBlockIndex.GenerateDict();
                foreach (var item2 in PositionalData)
                {
                    if (item2.Value == item.Value) 
                    {
                        if ((((item.Key.x==item2.Key.x-1)|| (item.Key.x == item2.Key.x + 1))&& item.Key.y == item2.Key.y)|| ((item.Key.y == item2.Key.y - 1) || (item.Key.y == item2.Key.y + 1)) && item.Key.x == item2.Key.x) 
                        {
                            if (!Matches.Contains(item2.Key))
                            {
                                Matches.Add(item2.Key);
                            }
                        }

                       

                    }

                }

                WaterBlockIndex.DestroyObjWithSpecificIndex(Matches);
             

            }
        
        }


        


      
    }
}
