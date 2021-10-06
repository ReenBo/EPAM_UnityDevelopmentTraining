using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ET.Scene
{
    public static class LoaderScenes
    {
        public enum Scene
        {
            _Level_0_Start,
            _Level_0_Loading,
            _Level_1
        }

        public static void Load(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }

    }
}
