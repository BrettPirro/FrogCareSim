using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciveWrapper : MonoBehaviour
{
    

    public Dictionary<string, object> GrabDictFromCentralTrans() 
    {
        return CentralTransObj.centralTrans.SendDataSet();
    }

    public void LoadDataSetToObj()
    {
        List<string> Vals = new List<string>();

        foreach (var item in GetComponents<IRecive>()) 
        {
          

            if (GrabDictFromCentralTrans().ContainsKey(item.GrabId())) 
            {
                Debug.Log("obj found");
                item.Load(GrabDictFromCentralTrans()[item.GrabId()]);
                Vals.Add(item.GrabId());
            }
           
        }


        CentralTransObj.centralTrans.RemoveDataSet(Vals);
    }





}
