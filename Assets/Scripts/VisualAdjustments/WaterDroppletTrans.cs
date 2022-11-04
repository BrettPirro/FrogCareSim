using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frog.VisualAdj 
{
    public class WaterDroppletTrans : MonoBehaviour
    {
        public bool LockAnimation = false;
        Rigidbody2D PhysicsObj;
        [SerializeField]SpriteRenderer spriteRenderer;
        [SerializeField] Sprite BaseBlock;
      


        void Start()
        {
            PhysicsObj = GetComponent<Rigidbody2D>();
            PhysicsObj.velocity= new Vector2(PhysicsObj.velocity.x, -0.1f);
          
        }

       
        void Update()
        {
          
            AnimationDeterminat();

        }

        private void AnimationDeterminat()
        {
            if ( PhysicsObj.velocity.y <=-0.05) { return; }
           

            switch (LockAnimation)
            {
                case false:
                    LeanTween.scaleX(spriteRenderer.gameObject, 1.25f, 0.01f).setEaseOutBounce();
                    spriteRenderer.sprite = BaseBlock;
                    if (spriteRenderer.gameObject.transform.localScale.x == 1.25f) { LockAnimation = true; }
                    break;
                case true:
                    LeanTween.scaleX(spriteRenderer.gameObject, 1f, 0.01f).setEaseOutBounce();
                    PhysicsObj.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                    break;
            }
            
          
        
        
        
        }


        public static bool AreAllBlocksGrounded() 
        {
            bool returnVal=true;

            foreach(WaterDroppletTrans i in FindObjectsOfType<WaterDroppletTrans>()) 
            {
                if (!i.LockAnimation) { returnVal = false; break; }
            }
            return returnVal;

        }


       
        public void ColorAdjustment(int val) 
        {
            switch (val) 
            {
                case 1:
                    spriteRenderer.color = Color.red;
                    break;
                case 2:
                    spriteRenderer.color = Color.white;
                    break;
                case 3:
                    spriteRenderer.color = Color.yellow;
                    break;
                case 4:
                    spriteRenderer.color = Color.blue;
                    break;
            }
        }


    }

}