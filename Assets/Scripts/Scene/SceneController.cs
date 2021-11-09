using ET.Core.UIRoot;
using ET.Scenes.Preloader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ET.Scenes
{
    public class SceneController : MonoBehaviour
    {
        private GameObject _preLoaderGameObject = null;
        private PreloaderScene _preloaderScene;

        protected void Start()
        {
            _preLoaderGameObject = GameObject.FindGameObjectWithTag(Tags.PRELOADER);
            _preloaderScene = _preLoaderGameObject.GetComponent<PreloaderScene>();
        }

        public void StartGame()
        {
            _preloaderScene.Load(SceneIndex._Level_1);
        }

        public void Restart()
        {
            _preloaderScene.Load(SceneIndex._Level_1);
            UIRoot.Instance.CloseAllWindow();
        }

        public void ReturnMainMenu()
        {
            SceneManager.LoadSceneAsync(SceneIndex._MainMenu.ToString());
        }

        public void EndGame()
        {
            Application.Quit();
        }
    }
}
