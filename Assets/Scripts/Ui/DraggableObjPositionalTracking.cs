using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Frog.UI 
{
    [RequireComponent(typeof(DraggableUIElement))]


    public class DraggableObjPositionalTracking : MonoBehaviour
    {
        [SerializeField]Transform[] PositionalBoundaries;
        DraggableUIElement draggableUI;

        public Action HoldEatAnimation;
        public Action DoneEatAnimation;

        


        private void Start()
        {
            draggableUI = GetComponent<DraggableUIElement>();
        }

        //with positional data checking if within a certain area see if its possible to hook up a link 
        //with the draggable manager with an established action this will allow for more of an animation to be incorperated on drop

        private void Update()
        {
            if (draggableUI.IsMoving() && IsInZone())
            {
                draggableUI.inArea = true;
                HoldEatAnimation();
               
            }
            else 
            {
                
                DoneEatAnimation();
                draggableUI.inArea = false;
                
            }



        }

        private bool IsInZone()
        {
            return (PositionalBoundaries[0].position.y > draggableUI.GrabPos().y &&
                            PositionalBoundaries[1].position.y < draggableUI.GrabPos().y &&
                             PositionalBoundaries[1].position.x > draggableUI.GrabPos().x &&
                             PositionalBoundaries[1].position.x > draggableUI.GrabPos().x);
        }
    }


}
