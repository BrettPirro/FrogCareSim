using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Frog.SaveManagment 
{
    public class SaveAndLoadSystem : MonoBehaviour
    {
        public string savePath => $"{Application.persistentDataPath}/DataSave.txt";
        public static SaveAndLoadSystem saveSystem;


        private void Awake()
        {
            if (!System.IO.File.Exists(savePath)) { return; }
            Load();
            saveSystem = this;
        }

        public void Save() 
        {
            var state = LoadFile();
            SaveState(state);
            SaveFile(state);
        }

        public void Load() 
        {
            var state = LoadFile();
            LoadState(state);
        }

        public void SaveFile(object state) 
        {
            using (var stream= File.Open(savePath, FileMode.Create)) 
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
            
        }

        Dictionary<string,object> LoadFile() 
        {
            if (!File.Exists(savePath)) 
            {
                Debug.Log("No File Found");
                return new Dictionary<string, object>();
            }

            using(FileStream stream= File.Open(savePath, FileMode.Open)) 
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }


        void SaveState(Dictionary<string,object> state) 
        {
            foreach(var saveable in FindObjectsOfType<SavableEntity>()) 
            {
                state[saveable.Id] = saveable.SaveState();
            }
        }

        void LoadState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SavableEntity>())
            {
                if(state.TryGetValue(saveable.Id,out object saveState)) 
                {
                    saveable.LoadState(saveState);
                }
            }
        }

        private void OnApplicationQuit()
        {
            Save();

        }

        private void OnApplicationPause(bool pause)
        {
            Save();
        }




    }

}
