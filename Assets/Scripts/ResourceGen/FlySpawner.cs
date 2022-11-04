using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frog.InputSystem;
using Frog.ValMangment;

namespace Frog.ResourceGen 
{
    public class FlySpawner : MonoBehaviour
    {
        [SerializeField] GameObject Fly;
        GameObject RecentlySpawnedFly;
        float timer;
        float flyTimer = 0;
        [SerializeField] float GenInterval = 70f;

        [SerializeField] float SpawnRate = 0;

        void Update()
        {
            timer += Time.deltaTime;



            if (timer >= GenInterval)
            {
                timer = 0;
                if (SpawnRate > Random.Range(0, 20))
                {

                    RecentlySpawnedFly = Instantiate(Fly, transform.position, transform.rotation) as GameObject;
                    RecentlySpawnedFly.GetComponent<FlyTapped>().UpdateCurrent(RecentlySpawnedFly.GetComponent<FlyTapped>());
                    try 
                    {
                        FindObjectOfType<InventoryManager>().UpdateCurrentFlyTappedRef();
                    }
                    catch 
                    {
                        Debug.LogWarning("InventoryManager not found");
                    }
                    LeanTween.moveX(RecentlySpawnedFly, 15f, 6.5f);
                }

            }








            if (RecentlySpawnedFly == null) { return; }

            flyTimer += Time.deltaTime;


            if (RecentlySpawnedFly.transform.position.x >= 10f) { Destroy(RecentlySpawnedFly); }
            else if (flyTimer >= Random.Range(.65f, 1f)) { LeanTween.moveY(RecentlySpawnedFly, Random.Range(-.5f, 2f), .5f).setEase(LeanTweenType.animationCurve); flyTimer = 0; }





        }
    }
}
