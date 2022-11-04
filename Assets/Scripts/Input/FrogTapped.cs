using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Frog.UI;


namespace Frog.InputSystem 
{
    public class FrogTapped : MonoBehaviour
    {
        public enum State { Slap,Pet,Feed,None};

        State CurrentState;

        public UnityEvent OnOutsideTouch;
        public UnityEvent OnSlapTrigger;
        public UnityEvent OnPetTrigger;
        CapsuleCollider2D co;
        Animator animator;
        [SerializeField] GameObject SlapParticle;
        [SerializeField] GameObject PetParticle;
        [SerializeField] GameObject PetObj;
        [SerializeField] float PettingInterval=0.5f;
        public Func<float> UpdateFrogMoodVal;


       

        bool handSpawned = false;
        public static FrogTapped Current;
        GameObject TrackingHand;
        float timer = 0;
        [SerializeField]BoxCollider2D MouthCollider;



        private void Awake()
        {
            Current = this;
        }

       


        void Start()
        {
            co = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<Animator>();
            CurrentState = State.None;

            foreach(DraggableObjPositionalTracking i in FindObjectsOfType<DraggableObjPositionalTracking>()) 
            {
                i.DoneEatAnimation += DoneHoldingEattingAnimation;
                i.HoldEatAnimation += HoldingEattingAnimation;
            }

        }


        void Update()
        {
            if (UpdateFrogMoodVal != null) 
            {
                animator.SetFloat("Mood", UpdateFrogMoodVal());
            }
          


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

               


                    Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                    if (co == touchedCollider)
                    {
                        switch (CurrentState) 
                        {
                            case State.Slap:
                                if (touch.phase == TouchPhase.Began) 
                                {
                                    CurrentState = State.None;
                                    animator.SetTrigger("Hit");
                                    OnOutsideTouch.Invoke();
                                    OnSlapTrigger.Invoke();
                                    Instantiate(SlapParticle, touchPos, Quaternion.identity);
                                }
                              


                            break;
                            case State.Pet:
                                 



                                if (touch.phase == TouchPhase.Began) 
                                {
                                    TrackingHand = Instantiate(PetObj, touchPos, Quaternion.identity) as GameObject;
                                    handSpawned = true;
                                
                                }
                                
                                
                                else if (touch.phase == TouchPhase.Moved) 
                                {
                                    timer += Time.deltaTime;
                                    if (PettingInterval<=timer) { OnPetTrigger.Invoke(); timer = 0; animator.SetTrigger("Pet"); Instantiate(PetParticle, transform.position, Quaternion.identity); }
                                    //look into this line when done in quick succestion
                                    TrackingHand.transform.position = touchPos;
                                }
                                else if (touch.phase == TouchPhase.Ended&&handSpawned) 
                                {
                                    handSpawned = false;
                                    timer = 0;
                                    OnOutsideTouch.Invoke();
                                    CurrentState = State.None;
                                    Destroy(TrackingHand);
                                }
                                

                                break;
                            case State.Feed:

                                break;
                            case State.None:
                                break;
                        }
                    }
                    else if(handSpawned)
                    {

                         Destroy(TrackingHand); OnOutsideTouch.Invoke(); CurrentState = State.None; handSpawned = false; 
                    }
                    
                    
                     

                
            }

        
        }

        public void SwitchCurrentStateToHit()
        {
            CurrentState = State.Slap;
        }

        public void HoldingEattingAnimation() 
        {
            animator.SetBool("NotHovering", true);
            animator.SetTrigger("Eatting");

        }

        public void DoneHoldingEattingAnimation() 
        {
            animator.SetBool("NotHovering", false);
        }

        public void SwitchCurrentStateToPet()
        {
            CurrentState = State.Pet;
        }

    }

}
