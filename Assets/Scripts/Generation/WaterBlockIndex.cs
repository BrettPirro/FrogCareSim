using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterBlockIndex : MonoBehaviour
{
    [SerializeField]Vector2 Index;
    Color color;
    

    public void SetIndexOnSpawn(Vector2 num) 
    {
        Index = num;

        
    }

    public void SetColor(Color colors) 
    {
        color = colors;
    }


    public Color GrabColor() 
    {
        return color;
    }

    public Vector2 GrabIndex() 
    {
        return Index;
    }

    [ContextMenu("Check Id")]
    private void CheckId() 
    {
        Debug.Log(Index);
    }

    
    


    static public GameObject FindObjWithIndex(Vector2 num) 
    {
        foreach(WaterBlockIndex obj in FindObjectsOfType<WaterBlockIndex>()) 
        {
            if (obj.GrabIndex() == num) { return obj.gameObject; }
        }

        return null;
    
    
    }

    static public void DestroyObjWithSpecificIndex(Vector2 num) 
    {
        Destroy(FindObjWithIndex(num).gameObject);
        SortedDictionary<float, WaterBlockIndex> Listing = new SortedDictionary<float, WaterBlockIndex>();

        foreach(var item in GrabAllObjInScene()) 
        {
            if (num.x == item.GrabIndex().x && num.y < item.GrabIndex().y) 
            {
                Listing.Add(item.gameObject.transform.position.y, item);
            }
        }

        foreach(var obj in Listing) 
        {
            obj.Value.SetIndexOnSpawn(new Vector2(obj.Value.GrabIndex().x, obj.Value.GrabIndex().y - 1));
            
        }



    }


    static public void DestroyObjWithSpecificIndex(List<Vector2> num)
    {
      //for the ordered list x and y are reversed to get correct listing for reset


       foreach(var item in num) 
       {
            Destroy(FindObjWithIndex(item).gameObject);
        
       }
        Dictionary<Vector2, Color> RefList = GenerateDict();
        List<float> XvalResets= new List<float>();
       foreach (var inDict in RefList) 
       {
            if (XvalResets.Contains(inDict.Key.x)) { continue; }

            SortedList<float, Vector2> ListToReset = new SortedList<float, Vector2>();

            foreach (var inDict2 in RefList)
            {
                if (inDict2.Key.x ==inDict.Key.x) 
                {
                    ListToReset.Add(inDict2.Key.y, inDict2.Key);

                }

            }

            int index = 0;

            foreach(var obj in ListToReset) 
            {
                

                var RefIndexScript = FindObjWithIndex(obj.Value).GetComponent<WaterBlockIndex>();

                RefIndexScript.SetIndexOnSpawn(new Vector2(inDict.Key.x, index));


                index++;

            }

            XvalResets.Add(inDict.Key.x);




        }




    }





    static public Dictionary<Vector2,Color> GenerateDict() 
    {
        Dictionary<Vector2, Color> Template = new Dictionary<Vector2, Color>();
    
        foreach(var i in GrabAllObjInScene()) 
        {
            Template.Add(i.GrabIndex(), i.GrabColor());
        }

        return Template;
    } 


    static public List<WaterBlockIndex> GrabAllObjInScene() 
    {
        var ReturnVal = new List<WaterBlockIndex>();

        foreach(WaterBlockIndex obj in FindObjectsOfType<WaterBlockIndex>()) 
        {
            ReturnVal.Add(obj);
        }

        return ReturnVal;
    }
}
