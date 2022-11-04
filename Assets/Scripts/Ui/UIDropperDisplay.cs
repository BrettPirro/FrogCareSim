using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Frog.UI 
{
    public class UIDropperDisplay : MonoBehaviour
    {
        [SerializeField]Image InDisplayItem;
        public static UIDropperDisplay current;
        public Func<Color> WaterBlockColorUpdate;


        private void Awake()
        {
            current = this;
        }

       
        void Update()
        {
            if (WaterBlockColorUpdate == null) { return; }
            InDisplayItem.color = WaterBlockColorUpdate();

        }
    }
}
