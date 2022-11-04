using UnityEngine;
using Frog.ValMangment;
using UnityEngine.UI;

namespace Frog.UI 
{
    [RequireComponent(typeof(Slider))]
    public class DisplayWaterCollected : MonoBehaviour
    {
        Slider Display;

        private void Awake()
        {
            Display = GetComponent<Slider>();
        }



        private void Start()
        {


            if (WaterCollectedManager.collectedManager != null) 
            {
                WaterCollectedManager.collectedManager.DisplayVal += UpdateDisplay;
            }
            
        }

        private void OnEnable()
        {
            if (WaterCollectedManager.collectedManager != null)
            {
                WaterCollectedManager.collectedManager.DisplayVal += UpdateDisplay;
            }
        }

        private void OnDisable()
        {
            if (WaterCollectedManager.collectedManager != null)
            {
                WaterCollectedManager.collectedManager.DisplayVal -= UpdateDisplay;
            }
        }

        public void UpdateDisplay(int displayVal) 
        {
            Display.value = displayVal;
        }
        
    }


}
