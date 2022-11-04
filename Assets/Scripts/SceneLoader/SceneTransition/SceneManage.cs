using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Frog.SaveManagment;

namespace Frog.SceneManagement 
{
    public class SceneManage : MonoBehaviour
    {
        public static SceneManage sceneManage;

        private void Awake()
        {
            if (sceneManage != null) { return; }
            sceneManage = this;
        }


        private void Start()
        {
            Fader.fader.InstantFadeIn();
            Fader.fader.FadeOutFunc(1f);
        }


        public void LoadNextWithText(string name) 
        {
            StartCoroutine(LoadNextSceneWithString(name));
        }


        IEnumerator LoadNextScene() 
        {
           
            SaveAndLoadSystem.saveSystem.Save();
            yield return Fader.fader.FadeIn(0.5f);
            yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
            SaveAndLoadSystem.saveSystem.Load();
           

            yield return Fader.fader.FadeOut(0.5f);

        }


        IEnumerator LoadNextSceneWithString(string item)
        {
            CentralTransObj.centralTrans.SaveDataToTransSet();//test run Line. use observer pattern with save system when done
            SaveAndLoadSystem.saveSystem.Save();
            yield return Fader.fader.FadeIn(0.5f);
            yield return SceneManager.LoadSceneAsync(item);
            SaveAndLoadSystem.saveSystem.Load();
           
            yield return Fader.fader.FadeOut(0.5f);
            CentralTransObj.centralTrans.LoadDataSet();


        }

        



    }
}
