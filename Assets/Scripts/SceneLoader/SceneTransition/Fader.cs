using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Frog.SceneManagement 
{
    public class Fader : MonoBehaviour
    {

        CanvasGroup canvasGroup;
        public static Fader fader;


        private void Awake()
        {
            if (fader == null) { fader = this; }

            
            canvasGroup = GetComponent<CanvasGroup>();
        }



        public IEnumerator FadeIn(float time) 
        {
            while (canvasGroup.alpha < 1) 
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }


        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                
                yield return null;
            }


        }


        public void FadeOutFunc(float time) 
        {
            StartCoroutine(FadeOut(time));
        }

        public void FadeInFunc(float time)
        {
            StartCoroutine(FadeIn(time));
        }

        public void InstantFadeIn() 
        {
            canvasGroup.alpha = 1;
        }

        public void InstantFadeOut()
        {
            canvasGroup.alpha = 0;
        }


    }

}