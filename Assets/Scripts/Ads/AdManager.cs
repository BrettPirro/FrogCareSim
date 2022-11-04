using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    string UnityIdNum = "4780849";


  
    private void RunAd()
    {
        Advertisement.Initialize(UnityIdNum);
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Debug.Log("Ad");

            Advertisement.Show("Rewarded_Android");
        }
    }
}
