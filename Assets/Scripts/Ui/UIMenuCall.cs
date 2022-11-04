using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frog.UI 
{
    public class UIMenuCall : MonoBehaviour
    {

        enum WhichMenuState {Settings,Hats,Ads,Help,Resources}




        WhichMenuState CurrentState;

        [SerializeField] GameObject MainParent;
        [SerializeField] GameObject SmallerBack;
        [SerializeField] GameObject BackingForIcons;
        [SerializeField] GameObject[] menus;
        //0 is settings 1 is help 2 is hats and 3 is ads
        bool ClosingMenu = false;


        public void OpenMenu()
        {
            ClosingMenu = false;
            LeanTween.scale(BackingForIcons, new Vector3(0f, 0f, 0f), 0f);
            LeanTween.scale(SmallerBack, new Vector3(10f, 15f, 1f), 0f);
            OpenUpCurrentlySelectedMenu();
            BackingForIcons.SetActive(true);
            MainParent.SetActive(true);

            LeanTween.scale(BackingForIcons, new Vector3(10f, 15f, 1f), .2f);


        }

        public void OpenSmallerMenu() 
        {
            ClosingMenu = false;
            LeanTween.scale(SmallerBack, new Vector3(0f, 0f, 0f), 0f);
            LeanTween.scale(BackingForIcons, new Vector3(10f, 15f, 1f), 0f);
            OpenUpCurrentlySelectedMenu();

            MainParent.SetActive(true);
            SmallerBack.SetActive(true);
            LeanTween.scale(SmallerBack, new Vector3(10f, 15f, 1f), .2f);

        }


        private void OpenUpCurrentlySelectedMenu()
        {
            if (menus == null) { return; }
            SmallerBack.SetActive(false);
            BackingForIcons.SetActive(false);
            foreach (GameObject Obj in menus)
            {
                Obj.SetActive(false);
            }

           
            switch (CurrentState)
            {
                case WhichMenuState.Settings:
                    TranistionMenu(0);
                    break;
                case WhichMenuState.Help:
                    TranistionMenu(1);
                    break;
                case WhichMenuState.Hats:
                    TranistionMenu(2);
                    break;
                case WhichMenuState.Ads:
                    TranistionMenu(3);
                    break;
                case WhichMenuState.Resources:
                    TranistionMenu(4);
                    break;

            }
        }

        private void TranistionMenu(int i)
        {
            LeanTween.scale(menus[i], Vector3.zero, 0f);
            menus[i].SetActive(true);

            LeanTween.scale(menus[i], Vector3.one, .2f);
        }

        public void CloseMenu()
        {

            LeanTween.scale(BackingForIcons, new Vector3(0f, 0f,0f), .2f);
            ClosingMenu = true;
            foreach (GameObject Obj in menus)
            {
                LeanTween.scale(Obj, Vector3.zero, 0.2f);
            }


        }


        public void CloseSmallerMenu()
        {
            ClosingMenu = true;
            LeanTween.scale(SmallerBack, new Vector3(0f, 0f, 0f), .2f);
            
            foreach (GameObject Obj in menus)
            {
                LeanTween.scale(Obj, Vector3.zero, 0.2f);
            }


        }


        private void Update()
        {
            if (ClosingMenu&&(BackingForIcons.transform.localScale==Vector3.zero|| SmallerBack.transform.localScale == Vector3.zero)) 
            {
                MainParent.SetActive(false);
                ClosingMenu = false;
          
            }
            
           
        }




        public void ChangeState(string state) 
        {
            string convertedString;

            convertedString = (state.ToLower()).Trim();

            switch (convertedString) 
            {
                case "settings":
                    CurrentState = WhichMenuState.Settings;
                    break;
                case "hats":
                    CurrentState = WhichMenuState.Hats;
                    break;
                case "ads":
                    CurrentState = WhichMenuState.Ads;
                    break;
                case "help":
                    CurrentState = WhichMenuState.Help;
                    break;
                case "resources":
                    CurrentState = WhichMenuState.Resources;
                    break;
            }
        }





    }


}