using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frog.InputSystem 
{
    public class BTNSelectToggle : MonoBehaviour
    {
        Animator animator;



        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void ToggleSelectedBool()
        {
            animator.SetBool("SelectedBtn", !animator.GetBool("SelectedBtn"));
        }
    }

}