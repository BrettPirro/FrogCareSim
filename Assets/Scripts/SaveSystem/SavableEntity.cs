using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Frog.SaveManagment 
{
    public class SavableEntity : MonoBehaviour
    {
        [SerializeField] private string id;

        public string Id => id;

       



        [ContextMenu("Gen ID")]
        private void GenId() 
        {
            id = Guid.NewGuid().ToString();
        }


        public object SaveState() 
        {
            var state = new Dictionary<string, object>();
            foreach(var savable in GetComponents<ISaveable>()) 
            {
                state[savable.GetType().ToString()] = savable.SaveState();
                
            }
            return state;
        }

        public void LoadState(object state) 
        {
            var stateDict = (Dictionary<string, object>)state;

            foreach(var savable in GetComponents<ISaveable>()) 
            {
                string typename = savable.GetType().ToString();

                if(stateDict.TryGetValue(typename, out object savedState)) 
                {
                    savable.LoadState(savedState);
                }
            
            }
        }


    }
}
