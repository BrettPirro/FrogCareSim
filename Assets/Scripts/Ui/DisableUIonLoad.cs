using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frog.UI 
{
    public class DisableUIonLoad : MonoBehaviour
    {
        [SerializeField] [Range(0f,1f)]float Delay = .01f; 


        void Start()
        {
            StartCoroutine(LateStart(Delay));
        }

        IEnumerator LateStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            gameObject.SetActive(false);
        }




    }


}