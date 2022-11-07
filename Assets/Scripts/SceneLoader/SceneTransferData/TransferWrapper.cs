
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Frog.SceneManagement 
{
    public class TransferWrapper : MonoBehaviour
    {
        [SerializeField] string id;

        string GlobalId => id;

        [ContextMenu("GenId")]
        void GenerateID()
        {
            id = Guid.NewGuid().ToString();
        }



        public Dictionary<string, object> TransferDataToGlobal()
        {
            var Data = new Dictionary<string, object>();

            foreach (var TransObj in GetComponents<ITransfer>())
            {
                Data.Add(GlobalId + '~' + TransObj.GetType(), TransObj.SaveData());

            }



            return Data;
        }

        public static Dictionary<string, object> GrabAllTransWrapValues()
        {
            var Data = new Dictionary<string, object>();

            foreach (var Wrapper in FindObjectsOfType<TransferWrapper>())
            {
                Wrapper.TransferDataToGlobal().ToList().ForEach(x => Data.Add(x.Key, x.Value));
            }

            return Data;
        }









    }

}

