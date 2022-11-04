using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObj : MonoBehaviour
{
    [SerializeField] GameObject SpawnedObject;
    static bool SpawnedItem = false;


    private void Awake()
    {
        if (!SpawnedItem) 
        {
            GameObject Spawned = Instantiate(SpawnedObject, transform.position, transform.rotation) as GameObject;
            DontDestroyOnLoad(Spawned);
            SpawnedItem = true;
            
        }
    }




}
