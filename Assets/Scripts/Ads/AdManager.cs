using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using Frog.ValMangment;

namespace Frog.ADs 
{
    public class AdManager : MonoBehaviour, IUnityAdsListener
    {
#if UNITY_IOS
    string gameID="4780848";
    string Ending="IOS";
#else
        string gameID = "4780849";
        string Ending = "Android";
#endif




        enum StateForReward
        {
            Flys, Water, Money
        }


        StateForReward state;

        static public AdManager current;


        public Action<int> RewardWater;
        public Action<int> RewardMoney;
        public Action<int> RewardFly;


        public void ToggleRewardState(int index)
        {
            switch (index)
            {
                case 0:
                    state = StateForReward.Flys;
                    break;
                case 1:
                    state = StateForReward.Water;
                    break;
                case 2:
                    state = StateForReward.Money;
                    break;
            }
        }

        

        private void Awake()
        {
            current = this;


        }


        private void Start()
        {
            Advertisement.Initialize(gameID);
            Advertisement.AddListener(this);
        }

        public void PlayBasicAd()
        {
            if (Advertisement.IsReady("Interstitial_" + Ending))
            {
                Advertisement.Show("Interstitial_" + Ending);
            }
        }


        public void PlayRewardAd()
        {
            if (Advertisement.IsReady("Rewarded_" + Ending))
            {
                Advertisement.Show("Rewarded_" + Ending);

            }
            else
            {
                Debug.Log("Ad is not ready");
            }
        }

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log("Ad Ready");
        }

        public void OnUnityAdsDidError(string message)
        {
            Debug.Log("Ad Error");
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            Debug.Log("Ad vid started");
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (placementId == ("Rewarded_" + Ending) && showResult == ShowResult.Finished)
            {
                switch (state)
                {
                    case StateForReward.Water:

                        if (RewardWater != null) { RewardWater(5); }
                        break;
                    case StateForReward.Money:
                        if (RewardMoney != null) { RewardMoney(10); }
                        break;
                    default:
                        if (RewardFly != null) { RewardFly(3); }
                        break;

                }
            }
        }
    }
}
