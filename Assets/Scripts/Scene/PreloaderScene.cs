using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ET.Scenes;

namespace ET.Scenes.Preloader
{
    public class PreloaderScene : MonoBehaviour
    {
        private AsyncOperation operation;

        [SerializeField] private GameObject _eventSystem;

        private string _preLevel = Scenes._PreLevel.ToString();

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected void Start()
        {
            if (SceneManager.GetActiveScene().name == _preLevel)
            {
                StartCoroutine(AsyncLoading(Scenes._Level_0_MainMenu));
            }
        }

        public void UploadPreScene() ///!!!
        {
            SceneManager.LoadSceneAsync(_preLevel, LoadSceneMode.Additive);
        }

        private IEnumerator AsyncLoading(Scenes scene)
        {
            _eventSystem.SetActive(false);

            operation = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);

            operation.allowSceneActivation = false;
            yield return new WaitForSeconds(1);
            operation.allowSceneActivation = true;

            yield return operation;
        }
    }
}
