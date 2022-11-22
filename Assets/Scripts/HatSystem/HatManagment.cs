using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatManagment : MonoBehaviour
{
    [SerializeField]HatObj[] HatListing;

    [SerializeField] GameObject HatObj;


    [SerializeField] Transform GridObject;

    
    void Start()
    {
        foreach(HatObj obj in HatListing) 
        {
            GameObject objectSpawn= Instantiate(HatObj, GridObject);
            HatInstantiateObj hat = objectSpawn.GetComponent<HatInstantiateObj>();
            hat.HatSprite.sprite = obj.sprite;
            hat.HatName.text = obj.HatName;
            hat.Price.text = obj.Price.ToString();

        }


    }

   
   
}
