using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frog.SceneManagement 
{
    public class SceneBtnExtraction : MonoBehaviour
    {
       
        public void LoadSceneNext(string name) 
        {
            SceneManage.sceneManage.LoadNextWithText(name);
        }


    }

}
