using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Frog.InputSystem 
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class FlyTapped : MonoBehaviour
    {
        public  static FlyTapped current;
        public Action<int> AddFlyToCount;

        BoxCollider2D boxCollider;

        private void Awake()
        {
            current = this;
        }


        private void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }


        public void UpdateCurrent(FlyTapped Recent) 
        {
            current = Recent;
        }




        void Update()
        {
            if (Input.touchCount == 0) { return; }

            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);

           

            if (touchedCollider == boxCollider&&touch.phase==TouchPhase.Began) 
            {
                Destroy(gameObject);
                AddFlyToCount(1);
            }






        }
    }
}
