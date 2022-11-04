using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frog.Gen;
using System;


namespace Frog.InputSystem 
{
    public class MoveDropper : MonoBehaviour
    {
        [SerializeField] float MoveAmount = 0.55f;
        float UpdatedPosX;
        bool moving=false;
        public Action<float> DelayMove;
        public static MoveDropper current;


        private void Awake()
        {
            current = this;
        }



        private void Start()
        {
            if (WaterBlockSpawner.current != null) 
            {
                WaterBlockSpawner.current.IsItemMoving += IsMovingCurrent;
            }


            UpdatedPosX = transform.position.x;
        }

        private void OnEnable()
        {
            if (WaterBlockSpawner.current != null)
            {
                WaterBlockSpawner.current.IsItemMoving += IsMovingCurrent;
            }
        }


        private void OnDisable()
        {
            if (WaterBlockSpawner.current != null)
            {
                WaterBlockSpawner.current.IsItemMoving -= IsMovingCurrent;
            }
        }


        public void MoveSpoutX(bool dir)
        {
            if (UpdatedPosX != transform.position.x) { return; }
            float multi;
            DelayMove(0.2f);
            switch (dir) 
            {
                case true:
                    multi = 1;
                    break;
                default:
                    multi = -1;
                    break;
            }
            float conversion = (Mathf.Clamp((UpdatedPosX + (MoveAmount * multi)), -2.01f, 2.01f));
            UpdatedPosX = (Mathf.Round(conversion*100))*0.01f;
            moving = true;



        }

        private void Update()
        {
            if (UpdatedPosX== transform.position.x) { moving = false; return; }
            LeanTween.moveLocalX(gameObject, UpdatedPosX, 0.015f);
           
        }


        public bool IsMovingCurrent() 
        {
            return moving;
        }



    }
}
