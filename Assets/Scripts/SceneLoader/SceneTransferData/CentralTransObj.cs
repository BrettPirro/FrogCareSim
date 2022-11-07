using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Frog.SceneManagement 
{
    public class CentralTransObj : MonoBehaviour
    {
        Dictionary<string, object> TransferableData = new Dictionary<string, object>();

        public static CentralTransObj centralTrans;

        private void Awake()
        {
            centralTrans = this;
        }



        public void SaveDataToTransSet()
        {
            TransferWrapper.GrabAllTransWrapValues().ToList().ForEach(x => TransferableData.Add(x.Key, x.Value));
            PrintDict();

        }

        public void LoadDataSet()
        {


            foreach (var items in FindObjectsOfType<ReciveWrapper>())
            {
                items.LoadDataSetToObj();
            }
        }



        public Dictionary<string, object> SendDataSet()
        {
            return TransferableData;
        }

        public void RemoveDataSet(List<string> RemoveVals)
        {
            foreach (var vals in RemoveVals)
            {
                TransferableData.Remove(vals);
            }


        }


        [ContextMenu("Check Dict")]
        private void PrintDict()
        {
            foreach (var i in TransferableData)
            {
                Debug.Log(i.Key);
                Debug.Log(i.Value);
            }
        }
    }
}
