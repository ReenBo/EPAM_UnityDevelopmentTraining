using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ET.Scene;

namespace ET.Scene.Preloader
{
    public class PreloaderScene : MonoBehaviour
    {
        private AsyncOperation operation;

        [SerializeField] private GameObject _eventSystem;

        private string _preLevel = Scenes._PreLevel.ToString();
        private string _mainMenu = Scenes._Level_0_MainMenu.ToString();

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected void Start()
        {
            UploadPreScene();
            //SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        public void UploadPreScene()
        {
            if (SceneManager.GetActiveScene().name == _preLevel)
            {
                StartCoroutine(AsyncLoading());
            }
            else
            {
                SceneManager.LoadSceneAsync(_preLevel, LoadSceneMode.Additive);
                _eventSystem.SetActive(true);
            } 
        }

        private IEnumerator AsyncLoading()
        {
            _eventSystem.SetActive(false);

            operation = SceneManager.LoadSceneAsync(_mainMenu, LoadSceneMode.Additive);

            operation.allowSceneActivation = false;
            yield return new WaitForSeconds(1);
            operation.allowSceneActivation = true;

            yield return operation;
        }

        //private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        //{
        //    SceneManager.LoadScene("_Level_0_MainMenu", LoadSceneMode.Additive);
        //}
    }
}
