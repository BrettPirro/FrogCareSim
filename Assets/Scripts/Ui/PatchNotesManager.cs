using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;
using UnityEngine.UI;

namespace Frog.UI 
{
    public class PatchNotesManager : MonoBehaviour
    {
        public struct userAttributes { }
        public struct appAttributes { }

        [SerializeField] Text textField;




        private void Awake()
        {
            ConfigManager.FetchCompleted += SetText;
            ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        }


        void SetText(ConfigResponse response) 
        {
            textField.text = ConfigManager.appConfig.GetString("PatchNotesText");
        }

        
        public void UpdatePatchText() 
        {
            ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        }

        private void OnDestroy()
        {
            ConfigManager.FetchCompleted -= SetText;
        }

    }
}
