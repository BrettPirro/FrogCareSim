using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Frog.ValMangment;

namespace Frog.UI 
{
    public class UICollectionDisplay : MonoBehaviour
    {
        bool PlayingAnimation = false;
        [SerializeField]RectTransform Display;


        private void Start()
        {
            WaterCollectedManager.collectedManager.uiCollectedDisplay += DisplayCollected;
        }


        public void DisplayCollected() 
        {
            if (!PlayingAnimation) 
            {
                Display.localScale = Vector3.zero;
                PlayingAnimation = true;
            }
            
        }


        private void Update()
        {
            switch (PlayingAnimation) 
            {
                case true:
                    LeanTween.scale(Display, Vector3.one, .3f);
                    
                    if (Display.localScale.x >= 1) 
                    {
                        PlayingAnimation = false;
                    }


                    break;
                default:
                    if (!(Display.localScale.x < 0)) 
                    {
                        LeanTween.scale(Display, Vector3.zero, .1f);
                    }

                    break;
            
            }


        }






    }


}
